using Hangfire;
using Hangfire.SqlServer;
using HangFire.Extensions;
using HangFire.RN.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace HangFire
{
    public static class HangfireService
    {
        public static IEnumerable<IDisposable> Inicializar(string connectioString)
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectioString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                });
            yield return new BackgroundJobServer();
        }

        public static IServiceCollection ConfiguraHangfireMvc(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            services.AddHangfireServer();
            return services;
        }

        public static IApplicationBuilder ConfiguraHangfireDashboard(this IApplicationBuilder app)
        {
            app.UseHangfireDashboard();
            new BackgroundJobServer();
            return app;
        }

        public static void ExecutarUmaVez(Action funcao)
        {
            BackgroundJob.Enqueue(() => funcao());
        }

        public static void ExecutarRepetidamente(Action funcao, TimeSpan tempo)
        {
            RecurringJob.AddOrUpdate(() => funcao(), tempo.ToCronExpression());
        }

        public static void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia)
        {
            var time = TimeSpan.FromSeconds((int)frequencia);
            ExecutarRepetidamente(funcao, time);
        }
    }
}
