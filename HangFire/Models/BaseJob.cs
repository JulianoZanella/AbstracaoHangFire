using HangFire.RN.Commom;
using HangFire.RN.Enums;
using HangFire.RN.Models;
using HangFire.RN.Servicos;
using Microsoft.Extensions.Logging;
using System;

namespace Hangfire.RN.Models
{
    public abstract class BaseJob : IBaseJob
    {
        private readonly Util _util;
        private readonly HangfireService _service;
        public BaseJob(ILogger logger = null)
        {
            _util = new Util(logger);
            _service = new HangfireService();
        }

        /// <summary>
        /// Método usado para executar a rotina
        /// </summary>
        public abstract void Rodar();

        /// <summary>
        /// Método para adicionar um serviço que executa uma única vez no hangfire
        /// </summary>
        /// <param name="funcao">Action que deve executar</param>
        public void ExecutarUmaVez(Action funcao)
        {
            _service.ExecutarUmaVez(funcao);
        }

        /// <summary>
        /// Método que executa repetidamente eternamente
        /// </summary>
        /// <param name="funcao">Action que deve executar</param>
        /// <param name="tempo">O tempo em que ela deve reexecutar</param>
        public void ExecutarRepetidamente(Action funcao, TimeSpan tempo) 
        {
            _service.ExecutarRepetidamente(funcao, tempo);
        }

        /// <summary>
        /// Método que executa repetidamente eternamente
        /// </summary>
        /// <param name="funcao">Action que deve executar</param>
        /// <param name="frequencia">A frequência de repetição</param>
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
