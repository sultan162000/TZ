using System;
using System.Data.SqlClient;
using RegisterUser;

namespace Anketa
{
    public class newAnketa
    {
        private int summ = 0;
        private int zarplata = 0;
        private double summCredit = 0;
        private int creditsFor = 0;
        private int srokCredit = 0;

        private DateTime beginCredint;
        private DateTime endCredit;
        private short historCredit = 0;


        public newAnketa(outUser newAcc)
        {
            sumOfcredit(newAcc);
        }

        public int sumOfcredit(outUser newAcc)
        {
            if (Credit.Credits.haveAactuallCredit(newAcc.idPassport))
            {
                System.Console.WriteLine("У вас уже есть кредит!");
                return -1;
            }
            else
            {
                if (Credit.Credits.haveAnotActualCredit(newAcc.idPassport) == 0)
                {

                }
                else if (Credit.Credits.haveAnotActualCredit(newAcc.idPassport) <= 2)
                {
                    summ += 1;
                }
                else if (Credit.Credits.haveAnotActualCredit(newAcc.idPassport) >= 3)
                {
                    summ += 2;
                }
            }

            if (newAcc.gender == 'm') summ += 1;
            else summ += 2;
            if (newAcc.martialStatus.ToLower() == "single") summ += 1;
            else if (newAcc.martialStatus.ToLower() == "family man") summ += 2;
            else if (newAcc.martialStatus.ToLower() == "divorced") summ += 1;
            else if (newAcc.martialStatus.ToLower() == "widow") summ += 2;
            if (newAcc.nationaly == "TJK") summ += 1;

            DateTime ageUser = Convert.ToDateTime(newAcc.birthDay);
            int ageUser1 = 2020 - ageUser.Year;
            if (ageUser1 > 25 && ageUser1 <= 35 && ageUser1 > 62) summ += 1;
            else if (ageUser1 > 35 && ageUser1 <= 62) summ += 2;


            System.Console.Write("Введите вашу ежемецячную зарплату: ");
            zarplata = int.Parse(Console.ReadLine());

            System.Console.WriteLine("Введите сумму кредита: ");
            summCredit = int.Parse(Console.ReadLine());

            int resultSumCredit = (int)((double)summCredit * 100 / zarplata);
            if (resultSumCredit < 80) summ += 4;
            else if (resultSumCredit >= 80 && resultSumCredit < 150) summ += 3;
            else if (resultSumCredit >= 150 && resultSumCredit < 250) summ += 2;
            else if (resultSumCredit >= 250) summ += 1;

            System.Console.Write("Цель кредита(1.Бытовая техника,2.Ремонт,3.Телефон,4.Прочее): ");
            creditsFor = int.Parse(Console.ReadLine());
            if (creditsFor == 1) summ += 2;
            else if (creditsFor == 2) summ += 1;
            else if (creditsFor == 4) summ -= 1;

            System.Console.Write("Срок кредита в месяцах: ");
            srokCredit = int.Parse(Console.ReadLine());
            if (srokCredit >= 1) summ += 1;


            System.Console.WriteLine("Рейтинг " + summ);
            Credit.Credits.verificationTheReitingForAdd(summ, (int)summCredit, creditsFor, srokCredit, newAcc);
            return summ;

        }

        public newAnketa(string numberPassport)
        {
            zapolnenieAnket(numberPassport);
            showCreditData();
        }

        public void zapolnenieAnket(string numberPassport)
        {
            if (Credit.Credits.haveAactuallCredit(numberPassport))
            {
                SqlConnection mssql = new SqlConnection(TZ.DataAccess.DBsql.connectionString);
                string strConect = $"select c.Sum, c.MonthCred, c.CreditFor, c.EndCreditData from Credits c, UserDocument u, UserAccaount a where u.IdDoc=a.IdDoc and c.IdCredit=a.IdCredit and u.IdPass='{numberPassport}'";
                mssql.Open();
                using (SqlCommand cmd = new SqlCommand(strConect, mssql))
                {
                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.HasRows)
                    {
                        summCredit = Convert.ToDouble(read.GetName(0));
                        beginCredint = Convert.ToDateTime(read.GetName(1));
                        creditsFor = Convert.ToInt32(read.GetName(2));
                        endCredit = Convert.ToDateTime(read.GetName(3));
                    }
                }
                mssql.Close();
            }
            else{
                System.Console.WriteLine("У вас нету активных кредитов.");
            }


        }

        public void showCreditData(){
            System.Console.WriteLine("Сумма кредита: "+summCredit);
            System.Console.WriteLine("Дата окончание: "+endCredit);

        }

    }
}