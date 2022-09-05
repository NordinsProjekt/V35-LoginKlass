using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ILogin
    {
        public bool LoginUser(string email, string password);
        public bool IsLoggedIn();
        public void Logout();
        public string GetUserName();
        public string GetUserRole();
    }
}
