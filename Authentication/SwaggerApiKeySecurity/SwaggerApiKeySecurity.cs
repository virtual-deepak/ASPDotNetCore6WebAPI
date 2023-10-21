using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public static class SwaggerApiSecurity
{
    public static void AddSwaggerApiKeySecurity(this SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("SwaggerSecurityDefinition", new OpenApiSecurityScheme
        {
            Description = "ApiKey must be present in header",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Name = "SwaggerSecurityDefinition"
        });

        var key = new OpenApiSecurityScheme()
        {
            Reference = new OpenApiReference()
            {
                Type = ReferenceType.SecurityScheme,
                Id = "SwaggerSecurityDefinition"
            },
            In = ParameterLocation.Header
        };

        var requirement = new OpenApiSecurityRequirement { { key, new List<string>() } };
        options.AddSecurityRequirement(requirement);
    }
}