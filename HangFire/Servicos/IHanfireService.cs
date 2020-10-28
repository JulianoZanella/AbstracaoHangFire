using HangFire.RN.Enums;
using HangFire.RN.Models;
using System;

namespace HangFire.RN.Servicos
{
    public interface IHanfireService
    {
        void ExecutarUmaVez<T>(Action funcao) where T : IBaseJob;

        void ExecutarRepetidamente<T>(Action funcao, TimeSpan tempo) where T : IBaseJob;

        void ExecutarRepetidamente<T>(Action funcao, EExecutarRepetidamente frequencia) where T : IBaseJob;
    }
}
