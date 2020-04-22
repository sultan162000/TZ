using System.Data.SqlClient;
using RegisterUser;
using System;

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
            SqlCommand cmd = new SqlCommand(sqlCommandString, mssql);



            cmd.Parameters.AddWithValue("@Name", forInsert.name);
            cmd.Parameters.AddWithValue("@LastName", forInsert.lastName);
            cmd.Parameters.AddWithValue("@MiddleName", forInsert.middleName);
            cmd.Parameters.AddWithValue("@Gender", forInsert.gender);
            cmd.Parameters.AddWithValue("@Nationaly", forInsert.nationaly);
            cmd.Parameters.AddWithValue("@MartialStatus", forInsert.martialStatus);
            cmd.Parameters.AddWithValue("@BirthDay", forInsert.birthDay);
            cmd.Parameters.AddWithValue("@IdPass", forInsert.idPassport);

            cmd.ExecuteNonQuery();
            string sqlCommandStringForAcc = @"Insert into UserAccaount(Login,Password,IdDoc,IdCredit) values(@Login,@Password,@IdDoc,@IdCredit)";
            cmd = new SqlCommand(sqlCommandStringForAcc, mssql);

            cmd.Parameters.AddWithValue("@Login", forInsert.login);
            cmd.Parameters.AddWithValue("@Password", forInsert.password);
            int idForDoc = chetTheIdDoc.returnIdDoc(forInsert.idPassport);
            if (idForDoc != -1)
            {
                cmd.Parameters.AddWithValue("@IdDoc", idForDoc);
            }

            cmd.Parameters.AddWithValue("@IdCredit", 1);
            cmd.ExecuteNonQuery();
            System.Console.WriteLine("Ok!");


            mssql.Close();
        }


    }

    public static class chetTheIdDoc
    {
        public static int returnIdDoc(string number)
        {
            string newInser = $"select IdDoc from UserDocument where IdPass='{number}'";
            SqlConnection msqql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            msqql.Open();
            SqlCommand cmds = new SqlCommand(newInser, msqql);
            
            int idForDoc = -1;
            SqlDataReader read = cmds.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    idForDoc = read.GetInt32(0);
                }
            }
            return idForDoc;
        }
    }


}