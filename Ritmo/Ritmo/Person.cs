using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{

    class Person
    {
        bool loggedin { get; set; }
        AccessLevel access;

        // create user after login, this user can be used to check the acces to other classes. 
        public Person(bool loggedin, AccessLevel access) 
        {
            this.loggedin = loggedin;
            this.access = access; 
        }
    }
}
