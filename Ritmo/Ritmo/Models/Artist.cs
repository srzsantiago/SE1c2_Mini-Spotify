using System;

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
