using System.Data.SqlClient;
using RegisterUser;
using Anketa;
using System;

namespace ActualCredits
{
    public class ActualCredit
    {
        public void addToFailed(newAnketa ank,string numberPass)
        {
            if(ank.beginCredint.ToString("dd-MM HH:mm:ss")==DateTime.Now.ToString("dd-MM HH:mm:ss")&&ank.endCredit == DateTime.Now){
                SqlConnection connection = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
                string updateHistory = $"update HistoryCredit set Active='no',Failed=3";
                
            }
        }


    }
}