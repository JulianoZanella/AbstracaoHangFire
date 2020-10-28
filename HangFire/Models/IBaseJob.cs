using HangFire.RN.Enums;
using System;

namespace HangFire.RN.Models
{
    public interface IBaseJob
    {
        void Rodar();
        void ExecutarUmaVez(Action funcao);
        void ExecutarRepetidamente(Action funcao, TimeSpan tempo);
        void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia);
    }
}
