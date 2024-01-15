
using Microsoft.EntityFrameworkCore;
using P07_UnitTesting.Database;
using P07_UnitTestingUzduotis.Extensions;
using P07_UnitTestingUzduotis.Repositories;
using P07_UnitTestingUzduotis.Services;

namespace P07_UnitTestingUzduotis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>(); //it's ok, repository shoud be scoped
            builder.Services.AddTransient<BlogPostMapper, BlogPostMapper>();

            builder.Services.AddDbContext<MyBlogContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.ConfigureSwaggerDoc();
                opt.ConfigureSwaggerXmlComments();
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

            app.ConfigureCors();


            app.MapControllers();

            app.Run();
        }
    }
}
