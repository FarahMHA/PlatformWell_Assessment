using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PlatformWell_Assessment.Entities;
using PlatformWell_Assessment.Operations;

namespace PlatformWell_Assessment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager config = builder.Configuration;

            builder.Services.AddDbContext<PlatformWellDbContext>(options => options.UseSqlServer(config["PlatformWellConnectionString"]));

            builder.Services.AddScoped<PlatformWellManagement>();

            // Add services to the container.
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            

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