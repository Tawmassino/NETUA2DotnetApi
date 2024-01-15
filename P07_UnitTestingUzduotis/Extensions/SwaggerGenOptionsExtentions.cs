using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace P07_UnitTestingUzduotis.Extensions;

public static class SwaggerGenOptionsExtentions
{
    public static void ConfigureSwaggerDoc(this SwaggerGenOptions opt)
    {
        opt.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Blog posts <S>SA erroneous%^^ and# not working>> API",
            Description = "An ASP.NET Core Web API for managing Blog posts Deliberate Errors",
        });
    }
    public static void ConfigureSwaggerXmlComments(this SwaggerGenOptions opt)
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        opt.IncludeXmlComments(xmlPath);
    }
}
