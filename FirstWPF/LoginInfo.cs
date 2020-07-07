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
        public string Role { get; set; }

       public LoginInfo(string login, string password, string role = "User")
       {
            Login = login;
            Password = password;
            Role = role;
       }
        public LoginInfo()
        {
            Login = "None";
            Password = "None";
            Role = "None";
        }

    }
}
