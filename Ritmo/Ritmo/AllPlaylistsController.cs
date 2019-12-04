using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class AllPlaylistsController
    {
        public AllPlaylists AllPlaylists { get; set; }

        public AllPlaylistsController()
        {
            this.AllPlaylists = new AllPlaylists();
        }

        public void AddTrackList(Playlist playlist) // adds a playlist to the playlist list.
        {
            AllPlaylists.Playlists.Add(playlist);
        }

        public void RemovePlaylist(Playlist playlist) // removes a playlist from the playlist list
        {
            if (AllPlaylists.Playlists.Contains(playlist)) // checks if the given playlist exists.
            {
                AllPlaylists.Playlists.Remove(playlist);
            }
            else
            {
                throw new Exception("This playlist does not exists.");
            }
        }

        public Playlist GetPlaylist(int playlistID)
        {
            //Set up query to get playlist by playlistID
            List<Playlist> playlists = AllPlaylists.Playlists;

            Playlist Result = (from p in AllPlaylists.Playlists
                               where (p.TrackListID == playlistID)
                               select p).Single();

            return Result;
        }
    }
}
