using Microsoft.Extensions.Configuration;
using System.IO;

namespace HangFire.Dashboard.Commom
{
    public static class ConfigurationManager
    {
        public static IConfiguration AppSetting { get; }
        static ConfigurationManager()
        {
            AppSetting = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).FullName)
                    .AddJsonFile("appsettings.json")
                    .Build();
        }

        public static string WebConfigPath { get { return AppSetting.GetValue<string>("WebConfigPath"); } }

    }
}
