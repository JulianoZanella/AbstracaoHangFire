using Microsoft.Extensions.Configuration;
using System.IO;

namespace HangFire.RN.Commom
{
    public static class Configuracao
    {
        public static IConfiguration AppSetting { get; }
        static Configuracao()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
        }

        public static string WebConfigPath { get { return AppSetting.GetSection("WebConfigPath").Value; } }

    }
}
