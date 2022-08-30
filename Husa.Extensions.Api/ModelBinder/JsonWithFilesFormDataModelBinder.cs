// https://thomaslevesque.com/2018/09/04/handling-multipart-requests-with-json-and-file-uploads-in-asp-net-core/
namespace Husa.Extensions.Api.ModelBinder
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;

    public class JsonWithFilesFormDataModelBinder : IModelBinder
    {
        private readonly IOptions<MvcNewtonsoftJsonOptions> jsonOptions;
        private readonly FormFileModelBinder formFileModelBinder;

        public JsonWithFilesFormDataModelBinder(IOptions<MvcNewtonsoftJsonOptions> jsonOptions, ILoggerFactory loggerFactory)
        {
            this.jsonOptions = jsonOptions ?? throw new ArgumentNullException(nameof(jsonOptions));
            this.formFileModelBinder = loggerFactory != null ? new FormFileModelBinder(loggerFactory) : throw new ArgumentNullException(nameof(loggerFactory));
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Retrieve the form part containing the JSON
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (valueResult == ValueProviderResult.None)
            {
                // The JSON was not found
                var message = bindingContext.ModelMetadata.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(bindingContext.FieldName);
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, message);
                return Task.CompletedTask;
            }

            return DeserializeAndBindAsync(bindingContext, valueResult);

            async Task DeserializeAndBindAsync(ModelBindingContext bindingContext, ValueProviderResult valueResult)
            {
                var rawValue = valueResult.FirstValue;

                // Deserialize the JSON
                var model = JsonConvert.DeserializeObject(rawValue, bindingContext.ModelType, this.jsonOptions.Value.SerializerSettings);

                // Now, bind each of the IFormFile properties from the other form parts
                foreach (var property in bindingContext.ModelMetadata.Properties)
                {
                    if (property.ModelType != typeof(IFormFile))
                    {
                        continue;
                    }

                    var fieldName = property.BinderModelName ?? property.PropertyName;
                    var modelName = fieldName;
                    var propertyModel = property.PropertyGetter(bindingContext.Model);
                    ModelBindingResult propertyResult;

                    using (bindingContext.EnterNestedScope(property, fieldName, modelName, propertyModel))
                    {
                        await this.formFileModelBinder.BindModelAsync(bindingContext);
                        propertyResult = bindingContext.Result;
                    }

                    if (propertyResult.IsModelSet)
                    {
                        // The IFormFile was sucessfully bound, assign it to the corresponding property of the model
                        property.PropertySetter(model, propertyResult.Model);
                    }
                    else if (property.IsBindingRequired)
                    {
                        var message = property.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(fieldName);
                        bindingContext.ModelState.TryAddModelError(modelName, message);
                    }
                }

                // Set the successfully constructed model as the result of the model binding
                bindingContext.Result = ModelBindingResult.Success(model);
            }
        }
    }
}
