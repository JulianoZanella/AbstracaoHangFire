using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace HangFire.RN.Commom
{
    public class Util
    {
        private readonly ILogger _log;
        public StringBuilder LogStringBuilder = new StringBuilder();

        public Util(ILogger logger)
        {
            _log = logger;
        }

        public Configuration BuscarConfiguracaoWebConfig()
        {
            var exeFilePath = Path.Combine(Configuracao.WebConfigPath, "web.config");

            if (!File.Exists(exeFilePath))
            {
                var ex = new Exception("Util: web.config não encontrado!\n" + exeFilePath);
                LogException(ex, "BuscarConfiguração");
                throw ex;
            }

            // Mapeia o arquivo de configuração.
            var configFile = new ExeConfigurationFileMap
            {
                ExeConfigFilename = exeFilePath
            };

            return ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
        }

        public static string BuscarConnectionStringHanfire()
        {
            return Configuracao.HangFireConnectionString;
        }

        public static string BuscarConnectionStringApp()
        {
            return Configuracao.HangFireConnectionString;
        }

        public static IConfiguration BuscarConfiguracao()
        {
            return Configuracao.AppSetting;
        }

        public void Log(string msg)
        {
            msg = string.Format("{0}: {1}", DateTime.Now, msg);
            Console.WriteLine(msg);
            LogStringBuilder.AppendLine(msg);
            _log?.LogInformation(msg);
        }

        public void LogException(Exception ex, string funcao)
        {
            string mensagem = string.Format("ERRO na função: {0} - {1}\nStackTrace:\n{2}", funcao, ex.Message, ex.StackTrace);
            mensagem = string.Format("{0}: {1}", DateTime.Now, mensagem);
            Console.WriteLine(mensagem);
            LogStringBuilder.AppendLine(mensagem);
            _log?.LogError(mensagem);
            if (ex.InnerException != null)
            {
                mensagem = "Inner Exception: " + ex.InnerException.Message;
                LogStringBuilder.AppendLine(mensagem);
                _log?.LogError(mensagem, LogLevel.Error);
            }
        }

        public void GravarLogEmArquivo(string nomeArquivo)
        {
            var path = Configuracao.CaminhoLog;
            try
            {
                Directory.CreateDirectory(path);
                var diretorioLog = string.Format(@"{0}\{1}.{2}.txt", path, DateTime.Now.ToString("yyyy.MM.dd"), nomeArquivo);
                Log(string.Format("Gravando em arquivo: {0}", diretorioLog));
                File.Create(diretorioLog).Dispose();
                using (StreamWriter file = new StreamWriter(diretorioLog, true))
                {
                    file.Write(LogStringBuilder.ToString());
                }
                Log("Arquivo de Log Finalizado");
            }
            catch (Exception ex)
            {
                LogException(ex, "Util.GravarLogEmArquivo");
                throw ex;
            }
        }
    }
}
