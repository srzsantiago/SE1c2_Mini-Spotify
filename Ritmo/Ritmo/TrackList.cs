using System.Collections.Generic;

namespace Ritmo
{
    public class TrackList
    {
        public string Name { get; set; }
        public LinkedList<Track> Tracks {get; set; }

        public TrackList(string name)
        {
            Name = name;
            Tracks = new LinkedList<Track>();
        }
    }
}