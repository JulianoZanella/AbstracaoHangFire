using HangFire.RN.Enums;
using System;

namespace HangFire.RN.Tarefas
{
    public interface ITarefaBase
    {
        void Rodar();
        void ExecutarUmaVez(Action funcao);
        void ExecutarRepetidamente(Action funcao, TimeSpan tempo);
        void ExecutarRepetidamente(Action funcao, EExecutarRepetidamente frequencia);
    }
}
