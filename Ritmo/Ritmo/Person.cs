using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{

    public abstract class Person
    {
        public bool Loggedin { get; set; }
        public AccessLevel Access { get; set; }
        public int PersonID { get; set; }

        // create user after login, this user can be used to check the acces to other classes. 
        public Person(bool loggedin, AccessLevel access, int personID) 
        {
            Loggedin = loggedin;
            Access = access;
            PersonID = personID;
        }
    }
}
