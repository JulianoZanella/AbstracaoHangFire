using HangFire.RN.Enums;
using System;

namespace HangFire.RN.Servicos
{
    public interface IBaseJob
    {
        void Rodar();
        void ExecutarUmaVez<T>(Action funcao) where T : IBaseJob;
        void ExecutarRepetidamente(Action funcao, TimeSpan tempo);
        void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia);
    }
}
