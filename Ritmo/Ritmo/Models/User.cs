using System;

namespace Ritmo
{
    class User : Person
    {

        public User(bool loggedin, int personID) : base(loggedin, AccessLevel.user, personID){ }
    }
}
