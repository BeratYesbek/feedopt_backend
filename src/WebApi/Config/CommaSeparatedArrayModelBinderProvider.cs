using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAPI.Config
{
    public class CommaSeparatedArrayModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return CommaSeparatedArrayModelBinder.IsSupportedModelType(context.Metadata.ModelType)
                ? new CommaSeparatedArrayModelBinder()
                : null;
        }
    }
}