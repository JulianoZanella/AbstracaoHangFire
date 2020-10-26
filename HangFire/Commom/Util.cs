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
        private string _connectionString;
        public StringBuilder _sb = new StringBuilder();
        private string _webConfigPath;

        public Util(ILogger logger, bool preparaConexao = false)
        {
            _log = logger;
            _webConfigPath = Configuracao.WebConfigPath;
            if (preparaConexao && !PreparaConexao())
            {
                var ex = new Exception("BaseJob: conexao não encontrada!");
                LogException(ex, "BaseJob");
                throw ex;
            }
        }
        public Configuration BuscarConfiguracao()
        {
            var exeFilePath = Path.Combine(_webConfigPath, "web.config");

            if (!File.Exists(exeFilePath))
            {
                var ex = new Exception("BaseJob: web.config não encontrado!\n" + exeFilePath);
                LogException(ex, "Buscar Configuração");
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
            //TODO: Buscar das configs
            return "Server=localhost;Database=HangFireSample;User Id=developer;Password=12345678";
        }

        public bool PreparaConexao()
        {
            Log("Iniciando PreparaConexao");

            try
            {
                var config = BuscarConfiguracao();
                //CooperDesp.Data.Conexao.ConexaoFactoryEnum = (CooperDesp.Data.Enums.ConexaoFactoryEnum)int.Parse(config.AppSettings.Settings["ConexaoFactoryEnum"].Value);
                _connectionString = config.ConnectionStrings.ConnectionStrings[config.AppSettings.Settings["Ambiente"].Value + "_sqlserver"].ConnectionString;
                Log(_connectionString);
            }
            catch (Exception ex)
            {
                LogException(ex, "ConnectionString");
                return false;
            }

            try
            {
                //string factory = CooperDesp.Data.Conexao.Factory;
                //new Parameters(factory, _connectionString);
            }
            catch (Exception ex)
            {
                LogException(ex, "PreparaConexao");
                return false;
            }

            return true;
        }

        public void Log(string msg)
        {
            msg = string.Format("{0}: {1}", DateTime.Now, msg);
            Console.WriteLine(msg);
            _sb.AppendLine(msg);
            _log?.LogInformation(msg);
        }

        public void LogException(Exception ex, string funcao)
        {
            string mensagem = string.Format("ERRO na função: {0} - {1}\nStackTrace:\n{2}", funcao, ex.Message, ex.StackTrace);
            mensagem = string.Format("{0}: {1}", DateTime.Now, mensagem);
            Console.WriteLine(mensagem);
            _sb.AppendLine(mensagem);
            _log?.LogError(mensagem);
            if (ex.InnerException != null)
            {
                mensagem = "Inner Exception: " + ex.InnerException.Message;
                _sb.AppendLine(mensagem);
                _log?.LogError(mensagem, LogLevel.Error);
            }
        }

        /*
        public void EnviarEmail(TipoEnvioEmail tipoEnvioEmail, string mensagem)
        {
            Log("Enviando email tipo:" + tipoEnvioEmail.ToString());
            var configuracaoEnvioEmail = ConfiguracaoEnvioEmail.Busca(tipoEnvioEmail);
            if (!Conexao.MandaEmail(configuracaoEnvioEmail.Remetente, configuracaoEnvioEmail.Destinatarios, configuracaoEnvioEmail.Assunto, mensagem))
            {
                Log("Erro ao enviar Email");
            }
        }

        public void GravarEmArquivo(string nomeArquivo)
        {
            var path = Configuracao.Path("CaminhoLog");
            Directory.CreateDirectory(path);
            var diretorioLog = string.Format(@"{0}\{1}.{2}.txt", path, DateTime.Now.ToString("yyyy.MM.dd"), nomeArquivo);
            Log(string.Format("Gravando em arquivo: {0}", diretorioLog));
            File.Create(diretorioLog).Dispose();
            using (StreamWriter file = new StreamWriter(diretorioLog, true))
            {
                file.Write(_sb.ToString());
            }
            Log("Arquivo de Log Finalizado");
        }
        */

    }
}
