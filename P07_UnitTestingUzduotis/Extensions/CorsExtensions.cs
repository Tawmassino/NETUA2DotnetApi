namespace P07_UnitTestingUzduotis.Extensions;

public static class CorsExtensions
{
    public static void ConfigureCors(this WebApplication app)
    {
        app.UseCors(builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
    }
}
