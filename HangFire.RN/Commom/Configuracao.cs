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
        public static string HangFireConnectionString { get { return AppSetting.GetConnectionString("HangfireConnection"); } }
        public static string AppConnectionString { get { return AppSetting.GetConnectionString("AppConnection"); } }
        public static string CaminhoLog { get { return AppSetting.GetSection("CaminhoLog").Value; } }
        public static int DapperCommandTimeout { get { return int.Parse(AppSetting.GetSection("DapperCommandTimeout").Value); } }
    }
}
