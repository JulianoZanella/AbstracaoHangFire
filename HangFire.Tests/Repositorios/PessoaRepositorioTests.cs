using HangFire.RN.Entidades;
using HangFire.RN.Repositorios;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace HangFire.Tests.Repositorios
{
    [TestClass]
    public class PessoaRepositorioTests
    {
        private readonly PessoaRepositorio _repositorio;

        public PessoaRepositorioTests()
        {
            _repositorio = new PessoaRepositorio();
        }

        [TestMethod]
        public void SucessoQuandoGravada()
        {
            var pessoa = new Pessoa { Nome = "TesteNome", Sobrenome = "TesteSobrenome" };
            _repositorio.Gravar(pessoa, out bool sucesso);
            Assert.IsTrue(sucesso);
        }

        [TestMethod]
        public void SucessoQuandoAlterada()
        {
            var pessoa = new Pessoa { Id = 1, Nome = "Baleia", Sobrenome = "Assassina" };
            _repositorio.Gravar(pessoa, out bool sucesso);
            Assert.IsTrue(sucesso);
        }

        [TestMethod]
        public void SucessoQuandoExcluida()
        {
            var pessoa = new Pessoa { Id = 2 };
            _repositorio.Excluir(pessoa);
        }

        [TestMethod]
        public void SucessoQuandoBusca()
        {
            var pessoa = _repositorio.Buscar(1);
            Assert.IsFalse(pessoa == null);
        }

        [TestMethod]
        public void SucessoQuandoLista()
        {
            var pessoas = _repositorio.Listar();
            Assert.IsTrue(pessoas.Any());
        }
    }
}
