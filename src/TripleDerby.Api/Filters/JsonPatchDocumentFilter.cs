using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TripleDerby.Api.Filters
{
    [ExcludeFromCodeCoverage]
    public class JsonPatchDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var schemas = swaggerDoc.Components.Schemas.ToList();
            foreach (var (key, _) in schemas)
            {
                if (key.StartsWith("OperationOf") || key.StartsWith("JsonPatchDocumentOf"))
                {
                    swaggerDoc.Components.Schemas.Remove(key);
                }
            }

            swaggerDoc.Components.Schemas.Add("Operation",
                new OpenApiSchema
                {
                    Type = "object",
                    Properties = new Dictionary<string, OpenApiSchema>
                    {
                        {"op", new OpenApiSchema {Type = "string"}},
                        {"value", new OpenApiSchema {Type = "object", Nullable = true}},
                        {"path", new OpenApiSchema {Type = "string"}}
                    }
                });

            swaggerDoc.Components.Schemas.Add("JsonPatchDocument",
                new OpenApiSchema
                {
                    Type = "array",
                    Items = new OpenApiSchema
                    {
                        Reference = new OpenApiReference {Type = ReferenceType.Schema, Id = "Operation"}
                    },
                    Description = "Array of operations to perform"
                });

            foreach (var (_, value) in swaggerDoc.Paths
                .SelectMany(p => p.Value.Operations)
                .Where(p => p.Key == OperationType.Patch))
            {
                foreach (KeyValuePair<string, OpenApiMediaType> item in value.RequestBody.Content.Where(c =>
                    c.Key != "application/json-patch+json"))
                {
                    value.RequestBody.Content.Remove(item.Key);
                }

                KeyValuePair<string, OpenApiMediaType> response =
                    value.RequestBody.Content.Single(c => c.Key == "application/json-patch+json");

                response.Value.Schema = new OpenApiSchema
                {
                    Reference = new OpenApiReference {Type = ReferenceType.Schema, Id = "JsonPatchDocument"}
                };
            }
        }
    }
}