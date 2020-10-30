using HangFire.RN.Commom;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HangFire.RN.Data
{
    public class ConexaoSqlServer
    {
        private SqlConnection _sqlConnection;
        public SqlConnection SqlConnection
        {
            get
            {
                if (_sqlConnection == null)
                {
                    var connectionString = Util.BuscarConnectionStringApp();
                    _sqlConnection = new SqlConnection(connectionString);
                }
                return _sqlConnection;
            }
        }

        private SqlTransaction sqlTransaction = null;
        public SqlTransaction BeginTransaction()
        {
            sqlTransaction = SqlConnection.BeginTransaction();
            return sqlTransaction;
        }

        public void Commit()
        {
            sqlTransaction?.Commit();
        }

        public void Rollback()
        {
            sqlTransaction?.Rollback();
        }

        public int ExecuteNonQuery(string sql, IEnumerable<SqlParameter> parametros = null)
        {
            using (var conn = SqlConnection)
            {
                using (var command = CriarComando(conn, sql, parametros))
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int ExecuteNonQuery(string sql, SqlConnection con)
        {
            return ExecuteNonQuery(sql, null, con);
        }
        public int ExecuteNonQuery(string sql, IEnumerable<SqlParameter> parametros, SqlConnection con)
        {
            using (var command = CriarComando(con, sql, parametros))
            {
                return command.ExecuteNonQuery();
            }
        }

        public void ExecuteReader(string sql, Action<SqlDataReader> readerAction)
        {
            ExecuteReader(sql, null, readerAction);
        }
        public void ExecuteReader(string sql, IEnumerable<SqlParameter> parametros, Action<SqlDataReader> readerAction)
        {
            using (var con = SqlConnection)
            {
                using (var command = CriarComando(con, sql, parametros))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        readerAction(reader);
                    }
                }
            }
        }

        public void ExecuteReader(string sql, Action<SqlDataReader> readerAction, SqlConnection con)
        {
            ExecuteReader(sql, null, readerAction, con);
        }
        public void ExecuteReader(string sql, IEnumerable<SqlParameter> parametros, Action<SqlDataReader> readerAction, SqlConnection con)
        {
            using (var command = CriarComando(con, sql, parametros))
            {
                using (var reader = command.ExecuteReader())
                {
                    readerAction(reader);
                }
            }
        }

        public T ExecuteScalar<T>(string sql, IEnumerable<SqlParameter> parametros = null)
        {
            using (var con = SqlConnection)
            {
                using (var command = CriarComando(con, sql, parametros))
                {
                    return (T)command.ExecuteScalar();
                }
            }
        }

        public T ExecuteScalar<T>(string sql, SqlConnection con)
        {
            return ExecuteScalar<T>(sql, null, con);
        }
        public T ExecuteScalar<T>(string sql, List<SqlParameter> parametros, SqlConnection con)
        {
            using (var command = CriarComando(con, sql, parametros))
            {
                return (T)command.ExecuteScalar();
            }
        }

        private SqlCommand CriarComando(SqlConnection conn, string sql, IEnumerable<SqlParameter> parametros)
        {
            var command = new SqlCommand(sql, conn)
            {
                CommandTimeout = 0
            };
            if (sqlTransaction != null)
            {
                command.Transaction = sqlTransaction;
            }
            if (parametros != null)
            {
                foreach (var parametro in parametros)
                {
                    command.Parameters.Add(parametro);
                }
            }
            return command;
        }
    }
}
