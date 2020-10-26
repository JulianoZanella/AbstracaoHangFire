using Hangfire;
using Hangfire.SqlServer;
using HangFire.Extensions;
using HangFire.RN.Commom;
using HangFire.RN.Enums;
using System;
using System.Linq.Expressions;

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

        public static void ExecutarUmaVez(Action funcao)
        {
            BackgroundJob.Enqueue(() => funcao.Invoke());
        }

        public static void ExecutarRepetidamente(Action funcao, TimeSpan tempo)
        {
            Expression<Action> expressao = () => funcao.Invoke();
            RecurringJob.AddOrUpdate(expressao, tempo.ToCronExpression());
        }

        public static void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia)
        {
            var time = TimeSpan.FromSeconds((int)frequencia);
            ExecutarRepetidamente(funcao, time);
        }
    }
}
