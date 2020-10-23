using HangFire.Dashboard.Jobs;

namespace HangFire.Dashboard.Services
{
    public static class JobsService
    {
        public static void Rodar()
        {
            new AtualizaRecargaProdutoCacheJob().Rodar();
        }
    }
}
