using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class Login
    {
        private Person _user;
        public bool loggedin = false;
        AccessLevel access { get; set; }

        // moet uit db gehaald worden 
        public string email = "gebruiker1@email.com";
        public string password = "gebruiker1";
        public AccessLevel accessdb = AccessLevel.admin;

        // login function
        public Login(string email, string password)
        {
            if(this.email == email)
            {
                if(this.password != password)
                {
                    Console.WriteLine("password incorrect");
                }
                this.email = email;
                this.password = password;
                this.access = accessdb;
                loggedin = true;
                // create new user with the loggedin bool and the access enum value
                if(access == AccessLevel.user)
                {
                    _user = new User(this.loggedin);
                }
                else if (access == AccessLevel.artist)
                {
                    _user = new Artist(this.loggedin, "naam moet uit db", "Producer moet uit db");
                }
                else if (access == AccessLevel.admin)
                {
                    _user = new Administrator(this.loggedin);
                }
                
            } else
            {
                Console.WriteLine("Username not found in our database");
            }
        }
    }
}
