using HangFire.RN.Commom;
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


    }
}
