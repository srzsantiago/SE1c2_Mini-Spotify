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
        private string Username { get; set; }
        private string Password { get; set; }
        private AccessLevel Access { get; set; }

        public Person(string username, string password, AccessLevel access)
        {
            Username = username;
            Password = password;
            Access = access;
        }
    }
}
