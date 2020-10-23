using HangFire.Dashboard.Commom;
using HangFire.RN.Enums;
using Microsoft.AspNetCore.Builder;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HangFire.Dashboard.Jobs
{
    public abstract class BaseJob
    {
        private readonly EventLog _log;
        private string _connectionString;
        private StringBuilder _sb = new StringBuilder();
        private string _webConfigPath;

        public BaseJob(string logSource, string logFileName, bool preparaConexao = false)
        {
            _log = new EventLog();
            if (!EventLog.SourceExists(logSource))
            {
                EventLog.CreateEventSource(logSource, logFileName);
            }
            _log.Source = logSource;
            _log.Log = logFileName;
            _webConfigPath = ConfigurationManager.WebConfigPath;
            if (preparaConexao && !PreparaConexao())
            {
                var ex = new Exception("BaseJob: conexao não encontrada!");
                LogException(ex, "BaseJob");
                throw ex;
            }
        }

        public abstract void Rodar();

        protected void ExecutarUmaVez(Action funcao)
        {
            Log("Executando uma vez");
            HangfireService.ExecutarUmaVez(funcao);
        }

        protected void ExecutarRepetidamente(Action funcao, TimeSpan tempo)
        {
            Log("Executando repetidamente");
            HangfireService.ExecutarRepetidamente(funcao, tempo);
        }

        protected void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia)
        {
            Log("Executando repetidamente");
            HangfireService.ExecutarRepetidamente(funcao, frequencia);
        }

        protected void Log(string msg, EventLogEntryType tipo = EventLogEntryType.Information)
        {
            msg = string.Format("{0}: {1}", DateTime.Now, msg);
            Console.WriteLine(msg);
            _sb.AppendLine(msg);
            _log.WriteEntry(msg, tipo);
        }

        protected void LogException(Exception ex, string funcao)
        {
            string mensagem = string.Format("ERRO na função: {0} - {1}\nStackTrace:\n{2}", funcao, ex.Message, ex.StackTrace);
            Log(mensagem, EventLogEntryType.Error);
            if (ex.InnerException != null)
            {
                mensagem = "Inner Exception: " + ex.InnerException.Message;
                Log(mensagem, EventLogEntryType.Error);
            }
        }

        private XDocument BuscarConfiguracao()
        {
            var exeFilePath = Path.Combine(_webConfigPath, "web.config");


            Log("Config Path: " + exeFilePath);

            if (!File.Exists(exeFilePath))
            {
                var ex = new Exception("BaseJob: web.config não encontrado!\n" + exeFilePath);
                LogException(ex, "Buscar Configuração");
                throw ex;
            }

            return XDocument.Load(exeFilePath);
        }

        private bool PreparaConexao()
        {
            Log("Iniciando PreparaConexao");

            try
            {
                var config = BuscarConfiguracao();
                // CooperDesp.Data.Conexao.ConexaoFactoryEnum = (CooperDesp.Data.Enums.ConexaoFactoryEnum)0;
                var settings = config.Element("configuration").Element("applicationSettings").Elements("CooperDesp.RN.Properties.Settings");
                var ambiente = from c in settings.Descendants("setting") where c.Attribute("name").Value == "Ambiente" select c.Value;
                var amb = ambiente.FirstOrDefault() + "_sqlserver";
                var connectionstring = config.Element("configuration")
                    .Descendants("connectionStrings").Descendants("add")
                    .Where(c => c.Attribute("name").Value == amb)
                    .Select(c => c.Attribute("connectionString").Value)
                    .FirstOrDefault();
                _connectionString = connectionstring;
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

        /*
        protected void EnviarEmail(TipoEnvioEmail tipoEnvioEmail, string mensagem)
        {
            Log("Enviando email tipo:" + tipoEnvioEmail.ToString());
            var configuracaoEnvioEmail = ConfiguracaoEnvioEmail.Busca(tipoEnvioEmail);
            if (!Conexao.MandaEmail(configuracaoEnvioEmail.Remetente, configuracaoEnvioEmail.Destinatarios, configuracaoEnvioEmail.Assunto, mensagem))
            {
                Log("Erro ao enviar Email");
            }
        }

        protected void GravarEmArquivo(string nomeArquivo)
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
