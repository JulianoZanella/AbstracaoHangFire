using HangFire.RN.Enums;
using System;

namespace HangFire.RN.Servicos
{
    public interface IHanfireService
    {
        void ExecutarUmaVez(Action funcao);

        void ExecutarRepetidamente(Action funcao, TimeSpan tempo);

        void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia);
    }
}
