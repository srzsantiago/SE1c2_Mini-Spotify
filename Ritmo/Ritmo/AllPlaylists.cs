using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    public class AllPlaylists
    {
        public List<Playlist> playlists { get; set; } // the list where all the playlists go into.

        public AllPlaylists()
        {
            playlists = new List<Playlist>();
        }
    }
}
