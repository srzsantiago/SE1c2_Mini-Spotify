using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class User : Person
    {
        
        public User (bool loggedin):base(loggedin, AccessLevel.user){
            
        }
    }
}
