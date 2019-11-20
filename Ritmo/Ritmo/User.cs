using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    // acceslevel enum to check what kind of user is logged in
    public enum AccessLevel { Admin, Artist, User }

    class User
    {
        bool loggedin { get; set; }
        AccessLevel access { get; }

        // create user after login, this user can be used to check the acces to other classes. 
        public User(bool loggedin, AccessLevel access) 
        {
            this.loggedin = loggedin;
            this.access = access; 
        }
    }
}
