
using ClothingStoreAPI.Filters;
using ClothingStoreAPI.Middleware;
using ClothingStoreAPI.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ClothingStoreAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
           
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddLogging(config =>
            {
                config.ClearProviders();
                config.AddConsole();
                config.AddDebug();
                
            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCorsPolicy",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:5500")
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .WithHeaders("Content-Type");
                    });
            });

            builder.Services.AddSingleton<SalesService>();

            builder.Services.AddControllers(options =>
            {
                
                options.Filters.Add(typeof(CustomExceptionFilter));
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            var salesService = app.Services.GetRequiredService<SalesService>();

            // Adicione os dados de exemplo chamando o método AddSampleData
            salesService.AddSampleData();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseCors("MyCorsPolicy");
            app.UseAuthorization();
            app.UseMiddleware<LoggingMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
