using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;

namespace WebAPI.Config
{
    public class CommaSeparatedArrayModelBinder : IModelBinder
    {
        private static Task CompletedTask => Task.CompletedTask;

        private static readonly Type[] supportedElementTypes = {
        typeof(int), typeof(long), typeof(short), typeof(byte),
        typeof(uint), typeof(ulong), typeof(ushort), typeof(Guid)
    };

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (!IsSupportedModelType(bindingContext.ModelType)) return CompletedTask;

            var providerValue = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (providerValue == ValueProviderResult.None) return CompletedTask;

            // Each value self may contains a series of actual values, split it with comma
            var strs = providerValue.Values.SelectMany(s => s.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();

            if (!strs.Any() || strs.Any(s => String.IsNullOrWhiteSpace(s)))
                return CompletedTask;

            var elementType = bindingContext.ModelType.GetElementType();
            if (elementType == null) return CompletedTask;

            var realResult = CopyAndConvertArray(strs, elementType);

            bindingContext.Result = ModelBindingResult.Success(realResult);

            return CompletedTask;
        }

        internal static bool IsSupportedModelType(Type modelType)
        {
            return modelType.IsArray && modelType.GetArrayRank() == 1
                    && modelType.HasElementType
                    && supportedElementTypes.Contains(modelType.GetElementType());
        }

        private static Array CopyAndConvertArray(IList<string> sourceArray, Type elementType)
        {
            var targetArray = Array.CreateInstance(elementType, sourceArray.Count);
            if (sourceArray.Count > 0)
            {
                var converter = TypeDescriptor.GetConverter(elementType);
                for (var i = 0; i < sourceArray.Count; i++)
                    targetArray.SetValue(converter.ConvertFromString(sourceArray[i]), i);
            }
            return targetArray;
        }
    }

    public static class CommaSeparatedArrayModelBinderServiceCollectionExtensions
    {
        private static int FirstIndexOfOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            var result = 0;

            foreach (var item in source)
            {
                if (predicate(item))
                    return result;

                result++;
            }

            return -1;
        }

        private static int FindModelBinderProviderInsertLocation(this IList<IModelBinderProvider> modelBinderProviders)
        {
            var index = modelBinderProviders.FirstIndexOfOrDefault(i => i is FloatingPointTypeModelBinderProvider);
            return index < 0 ? index : index + 1;
        }

        public static void InsertCommaSeparatedArrayModelBinderProvider(this IList<IModelBinderProvider> modelBinderProviders)
        {
            // Argument Check
            if (modelBinderProviders == null)
                throw new ArgumentNullException(nameof(modelBinderProviders));

            var providerToInsert = new CommaSeparatedArrayModelBinderProvider();

            // Find the location of SimpleTypeModelBinder, the CommaSeparatedArrayModelBinder must be inserted before it.
            var index = modelBinderProviders.FindModelBinderProviderInsertLocation();

            if (index != -1)
                modelBinderProviders.Insert(index, providerToInsert);
            else
                modelBinderProviders.Add(providerToInsert);
        }

        public static MvcOptions AddCommaSeparatedArrayModelBinderProvider(this MvcOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            options.ModelBinderProviders.InsertCommaSeparatedArrayModelBinderProvider();
            return options;
        }

        public static IMvcBuilder AddCommaSeparatedArrayModelBinderProvider(this IMvcBuilder builder)
        {
            builder.AddMvcOptions(options => AddCommaSeparatedArrayModelBinderProvider(options));
            return builder;
        }
    }
}
