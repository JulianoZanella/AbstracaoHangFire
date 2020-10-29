using Hangfire.RN.Tarefas;
using HangFire.RN.Entidades;
using HangFire.RN.Repositorios;
using System;

namespace Hangfire.Servicos.Tarefas
{
    public class PessoaTarefa : TarefaBase
    {
        private readonly PessoaRepositorio _repositorio;

        public PessoaTarefa()
        {
            _repositorio = new PessoaRepositorio();
        }

        public override void Rodar()
        {
            ExecutarRepetidamente(IncluirPessoa, TimeSpan.FromMinutes(5));
        }

        public void IncluirPessoa()
        {
            var pessoa = new Pessoa { Nome = "Peixe", Sobrenome = "Moribundo" };
            _repositorio.Gravar(pessoa, out bool ok);
        }
    }
}
