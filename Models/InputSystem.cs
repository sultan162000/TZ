using System.Collections.Generic;
using System.Data.SqlClient;
using DataBase;

namespace Auth
{
    public class InputSystem
    {
        private string login { get; set; }
        public bool AuthLogin(string Login)
        {
            List<string> listUser = Db.selectTheLogin();
            if (listUser.Contains(Login))
            {
                this.login = Login;
                return true;
            }
            return false;
        }

        public bool AuthPass(string login,string pass)
        {
                List<string> passUser = new List<string>();
                passUser.Add(Db.selectThePass(login,pass));
                if(passUser.Contains(pass)){
                    return true;
                }

            return false;
        }

    }
}