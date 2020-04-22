using System.Data.SqlClient;

namespace TZ.Models
{
    public  class Credits
    {
        public bool haveAactuallCredit(){
            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            string sqlCommand = "";
            
        }
    }
}