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
        public PlaylistController(Playlist playlist) // creates a playlist with given name
        {
            this.Playlist = playlist;
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
            string sql = $"DELETE FROM Track_has_Playlist WHERE trackID = {track.TrackId}"; // the sql query
            Database.DatabaseConnector.DeleteQueryDB(sql);
            Playlist.Tracks.Remove(track);
        }

        public void SetName(string name) // changes the name of the playlist
        {
            Playlist.Name = name;
        }

    }
}
