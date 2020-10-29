using Hangfire.Servicos.Tarefas;
using HangFire.RN.Servicos;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HangFire.Tests.Tarefas
{
    [TestClass]
    public class PessoaTarefaTests
    {
        
        public PessoaTarefaTests()
        {
            HangfireService.InicializaHangfire();            
        }

        [TestMethod]
        public void SucessoRodarATarefa()
        {
            new PessoaTarefa().Rodar();
        }
    }
}
