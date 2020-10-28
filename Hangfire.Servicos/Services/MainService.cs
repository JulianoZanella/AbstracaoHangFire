using Hangfire.Servicos.Jobs;
using HangFire.RN.Servicos;
using System;

namespace Hangfire.Servicos.Services
{
    /// <summary>
    /// Classe onde devem ser iniciados os Jobs.
    /// </summary>
    public class MainService
    {
        /// <summary>
        /// Método responsável por instanciar os Jobs e colocar em execução.
        /// </summary>
        public static void Rodar()
        {
            HangfireService.InicializaHangfire();
            new AtualizaRecargaProdutoCacheJob().Rodar();
        }

        /// <summary>
        /// Método chamado ao Iniciar o Serviço no windows
        /// </summary>
        public void Iniciar()
        {
            Console.WriteLine("Iniciando serviço");
            Rodar();
            Console.WriteLine("Serviços hangfire rodando");
        }

        /// <summary>
        /// Método chamado ao parar o serviço no Windows
        /// </summary>
        public void Parar()
        {
        }
    }
}
