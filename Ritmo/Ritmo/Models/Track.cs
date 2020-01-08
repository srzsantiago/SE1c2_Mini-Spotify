using Ritmo.Database;
using System;
using System.Collections.Generic;

namespace Ritmo
{
    public class Track
    {
        public int TrackId { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; } // A string for now, will later change to Artist class
        public int Duration { get; set; } // Duration in seconds
        public string Album { get; set; }
        public Uri AudioFile { get; set; }

        public Track() { }
        public Track(int trackId, string name, string artist, int duration)
        {
            TrackId = trackId;
            Name = name;
            Artist = artist;
            Duration = duration;
        }

        // function to get the album image which belongs to a track 
        public string GetAlbumCover(int id) // uses this id to select the right album which belongs to this track 
        {
            string sqlQuery;
            string albumCover = ""; // string to return the path of the image

            sqlQuery = $"SELECT image FROM Album WHERE idAlbum IN(SELECT albumID FROM Track_has_Album WHERE trackID = { id })"; // select query which uses the connection table
            List<Dictionary<string, object>> albumImages = DatabaseConnector.SelectQueryDB(sqlQuery); // run the query

            // get the results
            foreach (var dictionary in albumImages)
            {
                foreach (var key in dictionary)
                {
                    albumCover = key.Value.ToString(); //set the string with the path of the image
                }
            }
            return albumCover; // return the image path with the album cover image
        }
    }
}