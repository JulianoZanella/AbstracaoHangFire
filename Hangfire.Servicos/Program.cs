using Hangfire.Servicos.Services;
using HangFire.RN.Servicos;
using System;

namespace Hangfire.Servicos
{
    public class Program
    {
        static void Main(string[] args)
        {
            HangfireService.InicializaHangfire();
            JobsService.Rodar();
            Console.ReadLine();
        }
    }
}
