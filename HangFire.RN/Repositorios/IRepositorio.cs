using System.Collections.Generic;
using System.Data.SqlClient;

namespace HangFire.RN.Repositorios
{
    public interface IRepositorio<T>
    {
        bool Gravar(string sql, out long id, T obj);
        bool Gravar(string sql, out long id, T obj, SqlTransaction transacao);
        T Buscar(string sql, object filtro = null);
        T Buscar(string sql, SqlTransaction transacao, object filtro = null);
        IEnumerable<T> Listar(string sql, object filtro = null);
        IEnumerable<T> Listar(string sql, SqlTransaction transtransacaoaction, object filtro = null);
        int Excluir(string sql, object filtro = null);
        int Apagar(string sql, SqlTransaction transacao, object filtro = null);
        R PegarUmValor<R>(string sql, object filtro = null);
        R PegarUmValor<R>(string sql, SqlTransaction transacao, object filtro = null);
    }
}
