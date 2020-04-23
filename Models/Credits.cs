using System.Data.SqlClient;
using RegisterUser;

namespace Credit
{
    public static class Credits
    {
        public static bool haveAactuallCredit(string numberPassport){
            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            string sqlCommand = $"select Active from HistoryCredit h, Credits c, UserAccaount u, UserDocument d where u.IdDoc=d.IdDoc and u.IdCredit = h.IdCredit and d.IdPass ='{numberPassport}' and h.Active ='yes'";
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
            string sqlCommand = $"select Active from HistoryCredit h, Credits c, UserAccaount u, UserDocument d where u.IdDoc=d.IdDoc and u.IdCredit = h.IdCredit and d.IdPass ='{numberPassport}' and h.Active ='no'";
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

        public static void verificationTheReitingForAdd(int reiting,int summCredit, int creditsFor,int srokCredit, outUser forReint){
            if(reiting>11){

            }else{
                System.Console.WriteLine("Извините но мы не можем вам дать кредить т.к вы не соотвествуете правилам!");
            }
        }

        
    }
}