using System;

namespace Ritmo
{
    public class Playlist : TrackList
    {
        public Playlist(string name) : base(name)
        {
        }

        public Playlist(int tracklistid, string name, int duration, DateTime datetime) : base(tracklistid, name, duration, datetime)
        {
        }
    }
}