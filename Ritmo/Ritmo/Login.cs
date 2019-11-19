using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class Login
    {
        private User _user;
        public bool loggedin = false;
        public string typeofuser = "";

        string[,] array2d = new string[,]{{adminname1, adminpw1}, {adminname2, adminpw2}, {artistname1, artistpw1 }, {artistname2, artistpw2 },{username1,userpw1 }, {username2, userpw2 } };

        public Login(string username, string password)
        {
            var poginlogin = array2d
        }
    }
}
