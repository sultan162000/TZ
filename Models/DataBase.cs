using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TZ.DataAccess;

namespace DataBase
{
    public class Db
    {
        public static List<string> selectTheIdPassOfDB()
        {
            SqlConnection mssql = new SqlConnection(DBsql.connectionString);
            List<string> idUser = new List<string>();
            mssql.Open();
            using (SqlCommand command = new SqlCommand("Select IdPass from UserDocument", mssql))
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        idUser.Add((string)reader["IdPass"]);
                    }
                }
            }
            mssql.Close();
            return idUser;
        }

        public static List<string> selectTheLogin(){
            SqlConnection mssql = new SqlConnection(DBsql.connectionString);
            List<string> loginUser = new List<string>();
            mssql.Open();
            using (SqlCommand command = new SqlCommand("Select Login from UserAccaount", mssql))
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        loginUser.Add((string)reader["Login"]);
                    }
                }
            }
            mssql.Close();
            return loginUser;
        }

        public static List<string> selectThePass(){
            SqlConnection mssql = new SqlConnection(DBsql.connectionString);
            List<string> loginUser = new List<string>();
            mssql.Open();
            using (SqlCommand command = new SqlCommand("Select Password from UserAccaount", mssql))
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        loginUser.Add((string)reader["Password"]);
                    }
                }
            }
            mssql.Close();
            return loginUser;
        }
        
    }


}