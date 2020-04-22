using System.Data.SqlClient;
using RegisterUser;

namespace AddDocUserInDb
{
    public static class AddUserInDb
    {
        private static int schet = 1;

        public static void InsertAuto(outUser forInsert)
        {
            incrementSchet(ref schet);
            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            string sqlCommandString = @"Insert into UserDocument(IdDocUser,Name, LastName, MiddleName, Gender, Nationaly, MartialStatus, BirthDay, IdPass) "
                + "Values(@IdDocUser,@Name, @LastName, @MiddleName, @Gender, @Nationaly, @MartialStatus, @BirthDay, @IdPass)";
            mssql.Open();
            using (SqlCommand cmd = new SqlCommand(sqlCommandString, mssql))
            {
                cmd.Parameters.AddWithValue("@IdDocUser", schet);
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
            string sqlCommandStringForAcc = @"Insert into UserAccaount(IdDoc,Login,Password) values(@IdDoc,@Login,@Password)";
            using (SqlCommand newCmd = new SqlCommand(sqlCommandStringForAcc,mssql))
            {
                newCmd.Parameters.AddWithValue("@IdDoc", schet);
                newCmd.Parameters.AddWithValue("@Login", forInsert.login);
                newCmd.Parameters.AddWithValue("@Password", forInsert.password);
                newCmd.ExecuteNonQuery();
            }
            mssql.Close();
            schet++;
        }

        private static void incrementSchet(ref int a){
            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            string sqlCommandString = "Select MAX(IdDocUser) from UserDocument";
            mssql.Open();
            using (SqlCommand cmd = new SqlCommand(sqlCommandString, mssql))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows){
                    while (reader.Read())
                    {
                        a = (int)reader.GetValue(0);
                    }
                }

            }
            
            a++;
            mssql.Close();

        }

    }


}