using Topshelf;

namespace Hangfire.Servicos.Servicos
{
    public class ConfiguraService
    {
        /// <summary>
        /// Método que configua o Topshelf
        /// </summary>
        internal static void Configura()
        {
            HostFactory.Run(conf =>
            {
                conf.Service<MainService>(service =>
                {
                    service.ConstructUsing(s => new MainService());
                    service.WhenStarted(s => s.Iniciar());
                    service.WhenStopped(s => s.Parar());
                });
                conf.RunAsLocalService();//Conta que usa o serviço
                conf.SetDisplayName("HangfireTopshelf");
                conf.SetServiceName("HangfireTopshelf");
                conf.SetDescription("Serviço host para aplicações do hangfire");
            });
        }
    }
}
