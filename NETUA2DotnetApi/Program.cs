
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NETUA2DotnetApi.DataLayer;
using NETUA2DotnetApi.DataLayer.Repositories;
using NETUA2DotnetApi.Services;
using System.Reflection;

namespace NETUA2DotnetApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<ITodoRepository, TodoRepository>(); //uzregistravome servisas i DI konteineri
            builder.Services.AddTransient<IToDoEmailService, ToDoEmailService>();
            builder.Services.AddSingleton<IUzduotisValuesService, UzduotisValuesService>();
            builder.Services.AddSingleton<ITodoMapper, TodoMapper>();
            builder.Services.AddTransient<ITodoValidationService, TodoValidationService2>();

            builder.Services.AddDbContext<TodoContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen( opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Mokymai ToDo API",
                    Description = "An ASP.NET Core Web API for managing ToDo items",
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opt.IncludeXmlComments(xmlPath);

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
