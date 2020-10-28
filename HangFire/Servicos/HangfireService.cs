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

        public void ExecutarUmaVez(Action funcao) 
        {            
            var lambda = TransformarEmLambda(funcao);
            BackgroundJob.Enqueue(lambda);
        }

        public void ExecutarRepetidamente(Action funcao, TimeSpan tempo)
        {
            var lambda = TransformarEmLambda(funcao);
            RecurringJob.AddOrUpdate(lambda, tempo.ToCronExpression());
        }

        public void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia) 
        {
            var time = TimeSpan.FromSeconds((int)frequencia);
            ExecutarRepetidamente(funcao, time);
        }

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
