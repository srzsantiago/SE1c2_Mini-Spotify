using Ritmo.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ritmo
{
    public class AllPlaylistsController
    {
        public delegate void DeletePlaylistEventHandler();
        public static event DeletePlaylistEventHandler PlaylistDeleted; //Event that will be called when a playlist is removed

        public AllPlaylists AllPlaylists { get; set; }

        public AllPlaylistsController(int userID)
        {
            AllPlaylists = new AllPlaylists(userID);
        }

        public void AddTrackList(Playlist playlist) // adds a playlist to the playlist list.
        {
            AllPlaylists.Playlists.Add(playlist);
            playlist.AddplaylistQuery();
        }

        public void RemovePlaylist(Playlist playlist) // removes a playlist from the playlist list
        {
            string sqlquery;
            string sqlquery2;

            if (AllPlaylists.Playlists.Contains(playlist)) // checks if the given playlist exists.
            {
                if (!IsPlaylistEmpty(playlist.TrackListID))
                {
                    sqlquery2 = $"DELETE FROM Track_has_Playlist WHERE playlistID = { playlist.TrackListID}";
                    DatabaseConnector.DeleteQueryDB(sqlquery2);
                }

                sqlquery = $"DELETE FROM Playlist WHERE idPlaylist = {playlist.TrackListID}";
                DatabaseConnector.DeleteQueryDB(sqlquery);
                AllPlaylists.Playlists.Remove(playlist);
                PlaylistDeleted();
            }
            else
                throw new Exception("This playlist does not exists.");
        }

        public bool IsPlaylistEmpty(int playlistid)
        {
            string sql = $"SELECT idTrack FROM Track WHERE idTrack IN (SELECT trackID FROM Track_has_Playlist WHERE playlistID = {playlistid})";

            List<Dictionary<string, object>> idlist = DatabaseConnector.SelectQueryDB(sql);

            if (idlist.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
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
    }
}
