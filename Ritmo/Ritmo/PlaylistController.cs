using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class PlaylistController
    {
        public Playlist Playlist { get; set; }

        public PlaylistController(string name) // creates a playlist with given name
        {
            this.Playlist = new Playlist(name);
        }
        
        public void AddTrack(Track track) // adds a track to the playlist.
        {
            if(Playlist.Tracks.Contains(track)) // checks if the track exists in the playlist (tracks cant appear more than once in a playlist)
            {
                Console.WriteLine("track already exists in this playlist");
            } else
            {
                Playlist.Tracks.AddLast(track);
            }
            
        }

        public void RemoveTrack(Track track) // removes a track from the playlist.
        {
            if(Playlist.Tracks.Contains(track)) // checks if the given track is in the playlist.
            {
                Playlist.Tracks.Remove(track);
            } else
            {
                Console.WriteLine("track bestaat niet in deze playlist");
            }
            
        }

        public void SetName(string name) // changes the name of the playlist
        {
            Playlist.Name = name;
        }

    }
}
