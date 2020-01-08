using System;

namespace Ritmo
{
    class Administrator:Person
    {
        public Administrator(bool loggedin, int personID) : base(loggedin, AccessLevel.user, personID)
        {

        }
    }
}
