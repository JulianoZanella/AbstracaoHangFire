using Dapper;
using HangFire.RN.Commom;
using HangFire.RN.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HangFire.RN.Repositorios
{
    public class RepositorioDapper<T> : IRepositorio<T>
    {
        public ConexaoSqlServer ConexaoSqlServer { get; private set; }
        private readonly int _commandTimeout = Configuracao.DapperCommandTimeout;

        public RepositorioDapper()
        {
            ConexaoSqlServer = new ConexaoSqlServer();
        }
        public bool Gravar(string sql, out long id, T obj)
        {
            id = ConexaoSqlServer.SqlConnection.QuerySingleOrDefault<long>(sql, obj, commandTimeout: _commandTimeout);
            if (id == 0) throw new Exception("Erro ao gravar no banco de dados!");
            return id != 0;
        }

        public bool Gravar(string sql, out long id, T obj, SqlTransaction transacao)
        {
            id = ConexaoSqlServer.SqlConnection.QueryFirstOrDefault<long>(sql, obj, transacao, commandTimeout: _commandTimeout);
            if (id == 0) throw new Exception("Erro ao gravar no banco de dados!");
            return id != 0;
        }

        public T Buscar(string sql, object filtro = null)
        {
            return ConexaoSqlServer.SqlConnection.QueryFirstOrDefault<T>(sql, filtro, commandTimeout: _commandTimeout);
        }

        public T Buscar(string sql, SqlTransaction transaction, object filtro = null)
        {
            return ConexaoSqlServer.SqlConnection.QueryFirstOrDefault<T>(sql, filtro, transaction, commandTimeout: _commandTimeout);
        }

        public IEnumerable<T> Listar(string sql, object filtro = null)
        {
            return ConexaoSqlServer.SqlConnection.Query<T>(sql, filtro, commandTimeout: _commandTimeout);
        }

        public IEnumerable<T> Listar(string sql, SqlTransaction transaction, object filtro = null)
        {
            return ConexaoSqlServer.SqlConnection.Query<T>(sql, filtro, transaction, commandTimeout: _commandTimeout);
        }

        public int Excluir(string sql, object filtro = null)
        {
            return ConexaoSqlServer.SqlConnection.Execute(sql, filtro, commandTimeout: _commandTimeout);
        }

        public int Apagar(string sql, SqlTransaction transaction, object filtro = null)
        {
            return ConexaoSqlServer.SqlConnection.Execute(sql, filtro, transaction, commandTimeout: _commandTimeout);
        }

        public R PegarUmValor<R>(string sql, object filtro = null)
        {
            return ConexaoSqlServer.SqlConnection.QueryFirstOrDefault<R>(sql, filtro, commandTimeout: _commandTimeout);
        }

        public R PegarUmValor<R>(string sql, SqlTransaction transaction, object filtro = null)
        {
            return ConexaoSqlServer.SqlConnection.QueryFirstOrDefault<R>(sql, filtro, transaction, commandTimeout: _commandTimeout);
        }
    }
}
