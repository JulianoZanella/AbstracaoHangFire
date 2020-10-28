using Hangfire;
using Hangfire.SqlServer;
using HangFire.Extensions;
using HangFire.RN.Commom;
using HangFire.RN.Enums;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace HangFire.RN.Servicos
{
    public class HangfireService : IHanfireService
    {
        /// <summary>
        /// Faz a inicialização do servidor Hangfire
        /// </summary>
        public static void InicializaHangfire()
        {
            GlobalConfiguration.Configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(Util.BuscarConnectionStringHanfire(), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            });
        }

        /// <summary>
        /// Método para adicionar um serviço que executa uma única vez no hangfire
        /// </summary>
        /// <param name="funcao">Action que deve executar</param>
        public void ExecutarUmaVez(Action funcao) 
        {            
            var lambda = TransformarEmLambda(funcao);
            BackgroundJob.Enqueue(lambda);
        }

        /// <summary>
        /// Método que executa repetidamente eternamente
        /// </summary>
        /// <param name="funcao">Action que deve executar</param>
        /// <param name="tempo">O tempo em que ela deve reexecutar</param>
        public void ExecutarRepetidamente(Action funcao, TimeSpan tempo)
        {
            var lambda = TransformarEmLambda(funcao);
            RecurringJob.AddOrUpdate(lambda, tempo.ToCronExpression());
        }

        /// <summary>
        /// Método que executa repetidamente eternamente
        /// </summary>
        /// <param name="funcao">Action que deve executar</param>
        /// <param name="frequencia">A frequência de repetição</param>
        public void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia) 
        {
            var time = TimeSpan.FromSeconds((int)frequencia);
            ExecutarRepetidamente(funcao, time);
        }

        /// <summary>
        /// É necessário fazer desse jeito para o hangfire aceitar a reflection
        /// </summary>
        /// <param name="funcao">A action do método</param>
        /// <returns>Uma função lambda para o hangfire</returns>
        private Expression<Action> TransformarEmLambda(Action funcao)
        {
            var classe = funcao.GetMethodInfo().DeclaringType;
            var testMethodInfo = (classe).GetMethod(funcao.Method.Name, BindingFlags.Public | BindingFlags.Instance);
            var instance = Activator.CreateInstance(classe);
            var i = Expression.Constant(instance);
            var exp = Expression.Call(i, testMethodInfo);

            var lambda = Expression.Lambda<Action>(exp);
            return lambda;
        }
    }
}
