using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NetCoreApiTokenAuth.Helpers
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context != null && operation != null)
            {
                bool requireAuth = true;
                string id = "Bearer"; // Assuming you are using Bearer token authentication

                if (requireAuth && !string.IsNullOrEmpty(id))
                {
                    operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });

                    operation.Security = new List<OpenApiSecurityRequirement>
                             {
                          new OpenApiSecurityRequirement {
                                 {
                                     new OpenApiSecurityScheme{
                                     Reference = new       OpenApiReference
                                     { Type   =               ReferenceType.SecurityScheme, Id =     id }
                                 },
                             new List<string>() } } };
                }
            }
        }
    }
}

