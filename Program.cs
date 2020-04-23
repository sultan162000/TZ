using System;
using RegisterUser;
using AddDocUserInDb;
using Auth;
using Anketa;
using System.Data.SqlClient;

namespace TZ
{
    class Program
    {
        static void Main(string[] args)
        {

            bool active = false;
            bool adminActive = false;

            bool checkTheUser = true;
            outUser newU = null;

            while (checkTheUser)
            {
                if (active)
                {
                    checkTheUser = false;
                    break;
                }
                int checkUser = 0;
                do
                {
                    System.Console.Write("1.Вход\n2.Регистрация\n:");
                    checkUser = int.Parse(Console.ReadLine());

                } while (!(checkUser == 1) && !(checkUser == 2));

                switch (checkUser)
                {
                    case 1:
                        InputSystem autho = new InputSystem();
                        while (true)
                        {
                            System.Console.Write("Введите логин(Номер телефона): ");
                            string login = Console.ReadLine();
                            if (login == "7711112")
                            {
                                Console.Write("Введите пароль:");
                                if (Console.ReadLine() == "parol1111")
                                {
                                    adminActive = true;
                                    checkTheUser = false;
                                    break;
                                }
                            }
                            if (autho.AuthLogin(login))
                            {
                                System.Console.Write("Введите пароль: ");
                                string pass = Console.ReadLine();
                                if (autho.AuthPass(login, pass))
                                {
                                    System.Console.WriteLine("Успешно!");
                                    active = true;
                                    newU = new outUser(login, pass);
                                    break;
                                }
                                else
                                {
                                    System.Console.WriteLine("Неверный пароль. Хотите попробовать ещё?\n1.Yes\n2.No");
                                    if (int.Parse(Console.ReadLine()) == 2) break;
                                }
                            }
                            else
                            {
                                System.Console.WriteLine("Неверный логин. Хотите попробовать ещё?\n1.Yes\n2.No");
                                if (int.Parse(Console.ReadLine()) == 2) break;
                            }
                        }


                        break;
                    case 2:
                        newU = new outUser();
                        if (Verifications.RegUser.verificationUser(newU.idPassport))
                        {
                            System.Console.WriteLine("У вас уже есть акк.");
                            break;
                        }
                        else
                        {
                            try
                            {
                                AddUserInDb.InsertAuto(newU);
                            }
                            catch (Exception ex)
                            {
                                System.Console.WriteLine(ex.Message);
                                break;
                            }

                        }
                        if (!(newU is null))
                        {
                            System.Console.WriteLine("Поздравляем с успешной регистрации!");
                            active = true;
                        }
                        break;
                    default:
                        System.Console.WriteLine("Error!");
                        break;
                }
            }
            if (adminActive)
            {

            }


            if (active)
            {
                bool checkWhile = true;
                while (checkWhile)
                {
                    System.Console.Write("1.Взять кредит\n2.Показат кредит\n:");
                    switch (int.Parse(Console.ReadLine()))
                    {
                        case 1:
                            newAnketa anket = new newAnketa(newU);
                            break;
                        case 2:
                            newAnketa showAnk = new newAnketa(newU.idPassport);
                            break;
                        case 3:
                            checkWhile = false;
                            break;
                    }
                }
            }
            


        }
    }
}
