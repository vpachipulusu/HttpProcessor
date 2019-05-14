using Microsoft.Extensions.Configuration;
using System.IO;

namespace Runpath.Common.Helpers
{
    public static class AppSettingsConfiguration
    {
        public static IConfiguration Configuration { get; set; }
        public static string AppSetting(string key)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json");

            Configuration = builder.Build();
            return Configuration[key];
        }
    }
}
