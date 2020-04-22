using System;
using RegisterUser;

namespace Anketa
{
    public class newAnketa{
        private int summ = 0;
        public int zarplata = 0;
        public int summCredit = 0;
        public int creditsFor = 0;
        public int srokCredit = 0;


        public newAnketa(string numberPassport,outUser newAcc){
            sumOfcredit(numberPassport,newAcc);
        }

        public void sumOfcredit(string numberPassport,outUser newAcc){
            if(newAcc.gender == 'm') summ += 1;
            else summ += 2;
            if(newAcc.martialStatus==1)summ += 1;
            else if(newAcc.martialStatus == 2) summ += 2;
            else if(newAcc.martialStatus == 3) summ +=1;
            else if(newAcc.martialStatus == 4) summ += 2;
            if(newAcc.nationaly == "TJK") summ += 1;
            DateTime ageUser = Convert.ToDateTime(newAcc.birthDay);
            int ageUser1 = 2020-ageUser.Year;
            System.Console.Write("Введите вашу ежемецячную зарплату: ");
            zarplata = int.Parse(Console.ReadLine());
            System.Console.WriteLine("Введите сумму кредита: ");
            summCredit = int.Parse(Console.ReadLine());
            System.Console.Write("Цель кредита(1.Бытовая техника,2.Ремонт,3.Телефон,4.Прочее): ");
            creditsFor = int.Parse(Console.ReadLine());
            if(creditsFor == 1)summ+=2;
            else if(creditsFor == 2)summ +=1;
            else if(creditsFor == 4)summ -= 1;
            System.Console.Write("Срок кредита в месяцах: ");
            srokCredit = int.Parse(Console.ReadLine());
            if(srokCredit >= 12)summ+=1;
            System.Console.WriteLine("Рейтинг "+summ);

        }
    }
}