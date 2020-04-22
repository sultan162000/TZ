using System.Data.SqlClient;

namespace Credit
{
    public static class Credits
    {
        public static bool haveAactuallCredit(string numberPassport){
            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            string sqlCommand = $"SELECT Active FROM UserDocument, UserAccaount, Credits, HistoryCredit where  HistoryCredit.IdCredit=Credits.IdCredit and Credits.IdAccs=UserAccaount.IdAcc and UserAccaount.IdDoc = UserDocument.IdDocUser and Active='yes' and UserDocument.IdPass='{numberPassport}'";
            mssql.Open();
            using (SqlCommand command = new SqlCommand(sqlCommand,mssql))
            {
                SqlDataReader read = command.ExecuteReader();
                if(read.HasRows){
                    while (read.Read())
                    {
                        return true;
                    }
                }
                read.Close();
            }
            mssql.Close();
            return false;
            
        }

        public static int haveAnotActualCredit(string numberPassport){
            int count = 0;
            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            string sqlCommand = $"SELECT Active FROM UserDocument, UserAccaount, Credits, HistoryCredit where  HistoryCredit.IdCredit=Credits.IdCredit and Credits.IdAccs=UserAccaount.IdAcc and UserAccaount.IdDoc = UserDocument.IdDocUser and Active='no' and UserDocument.IdPass='{numberPassport}'";
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