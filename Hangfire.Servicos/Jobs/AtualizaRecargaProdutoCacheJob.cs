using Hangfire.RN.Models;
using System;

namespace Hangfire.Servicos.Jobs
{
    public class AtualizaRecargaProdutoCacheJob : BaseJob
    {
        public AtualizaRecargaProdutoCacheJob() : base(true)
        {
        }

        public override void Rodar()
        {
            ExecutarUmaVez(Executar);
            ExecutarRepetidamente(Executar, TimeSpan.FromMinutes(5));
        }

        public void Executar()
        {
            var horaInicial = DateTime.Now;
            Log("Iniciando atualização de Operadoras e Produtos");
            Log("Iniciando RecargaService");
            try
            {
                //var service = new RecargaService(BuscarConfiguracao());
                //Log("Iniciando Atualização das Operadoras de Recarga");
                //int itens = service.AtualizarOperadorasEProdutosEpay();
                var horaFinal = DateTime.Now;
                var tempoDecorrido = horaFinal - horaInicial;
                Log(string.Format("Fim da atualização das tabelas, tempo decorrido: {0}", tempoDecorrido.TotalSeconds));
                //Log(string.Format("Registros atualizados: {0}", itens));
            }
            catch (Exception ex)
            {
                LogException(ex, "AtualizaRecargaProdutoCacheJob.Executar()");
                Log("Fim da execução: com erros.");
                //EnviarEmail();
                //GravarEmArquivo();
            }

        }
    }
}
