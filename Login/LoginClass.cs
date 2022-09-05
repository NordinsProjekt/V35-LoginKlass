using Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LoginClass
{
    public class Login :ILogin
    {
        private bool isLoggedIn = false;
        private User? user;
        public bool LoginUser(string email, string password)
        {
            if (CheckIfLoginIsValid(email, password))
                return true;
            //kontrollera om det är en email som följer reglerna.
            if (CheckEmail(email))
                if (CheckPassword(password)) //kontrollera om det är ett lösenord som följer reglerna.
                    return false;
                else
                    throw new ArgumentException("Password is not valid");
            else
                throw new ArgumentException("Email is not valid");              
        }
        public bool IsLoggedIn()
        {
            return isLoggedIn;
        }
        private bool CheckEmail(string email)
        {
            if (email.Length < 5 || email.Length > 30) return false;
            if (email.Contains('@') && email.Contains('.'))
            {
                int resultat = email.LastIndexOf('.') - email.IndexOf('@');
                string addKod = email.Substring(email.LastIndexOf('.')+1);
                if (resultat > 0 && AddKod().Contains(addKod)) return true;
                else return false;
            }
            return false;
        }
        private bool CheckIfLoginIsValid(string email, string password)
        {
            isLoggedIn = false;
            var tempUser = UserList().SingleOrDefault(u => u[0].Equals(email));

            if (tempUser == null) return false;
            if (email != (string)tempUser[0]) return false;
            if (password != (string)tempUser[1]) return false;

            user = new User((string)tempUser[2], (string)tempUser[3], DateTime.Now);
            isLoggedIn = true;
            return true;
        }
        private List<object[]> UserList()
        {
            List<object[]> userList = new List<object[]>()
            {
                new object[] { "admin@uddevalla.se","Password123!","Admin","Admin" },
                new object[] { "belfegorc4@gmail.com","Markus123!","Markus Nordin","User" },
                new object[] { "eric@hotmail.se","EricUddevalla123!","Eric","User" }
            };
            return userList;
        }
        


        private bool CheckPassword(string password)
        {
            if (password.Length < 5 || password.Length > 20) return false;

            string validChars = "abcdefghijklmnopqrstuvwxyzåäöABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ0123456789!\"#¤%&/()=?";
            string special = "!\"#¤%&/()=?";
            int[] numberOfChars = new int[] { 0, 0, 0, 0 };
            foreach (char c in password)
            {
                if (!validChars.Contains(c)) return false;
                if (Char.IsUpper(c)) numberOfChars[0]++;
                if (Char.IsLower(c)) numberOfChars[1]++;
                if (Char.IsNumber(c)) numberOfChars[2]++;
                if (special.Contains(c)) numberOfChars[3]++;
            }
            if (numberOfChars[0] > 0 && numberOfChars[1] > 0 && numberOfChars[2] > 0 && numberOfChars[3] > 0)
                return true;
            else
                return false;
        }

        private List<string> AddKod()
        {
            List<string> kod = new List<string>()
            {
                "se","com","io","jp","cz"
            };
            return kod;
        }

        public void Logout()
        {
            isLoggedIn = false;
        }

        public string GetUserName()
        {
            if (user == null)
                return "";
            else
                return user.username;
        }

        public string GetUserRole()
        {
            if (user == null)
                return "";
            else
                return user.role;
        }
    }
}
