using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public enum AccessLevel { Admin, Artist, User }
    class Person
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public AccessLevel Access { get; set; }

        public Person(string email, string password, AccessLevel access)
        {

            Email = email;
            Password = password;
            Access = access;
        }
    }
}
