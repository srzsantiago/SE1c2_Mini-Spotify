using System;
using System.Collections.Generic;

namespace Ritmo
{
    public class Playlist : TrackList
    {
        public Playlist(string name) : base(name)
        {
            AddplaylistQuery();
        }

        public void AddplaylistQuery()
        {
            String sql = "";
            sql = "INSERT INTO Playlist (name, creationDate) VALUES ('" + this.Name + "','" + this.CreationDate + "')";
            Database.DatabaseConnector.InsertQueryDB(sql);
        }

    }
}