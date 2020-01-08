using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class Artist : Person
    {
        public string Name;
        public string Producer;
        public Artist(bool loggedin, string Name, string Producer, int personID) : base(loggedin, AccessLevel.artist, personID)
        {
            this.Name = Name;
            this.Producer = Producer;
        }
    }
}
