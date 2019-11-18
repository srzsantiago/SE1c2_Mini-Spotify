using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class Artist : Person
    {
        public string Name { get; set; }
        public string Producer { get; set; }

        public Artist(string username, string password, AccessLevel access, string name, string producer) : base(username, password, access)
        {
            Name = name;
            Producer = producer;
        }
    }
}
