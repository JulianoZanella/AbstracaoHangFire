using HangFire.RN.Commom;
using HangFire.RN.Enums;
using HangFire.RN.Models;
using HangFire.RN.Servicos;
using System;

namespace Hangfire.RN.Models
{
    public abstract class BaseJob : IBaseJob
    {
        private readonly Util _util;
        private readonly HangfireService _service;
        public BaseJob(bool preparaConexao = false)
        {
            _util = new Util(null, preparaConexao);
            _service = new HangfireService();
        }

        public abstract void Rodar();

        public void ExecutarUmaVez(Action funcao)
        {
            _service.ExecutarUmaVez(funcao);
        }

        public void ExecutarRepetidamente(Action funcao, TimeSpan tempo) 
        {
            _service.ExecutarRepetidamente(funcao, tempo);
        }

        public void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia)
        {
            _service.ExecutarRepetidamente(funcao, frequencia);
        }

        protected void Log(string msg)
        {
            _util.Log(msg);
        }

        protected void LogException(Exception ex, string msg)
        {
            _util.LogException(ex, msg);
        }

        public string PegaLog()
        {
            return _util.LogStringBuilder.ToString();
        }

    }
}
