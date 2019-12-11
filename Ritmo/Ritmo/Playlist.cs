using Ritmo.Database;
using System;
using System.Collections.Generic;

namespace Ritmo
{
    public class Playlist : TrackList
    {
        public Playlist(string name) : base(name)
        {
            CreationDate = DateTime.Now;
            AddplaylistQuery();
        }

        //Adds playlist to the database
        public void AddplaylistQuery()
        {
            string sql = $"INSERT INTO Playlist (name, creationDate) VALUES ('{Name}','{CreationDate.ToString("yyyy-MM-dd")}')";
            DatabaseConnector.InsertQueryDB(sql);
        }

    }
}