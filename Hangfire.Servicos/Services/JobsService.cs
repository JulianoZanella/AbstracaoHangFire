using Hangfire.Servicos.Jobs;
using HangFire.RN.Servicos;

namespace Hangfire.Servicos.Services
{
    /// <summary>
    /// Classe onde devem ser iniciados os Jobs.
    /// </summary>
    public static class JobsService
    {
        /// <summary>
        /// Método responsável por instanciar os Jobs e colocar em execução.
        /// </summary>
        public static void Rodar()
        {
            HangfireService.InicializaHangfire();
            new AtualizaRecargaProdutoCacheJob().Rodar();
        }
    }
}
