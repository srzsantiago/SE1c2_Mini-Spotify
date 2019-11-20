using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    // acceslevel enum to check what kind of user is logged in
    public enum AccessLevel { Admin, Artist, User }
    class Login
    {
        private Person _user;
        public bool loggedin = false;
        public AccessLevel access { get; set; }

        // moet uit db gehaald worden 
        public string email = "gebruiker1@email.com";
        public string password = "gebruiker1";
        public string accessdb = "Admin";
       
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
                _user = new Person(this.loggedin, this.access);
            } else
            {
                Console.WriteLine("Username not found in our database");
            }
        }
    }
}
