using System;
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
            if(reiting>10){
                SqlConnection connection = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
                string strConn = "insert into Credits(Sum, MonthCred, CreditFor, EndCreditData) values(@Sum,@MonthCred,@CreditFor,@EndCreditData)";
                connection.Open();
                DateTime dateEnd = DateTime.Now;
                string EndCreditData = Convert.ToString(dateEnd.AddMonths(srokCredit));
                using(SqlCommand cmd = new SqlCommand(strConn, connection)){
                    cmd.Parameters.AddWithValue("@Sum",summCredit);
                    cmd.Parameters.AddWithValue("@MonthCred",DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreditFor", creditsFor);
                    cmd.Parameters.AddWithValue("@EndCreditData",EndCreditData);

                    cmd.ExecuteNonQuery();
                }

                string slectTheMax = "Select MAX(IdCredit) from Credits";
                int idDocs =0;
                using (SqlCommand cmd = new SqlCommand(slectTheMax,connection))
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        idDocs = (int)read.GetValue(0);
                    }
                    read.Close();
                }
                string updateTheAcc = $"update UserAccaount set UserAccaount.IdCredit = {idDocs} where UserAccaount.Login='{forReint.login}'";
                using (SqlCommand cmd = new SqlCommand(updateTheAcc,connection))
                {
                    cmd.ExecuteNonQuery();
                }
                string updateHistory = $"update HistoryCredit set IdCredit={idDocs}, Active='yes',Failed=0";
                using (SqlCommand cmd = new SqlCommand(updateHistory,connection))
                {
                    cmd.ExecuteNonQuery();
                }

                System.Console.WriteLine("Теперь у вас есть кредит!");
                connection.Close();



            }else{
                System.Console.WriteLine("Извините но мы не можем вам дать кредить т.к вы не соотвествуете правилам!");
            }
        }

        
    }
}