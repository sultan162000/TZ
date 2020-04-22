using System.Collections.Generic;
using RegisterUser;

namespace Anketa
{
    public class newAnketa{
        private int summ = 0;

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
            System.Console.WriteLine("Введите вашу ежемецячную зарплату: ");
        }
    }
}