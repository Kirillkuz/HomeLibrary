using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWPF
{
    public class LoginInfo
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

       public LoginInfo(string login, string password)
       {
            Login = login;
            Password = password;
       }
        public LoginInfo()
        {
            Login = "None";
            Password = "None";
        }

    }
}
