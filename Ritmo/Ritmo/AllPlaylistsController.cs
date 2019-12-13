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
            playlist.AddplaylistQuery();
        }

        public void RemovePlaylist(Playlist playlist) // removes a playlist from the playlist list
        {
            string sqlquery = "";
            string sqlquery2 = "";
            if (AllPlaylists.Playlists.Contains(playlist)) // checks if the given playlist exists.
            {
                //sqlquery2 can be used when we have the function to add tracks to the playlist
                //sqlquery2 = $"DELETE FROM Track_has_Playlist WHERE playlistID = { playlist.TrackListID}";
                sqlquery = $"DELETE FROM Playlist WHERE idPlaylist = {playlist.TrackListID}";
                //Database.DatabaseConnector.DeleteQueryDB(sqlquery2);
                Database.DatabaseConnector.DeleteQueryDB(sqlquery);
                AllPlaylists.Playlists.Remove(playlist);
            }
            else
            {
                throw new Exception("This playlist does not exists.");
            }
        }

        public Playlist GetPlaylist(int playlistID)
        {
            List<Playlist> playlists = AllPlaylists.Playlists;
            
            Playlist Result = (from p in AllPlaylists.Playlists
                               where (p.TrackListID == playlistID)
                               select p).Single();
            return Result;
        }

        public bool IsDupliaceName(string name)
        {
            //Find name
            try
            {
                bool nameExists = AllPlaylists.Playlists.Find(x => x.Name.Equals(name)).Name.Equals(name);
                return nameExists;
            }
            catch //Catch no name found
            {
                return false;
            }
        }
    }
}
