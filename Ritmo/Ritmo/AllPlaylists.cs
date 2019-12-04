using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class AllPlaylists
    {
        public List<Playlist> Playlists { get; set; } // the list where all the playlists go into.

        public AllPlaylists()
        {
            Playlists = new List<Playlist>();
        }
    }
}
