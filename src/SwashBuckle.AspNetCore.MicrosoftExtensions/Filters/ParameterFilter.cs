using SwashBuckle.AspNetCore.MicrosoftExtensions.Attributes;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;

namespace SwashBuckle.AspNetCore.MicrosoftExtensions.Filters
{
    public class ParameterFilter : IParameterFilter
    {
        public void Apply(IParameter parameter, ParameterFilterContext context)
        {
            if (parameter == null || context == null)
                return;

            if (context.PropertyInfo == null)
                return;

            if (!context.PropertyInfo.CustomAttributes.Any())
                return;

            if (!context.PropertyInfo.GetCustomAttributes().Any(attr => attr is MetadataAttribute))
                return;

            MetadataAttribute attribute = (MetadataAttribute)context.PropertyInfo.GetCustomAttributes()
                .FirstOrDefault(attr => attr is MetadataAttribute);

            if (!string.IsNullOrEmpty(attribute?.Summary))
                parameter.Extensions.Add("x-ms-summary", attribute.Summary);

            if (attribute != null)
                parameter.Extensions.Add("x-ms-visibility", attribute.Visibility.ToString());

            if (attribute != null)
                parameter.Description = attribute.Description;
        }
    }
}