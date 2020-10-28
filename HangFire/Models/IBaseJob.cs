using HangFire.RN.Enums;
using System;

namespace HangFire.RN.Models
{
    public interface IBaseJob
    {
        void Rodar();
        void ExecutarUmaVez<T>(Action funcao) where T : IBaseJob;
        void ExecutarRepetidamente<T>(Action funcao, TimeSpan tempo) where T : IBaseJob;
        void ExecutarRepetidamente<T>(Action funcao, EExecutarRepetidamente frequencia) where T : IBaseJob;
    }
}
