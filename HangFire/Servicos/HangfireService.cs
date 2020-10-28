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
    public class HangfireService
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

        public void ExecutarUmaVez<T>(Action funcao) where T : IBaseJob
        {
            var lambda = TransformarEmLambda<T>(funcao);
            BackgroundJob.Enqueue(lambda);
        }

        public void ExecutarRepetidamente<T>(Action funcao, TimeSpan tempo)
        {
            var lambda = TransformarEmLambda<T>(funcao);
            RecurringJob.AddOrUpdate(lambda, tempo.ToCronExpression());
        }

        public void ExecutarRepetidamente<T>(Action funcao, EExecutarRepetidamente frequencia)
        {
            var time = TimeSpan.FromSeconds((int)frequencia);
            ExecutarRepetidamente<T>(funcao, time);
        }

        private Expression<Action> TransformarEmLambda<T>(Action funcao)
        {
            var testMethodInfo = (typeof(T)).GetMethod(funcao.Method.Name, BindingFlags.Public | BindingFlags.Instance);
            var instance = (T)Activator.CreateInstance(typeof(T));
            var i = Expression.Constant(instance);
            var exp = Expression.Call(i, testMethodInfo);

            var lambda = Expression.Lambda<Action>(exp);
            return lambda;
        }
    }
}
