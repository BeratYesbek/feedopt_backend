
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
    public class CommaSeparatedArrayModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return CommaSeparatedArrayModelBinder.IsSupportedModelType(context.Metadata.ModelType) ? new CommaSeparatedArrayModelBinder() : null;
        }
    }
}
