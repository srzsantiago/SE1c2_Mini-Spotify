using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class Administrator : Person
    {
        public Administrator(string email, string password, AccessLevel access) : base(email, password, access)
        {
        }
    }
}
