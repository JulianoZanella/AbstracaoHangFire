using Hangfire.Servicos.Jobs;

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
            new AtualizaRecargaProdutoCacheJob().Rodar();
        }
    }
}
