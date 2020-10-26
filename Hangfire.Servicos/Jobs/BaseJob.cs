using HangFire.RN.Commom;
using HangFire.RN.Enums;
using HangFire.RN.Servicos;
using System;

namespace Hangfire.Servicos.Jobs
{
    public abstract class BaseJob
    {
        private readonly Util _util;
        public BaseJob(bool preparaConexao = false)
        {
            _util = new Util(null, preparaConexao);
        }

        public abstract void Rodar();

        public static void ExecutarUmaVez(Action funcao)
        {
            HangfireService.ExecutarUmaVez(funcao);
        }

        public static void ExecutarRepetidamente(Action funcao, TimeSpan tempo)
        {
            HangfireService.ExecutarRepetidamente(funcao, tempo);
        }

        public static void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia)
        {
            HangfireService.ExecutarRepetidamente(funcao, frequencia);
        }

        protected void Log(string msg)
        {
            _util.Log(msg);
        }

        protected void LogException(Exception ex, string msg)
        {
            LogException(ex, msg);
        }

        public string PegaLog()
        {
            return _util._sb.ToString();
        }
    }
}
