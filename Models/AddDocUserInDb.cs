using System.Data.SqlClient;
using RegisterUser;

namespace AddDocUserInDb
{
    public static class AddUserInDb
    {

        public static void InsertAuto(outUser forInsert)
        {
            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            string sqlCommandString = @"Insert into UserDocument(Name, LastName, MiddleName, Gender, Nationaly, MartialStatus, BirthDay, IdPass) "
                + "Values(@Name, @LastName, @MiddleName, @Gender, @Nationaly, @MartialStatus, @BirthDay, @IdPass)";
            mssql.Open();
            using (SqlCommand cmd = new SqlCommand(sqlCommandString, mssql))
            {
                cmd.Parameters.AddWithValue("@Name", forInsert.name);
                cmd.Parameters.AddWithValue("@LastName", forInsert.lastName);
                cmd.Parameters.AddWithValue("@MiddleName", forInsert.middleName);
                cmd.Parameters.AddWithValue("@Gender", forInsert.gender);
                cmd.Parameters.AddWithValue("@Nationaly", forInsert.nationaly);
                cmd.Parameters.AddWithValue("@MartialStatus", forInsert.martialStatus);
                cmd.Parameters.AddWithValue("@BirthDay", forInsert.birthDay);
                cmd.Parameters.AddWithValue("@IdPass", forInsert.idPassport);

                cmd.ExecuteNonQuery();
            }
            string sqlCommandStringForAcc = @"Insert into UserAccaount(Login,Password) values(@Login,@Password)";
            using (SqlCommand newCmd = new SqlCommand(sqlCommandStringForAcc,mssql))
            {
                newCmd.Parameters.AddWithValue("@Login", forInsert.login);
                newCmd.Parameters.AddWithValue("@Password", forInsert.password);
                newCmd.ExecuteNonQuery();
            }
            mssql.Close();
        }

    }


}