using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Library.API.SwaggerConfiguration
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation == null || context == null)
            {
                return;
            }

            var fileParams = context.ApiDescription?.ParameterDescriptions?
                .Where(p => p.ModelMetadata?.ModelType == typeof(IFormFile))
                .ToList();

            if (fileParams != null && fileParams.Any())
            {
                foreach (var param in fileParams)
                {
                    operation.RequestBody ??= new OpenApiRequestBody
                    {
                        Content = new Dictionary<string, OpenApiMediaType>()
                    };

                    if (!operation.RequestBody.Content.ContainsKey("multipart/form-data"))
                    {
                        operation.RequestBody.Content["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = new Dictionary<string, OpenApiSchema>()
                            }
                        };
                    }

                    operation.RequestBody.Content["multipart/form-data"].Schema.Properties[param.Name] = new OpenApiSchema
                    {
                        Type = "string",
                        Format = "binary"
                    };
                }
            }
        }
    }
}