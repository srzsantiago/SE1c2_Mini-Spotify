using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class Track
    {

        public Track(){}
        public Track(string name)
        {
            Name = name;
        }
        public Track(string name, string artist, int duration)
        {
            Name = name;
            Artist = artist;
            Duration = duration;
        }

        public string Name { get; set; }

        public string Artist { get; set; } // its a string for now, testing
        public int Duration { get; set; } // Duration in seconds
        public Uri AudioFile { get; set; }

        
    }
}