using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class AllPlaylistsController
    {
        private AllPlaylists allplaylists;

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
            allplaylists.playlists.Remove(playlist);
        }
    }
}
