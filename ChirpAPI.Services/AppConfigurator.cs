using Microsoft.Extensions.Configuration;

namespace ChirpAPI
{
    public class AppConfigurator
    {
        public static IConfiguration Configuration { get; private set; }
        static AppConfigurator()
        {
            if (Configuration == null)
            {
                Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
                    .Build();
            }
        }

        public static string? GetConnectionString()
        {
            return Configuration.GetConnectionString("Default");
            
        }
        
    }
}
