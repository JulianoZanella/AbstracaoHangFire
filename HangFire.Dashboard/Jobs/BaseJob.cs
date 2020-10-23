using HangFire.RN.Enums;
using System;

namespace HangFire.Dashboard.Jobs
{
    public abstract class BaseJob
    {
        protected void ExecutarUmaVez(Action funcao)
        {
            HangfireService.ExecutarUmaVez(funcao);
        }

        protected void ExecutarRepetidamente(Action funcao, TimeSpan tempo)
        {
            HangfireService.ExecutarRepetidamente(funcao, tempo);
        }

        protected void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia)
        {
            HangfireService.ExecutarRepetidamente(funcao, frequencia);
        }
    }
}
