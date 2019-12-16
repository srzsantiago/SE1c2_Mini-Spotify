using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class Track
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; } // its a string for now, testing
        public int Duration { get; set; } // Duration in seconds

        public string Album { get; set; }
        public Uri AudioFile { get; set; }

        public Track()
        {

        }

        public Track(string name)
        {
            Name = name;
        }

        public Track(int trackId, string name, string artist, int duration)
        {
            TrackId = trackId;
            Name = name;
            Artist = artist;
            Duration = duration;
        }

        public string getAlbumCover(int id)
        {
            String sqlQuery = "";
            string albumCover = "";

            sqlQuery = $"SELECT image FROM Album WHERE idAlbum IN(SELECT albumID FROM Track_has_Album WHERE trackID = { id })";
            List<Dictionary<string, object>> albumImages = Database.DatabaseConnector.SelectQueryDB(sqlQuery);

            foreach (var dictionary in albumImages)
            {
                foreach (var key in dictionary)
                {
                    albumCover = key.Value.ToString();
                }
            }
            return albumCover;
        }
    }
}