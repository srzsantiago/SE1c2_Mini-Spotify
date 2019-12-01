using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class Administrator:Person
    {
        public Administrator(bool loggedin) : base(loggedin, AccessLevel.user)
        {

        }
    }
}
