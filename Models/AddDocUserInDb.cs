using System.Data.SqlClient;
using RegisterUser;
using System;
using System.Data;

namespace AddDocUserInDb
{
    public static class AddUserInDb
    {

        public static void InsertAuto(outUser forInsert)
        {

            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            string sqlCommandString = @"Insert into UserDocument(Name, LastName, MiddleName, Gender, Nationaly, MartialStatus, BirthDay, IdPass) "
                + "Values(@Name, @LastName, @MiddleName, @Gender, @Nationaly, @MartialStatus, @BirthDay, @IdPass)";
            string sqlCommandStringForAcc = @"Insert into UserAccaount(Login,Password,IdDoc) values(@Login,@Password,@IdDoc)";


            SqlTransaction trans = null;
            SqlCommand cmd = new SqlCommand(sqlCommandString, mssql);
            mssql.Open();
            try
            {

                trans = mssql.BeginTransaction();
                cmd.Parameters.AddWithValue("@Name", forInsert.name);
                cmd.Parameters.AddWithValue("@LastName", forInsert.lastName);
                cmd.Parameters.AddWithValue("@MiddleName", forInsert.middleName);
                cmd.Parameters.AddWithValue("@Gender", forInsert.gender);
                cmd.Parameters.AddWithValue("@Nationaly", forInsert.nationaly);
                cmd.Parameters.AddWithValue("@MartialStatus", forInsert.martialStatus);
                cmd.Parameters.AddWithValue("@BirthDay", forInsert.birthDay);
                cmd.Parameters.AddWithValue("@IdPass", forInsert.idPassport);


                cmd.Transaction = trans;

                cmd.ExecuteNonQuery();
                trans.Commit();

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                trans.Rollback();
                mssql.Close();
            }

            if (mssql.State == ConnectionState.Open)
            {
                cmd = new SqlCommand(sqlCommandStringForAcc, mssql);

                cmd.Parameters.AddWithValue("@Login", forInsert.login);
                cmd.Parameters.AddWithValue("@Password", forInsert.password);
                int idForDoc = chetTheIdDoc.returnIdDoc(forInsert.idPassport);
                if (idForDoc != -1)
                {
                    cmd.Parameters.AddWithValue("@IdDoc", idForDoc);
                }
                else
                {
                    return;
                }
                cmd.ExecuteNonQuery();
                System.Console.WriteLine("Пользователь добавлен в БД.");
                
                mssql.Close();
            }else{
                System.Console.WriteLine("Error! Данные не добавленно");
            }



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