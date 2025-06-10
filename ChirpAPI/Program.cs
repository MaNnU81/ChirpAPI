using ChirpAPI.Models;
using ChirpAPI.Services.Services;
using ChirpAPI.Services.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace ChirpAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(builder.Configuration)
            //    .Enrich.FromLogContext()
            //    .CreateLogger();

            // Add services to the container.
            builder.Services.AddDbContext<CinguettioContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddControllers();

            builder.Services.AddScoped<IChirpsService, GiovanniChirpsService>();

            var app = builder.Build();
            builder.Services.AddLogging();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

           // app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
