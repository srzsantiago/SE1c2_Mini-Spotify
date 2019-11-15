using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class PlayListController
    { 
        private Playlist playlist; 

        public PlayListController(string name) // creates a playlist with given name
        {
            this.playlist = new Playlist(name);
        }
        
        public void AddTrack(Track track) // adds a track to the playlist
        {
            playlist.Tracks.AddLast(track);
        }

        public void RemoveTrack(Track track) // removes a track from the playlist
        {
            playlist.Tracks.Remove(track);
        }

        public void SetName(string name) // changes the name of the playlist
        {
            playlist.Name = name;
        }

    }
}
