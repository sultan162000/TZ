using System.Collections.Generic;
using DataBase;


namespace Verifications
{
   public static class RegUser
    {
        public static bool verificationUser(string numberPassport)
        {
            List<string> checkThePassport = Db.selectTheIdPassOfDB();
            if (checkThePassport.Contains(numberPassport))
            {
                return true;
            }
            return false;
        }
        
    }
}