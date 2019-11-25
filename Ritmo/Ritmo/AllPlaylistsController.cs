using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class AllPlaylistsController
    {
        public AllPlaylists allplaylists { get; set; }

        public AllPlaylistsController()
        {
            this.allplaylists = new AllPlaylists();
        }

        public void AddTrackList(Playlist playlist) // adds a playlist to the playlist list.
        {
            allplaylists.playlists.Add(playlist);
        }

        public void RemovePlaylist(Playlist playlist) // removes a playlist from the playlist list
        {
            if (allplaylists.playlists.Contains(playlist)) // checks if the given playlist exists.
            {
                allplaylists.playlists.Remove(playlist);
            }
            else
            {
                throw new Exception("This playlist does not exists.");
            }
        }
    }
}
