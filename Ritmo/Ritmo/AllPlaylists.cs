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
            
            sqlquery = "SELECT name, creationDate FROM Playlist";
            List<Dictionary<string, object>> playlistNames = Database.DatabaseConnector.SelectQueryDB(sqlquery);
            string name="";
            string creationDate="";
            foreach (var dictionary in playlistNames)
            {
                
                foreach (var key in dictionary)
                {
                    if (key.Key.Equals("name"))
                    {
                        name = key.Value.ToString();
                        count++;
                    }
                    else if (key.Key.Equals("creationDate"))
                    {
                        creationDate = key.Value.ToString();
                        count++;
                    }
                }
                if(count % 2 == 0)
                {
                    Playlists.Add(new Playlist(name) { CreationDate = Convert.ToDateTime(creationDate) });
                    Console.WriteLine(name + creationDate);
                }
            }
        }
        }
    }

