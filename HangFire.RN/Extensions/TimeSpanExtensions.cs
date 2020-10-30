using System;

namespace HangFire.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToCronExpression(this TimeSpan timeSpan)
        {
            var cron = " {0} {1} {2} {3} {4} {5} ";//segundo minuto hora diaDoMes mes diaDaSemana
            const string ALL = "*";
            var segundos = timeSpan.TotalSeconds;
            var minutos = timeSpan.TotalMinutes;
            var horas = timeSpan.TotalHours;
            var dias = timeSpan.TotalDays;
            if (dias > 1)
            {
                return string.Format(cron, "0", "0", "0", "*/" + Math.Truncate(dias), ALL, ALL);
            }
            if (horas > 1)
            {
                return string.Format(cron, "0", "0", "*/" + Math.Truncate(horas), ALL, ALL, ALL);
            }
            if (minutos > 1)
            {
                return string.Format(cron, "0", "*/" + Math.Truncate(minutos), ALL, ALL, ALL, ALL);
            }
            if (segundos > 1)
            {
                return string.Format(cron, "*/" + Math.Truncate(minutos), ALL, ALL, ALL, ALL, ALL);
            }
            return string.Format(cron, ALL, ALL, ALL, ALL, ALL, ALL);
        }
    }
}
