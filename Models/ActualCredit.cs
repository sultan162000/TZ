using System.Data.SqlClient;
using RegisterUser;
using Anketa;
using System;

namespace ActualCredits
{
    public class ActualCredit
    {
        public static void addToFailed(newAnketa ank,string numberPass)
        {
            if(ank.beginCredint.ToString("dd-MM HH:mm:ss")==DateTime.Now.ToString("dd-MM HH:mm:ss")&&ank.endCredit == DateTime.Now){
                SqlConnection connection = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
                int sizeFailed = checkFailed(numberPass);
                sizeFailed++;
                string updateHistory = $"update HistoryCredit set Active='no',Failed={sizeFailed} where HistoryCredit.IdCredit=Credits.IdCredit";
                connection.Open();
                using(SqlCommand cmd = new SqlCommand(updateHistory,connection)){
                    cmd.ExecuteNonQuery();
                }
                connection.Close();

            }
        }

        public static int checkFailed(string number){
            string valueFailed = $"select h.Failed from Credits c, UserDocument u, UserAccaount a, HistoryCredit h where u.IdDoc=a.IdDoc and c.IdCredit=a.IdCredit and u.IdPass='{number}'";
            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            mssql.Open();
            SqlCommand cmd = new SqlCommand(valueFailed,mssql);
            int count =0;
            SqlDataReader read = cmd.ExecuteReader();
            if(read.HasRows){
                while (read.Read())
                {
                    count = (int)read["Failed"];
                }
            }
            mssql.Close();
            return count;
        }


    }
}