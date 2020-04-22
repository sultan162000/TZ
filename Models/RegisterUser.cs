using System;
using System.Data.SqlClient;

namespace RegisterUser
{
        public class outUser
    {
        public string login{get;set;}
        public string password{get;set;}
        public string name { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public char gender { get; set; }
        public string nationaly { get; set; }
        public string birthDay { get; set; }
        public int martialStatus { get; set; }
        public string idPassport { get; set; }

        public outUser()
        {
                registerNewUser();
                if (idPassport != null)
                {
                    creatNewAcc();
                }
        }

        public outUser(string login, string password){
            this.login = login;
            this.password = password;
            inputInAcc(login,password);
        }

        public void showInfo()
        {
            System.Console.WriteLine("Логин: "+login);
            System.Console.WriteLine("Пароль: "+password);
            System.Console.WriteLine("Name: " + name);
            System.Console.WriteLine("Lastname: " + lastName);
            System.Console.WriteLine("MIddle: " + middleName);
            System.Console.WriteLine("Gender: " + gender);
            System.Console.WriteLine("National: " + nationaly);
            System.Console.WriteLine("Birthday: " + birthDay);
            System.Console.Write("Семейное положение: ");
            switch (martialStatus)
            {
                case 1:
                System.Console.WriteLine("Холостяк");
                break;
                case 2:
                System.Console.WriteLine("Семянин");
                break;
                case 3:
                System.Console.WriteLine("В разводе");
                break;
                case 4:
                System.Console.WriteLine("Вдова/Вдовец");
                break;
            }
            System.Console.WriteLine("idPassport: " + idPassport);
            
        }

        public void registerNewUser()
        {

            while (true)
            {
                System.Console.Write("Введите имя: ");
                this.name = Console.ReadLine();
                if (string.IsNullOrEmpty(name))
                {
                    System.Console.WriteLine("Поля не может быть пустой!");
                }
                else { break; }
            }


            while (true)
            {
                System.Console.Write("Введите фамилию: ");
                this.lastName = Console.ReadLine();
                if (string.IsNullOrEmpty(this.lastName))
                {
                    System.Console.WriteLine("Поля не может быть пустой!");
                }
                else
                {
                    break;
                }
            }

            System.Console.Write("Введите отчество(необязательно): ");
            this.middleName = Console.ReadLine();


            while (true)
            {
                System.Console.Write("Вaш пол(М/Ж): ");
                this.gender = char.Parse(Console.ReadLine());
                if (this.gender.ToString().ToUpper() == "M" || this.gender.ToString().ToUpper() == "F")
                {
                    break;
                }
                else System.Console.WriteLine("Неопознано! Попробуйте ещё.");
            }


            
            System.Console.Write("Birth Day:");
            this.birthDay = Console.ReadLine();

            do{
                System.Console.Write("Семейнее положение(1.Холостяк,2.Семянин,3.В разводе,4.Вдова/Вдовец): ");
                this.martialStatus = int.Parse(Console.ReadLine());
            }while(martialStatus != 1 || martialStatus != 2 || martialStatus != 3 || martialStatus != 4);
            

            System.Console.Write("Nationaly: ");
            this.nationaly = Console.ReadLine();
            if(nationaly.ToLower() == "tjk" || nationaly.ToLower() == "tajikistan"){
                nationaly = "TJK";
            }
            

            System.Console.Write("Document ID ");
            if (this.nationaly == "TJK")
            {


                System.Console.Write("\nВведите ID Passport: ");
                while (true)
                {
                    idPassport = Console.ReadLine();

                    if ((idPassport.ToCharArray()).Length == 9 && idPassport[0] == 'A')
                    {
                        break;
                    }
                    else System.Console.WriteLine("Попробуйте ещё!");
                }
            }
            else
            {
                idPassport = Console.ReadLine();
            }

        }

        public void creatNewAcc(){

            System.Console.Write("Введите номер телефона(логин), без международного формата: ");
            while (true)
            {
                login = Console.ReadLine();

                if(Verifications.RegUser.verificationUser(login)){
                    System.Console.WriteLine("Такой номер уже существует!");
                    while (true)
                    {
                        System.Console.Write("Попробуйте ещё: ");
                        login=Console.ReadLine();
                        if (Verifications.RegUser.verificationUser(login))
                        {
                            System.Console.WriteLine("Такой номер уже существует!");
                        }else {
                            break;
                        }
                    }
                    
                }
                if(login.Length == 9){
                    break;
                }else{
                    System.Console.WriteLine("Неверный номер!");
                }
                System.Console.Write("Попробуйте ещё.\nВведите номер: ");
            }

            System.Console.Write("Введите пароль(не менее 5 символов): ");
            while (true)
            {
                password = Console.ReadLine();
                if(password.Length<=5){
                    System.Console.Write("Короткий пароль. \nПопробуйте ещё: ");
                }
                else {
                    break;
                }
            }
        }

        public void inputInAcc(string login, string pass){
            SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
            string comStr = $"select Name from UserDocument, UserAccaount where UserAccaount.Login='{login}'";
            mssql.Open();
            using (SqlCommand cmd = new SqlCommand(comStr,mssql))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows){
                    while(reader.Read()){
                        this.name = (string)reader["Name"];
                    }
                }
                reader.Close();
            }
            comStr = $"select LastName from UserDocument, UserAccaount where UserAccaount.Login='{login}'";
            using (SqlCommand cmd = new SqlCommand(comStr,mssql))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows){
                    while(reader.Read()){
                        this.lastName = (string)reader["LastName"];
                    }
                }
                reader.Close();
            }

            comStr = $"select MiddleName from UserDocument, UserAccaount where UserAccaount.Login='{login}'";
            using (SqlCommand cmd = new SqlCommand(comStr,mssql))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows){
                    while(reader.Read()){
                        this.middleName = (string)reader["MiddleName"];
                    }
                }
                reader.Close();
            }

            comStr = $"select Gender from UserDocument, UserAccaount where UserAccaount.Login='{login}'";
            using (SqlCommand cmd = new SqlCommand(comStr,mssql))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows){
                    while(reader.Read()){
                        this.gender = (Convert.ToChar(reader["Gender"]));
                    }
                }
                reader.Close();
            }

            comStr = $"select Nationaly from UserDocument, UserAccaount where UserAccaount.Login='{login}'";
            using (SqlCommand cmd = new SqlCommand(comStr,mssql))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows){
                    while(reader.Read()){
                        this.nationaly = (string)reader["Nationaly"];
                    }
                }
                reader.Close();
            }

            comStr = $"select BirthDay from UserDocument, UserAccaount where UserAccaount.Login='{login}'";
            using (SqlCommand cmd = new SqlCommand(comStr,mssql))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows){
                    while(reader.Read()){
                        this.birthDay = Convert.ToString(reader["BirthDay"]);
                    }
                }
                reader.Close();
            }

            comStr = $"select MartialStatus from UserDocument, UserAccaount where UserAccaount.Login='{login}'";
            using (SqlCommand cmd = new SqlCommand(comStr,mssql))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows){
                    while(reader.Read()){
                        this.martialStatus = (int)reader["MartialStatus"];
                    }
                }
                reader.Close();
            }

            comStr = $"select IdPass from UserDocument, UserAccaount where UserAccaount.Login='{login}'";
            using (SqlCommand cmd = new SqlCommand(comStr,mssql))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows){
                    while(reader.Read()){
                        this.idPassport = Convert.ToString(reader["IdPass"]);
                    }
                }
                reader.Close();
            }

            

        }



    }
}