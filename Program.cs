using System;
using RegisterUser;
using AddDocUserInDb;
using Auth;
using Anketa;

namespace TZ
{
    class Program
    {
        static void Main(string[] args)
        {

            bool active = false;

            bool checkTheUser = true;
            outUser newU;

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
                            if (autho.AuthLogin(login))
                            {
                                System.Console.Write("Введите пароль: ");
                                string pass = Console.ReadLine();
                                if (autho.AuthPass(pass))
                                {
                                    System.Console.WriteLine("Успешно!");
                                    active = true;
                                    newU = new outUser(login,pass);
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
                            AddUserInDb.InsertAuto(newU);
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
            

            if(active){
                System.Console.Write("1.Взять кредит\n:");
                switch(int.Parse(Console.ReadLine())){
                    case 1:
                    newAnketa anket = new newAnketa();
                    break;
                }
            }




            Console.ReadKey();


        }
    }
}
