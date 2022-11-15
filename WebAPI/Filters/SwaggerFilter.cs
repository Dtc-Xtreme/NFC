using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Runtime.Serialization;

namespace WebAPI.Filters
{
    public class SwaggerFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            model.Properties.Remove("IsDirty");
            model.Properties.Remove("IsNew");

        }
    }
}
