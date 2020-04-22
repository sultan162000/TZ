using System.Data.SqlClient;

namespace Credit
{
    public static class Credits
    {
        public static bool haveAactuallCredit(string numberPassport){
            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            string sqlCommand = $"select Active from HistoryCredit h, Credits c, UserAccaount u, UserDocument d where u.IdDoc=d.IdDoc and u.IdCredit = h.IdCredit and d.IdPass ='{numberPassport}'";
            mssql.Open();
            using (SqlCommand command = new SqlCommand(sqlCommand,mssql))
            {
                SqlDataReader read = command.ExecuteReader();
                
                    while (read.Read())
                    {
                        return true;
                    }
                
                read.Close();
            }
            mssql.Close();
            return false;
            
        }

        public static int haveAnotActualCredit(string numberPassport){
            int count = 0;
            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            string sqlCommand = $"SELECT Active FROM UserDocument, UserAccaount, Credits, HistoryCredit";
            mssql.Open();
            using (SqlCommand command = new SqlCommand(sqlCommand,mssql))
            {
                SqlDataReader read = command.ExecuteReader();
                if(read.HasRows){
                    while (read.Read())
                    {
                        count++;
                    }
                }
                read.Close();
            }
            mssql.Close();
            return count;
            
        }
    }
}