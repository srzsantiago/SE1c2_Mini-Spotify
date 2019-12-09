using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class AllPlaylists
    {
        public List<Playlist> Playlists
        { get; set; } // the list where all the playlists go into.

        public AllPlaylists()
        {
            Playlists = new List<Playlist>();
            //GetPlaylists();

        }
        public void GetPlaylists()
        {
            String sqlquery = "";
            int count = 0;
            
            sqlquery = "SELECT name, creationDate FROM Playlist"; // the query that is going to get the info from the database
            // Dictionary<string, object> = the string is the key (so [name] and [creationDate] the object is the value bound to the key)
            List<Dictionary<string, object>> playlistNames = Database.DatabaseConnector.SelectQueryDB(sqlquery); // executing the query
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
                    }
                }
                if(count % 2 == 0) // if 2 values got assigned (so a name and a creation date)
                {
                    Playlists.Add(new Playlist(name) { CreationDate = Convert.ToDateTime(creationDate) }); // creates the playlist
                }
            }
        }
        }
    }

