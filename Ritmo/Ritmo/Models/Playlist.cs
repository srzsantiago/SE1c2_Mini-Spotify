using Ritmo.Database;
using System;

namespace Ritmo
{
    public class Playlist : TrackList
    {
        public Playlist(string name) : base(name)
        {
            CreationDate = DateTime.Now;
        }

        //Adds playlist to the database
        public void AddplaylistQuery()
        {
            string sql = $"INSERT INTO Playlist (name, creationDate, consumerID) VALUES ('{Name}','{CreationDate.ToString("yyyy-MM-dd")}','{OwnerID}')";
            DatabaseConnector.InsertQueryDB(sql);
        }
    }
}