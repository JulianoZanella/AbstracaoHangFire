using Hangfire.Servicos.Services;
using System;

namespace Hangfire.Servicos
{
    public class Program
    {
        static void Main(string[] args)
        {
            JobsService.Rodar();
            Console.ReadLine();
        }
    }
}
