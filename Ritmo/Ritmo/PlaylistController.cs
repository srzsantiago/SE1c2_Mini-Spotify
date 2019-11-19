using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class PlaylistController
    {
        public Playlist Playlist { get; set; }

        public PlaylistController(string name) // creates a playlist with given name
        {
            this.Playlist = new Playlist(name);
        }
        
        public void AddTrack(Track track) // adds a track to the playlist.
        {
            if (Playlist.Tracks.Contains(track))
            {
                throw new Exception("This track already contains in this playlist.");
            }
            else
            {
                Playlist.Tracks.AddLast(track);
            }            
        }

        public void RemoveTrack(Track track) // removes a track from the playlist.
        {
            if (Playlist.Tracks.Contains(track)) // checks if the given track is in the playlist.
            {
                Playlist.Tracks.Remove(track);
            } else
            {
                throw new Exception("This track does not exist in this playlist.");
            }
            
        }

        public void SetName(string name) // changes the name of the playlist
        {
            Playlist.Name = name;
        }

    }
}
