using Ritmo.Database;
using System;
using System.Collections.Generic;

namespace Ritmo
{
    public class AllPlaylists
    {
        public List<Playlist> Playlists
        { get; set; } // the list where all the playlists go into.

        public AllPlaylists(int userID)
        {
            Playlists = new List<Playlist>();
            GetPlaylists(userID);
        }
        public void GetPlaylists(int userID)
        {
            int count = 0;

            string sqlquery = $"SELECT idPlaylist, name, creationDate FROM Playlist WHERE consumerID = {userID}";
            // Dictionary<string, object> = the string is the key (so [name] and [creationDate] the object is the value bound to the key)
            List<Dictionary<string, object>> playlistNames = DatabaseConnector.SelectQueryDB(sqlquery); // executing the query
            int playlistid = 0;
            string name="";
            string creationDate="";
            foreach (var dictionary in playlistNames) { // goes through the dictionary
                
                foreach (var key in dictionary) // goes through the keys (example: name, creationDate, name, creationDate...... etc)
                {
                    if (key.Key.Equals("name")) // if the key is [name]:
                    {
                        name = key.Value.ToString();
                        count++;
                    }
                    else if (key.Key.Equals("creationDate")) // if the key is [creationDate]
                    {
                        creationDate = key.Value.ToString();
                        count++;
                    } else if (key.Key.Equals("idPlaylist")) // if the key is [idPlaylist]
                    {
                        playlistid = Convert.ToInt32(key.Value);
                        count++;
                    }
                }
                if(count % 3 == 0) // if 3 values got assigned (so an id, name and creation date)
                {
                    Playlists.Add(new Playlist(name) { TrackListID = playlistid, CreationDate = Convert.ToDateTime(creationDate) }); // creates the playlist
                }
            }
        }
        }
    }

