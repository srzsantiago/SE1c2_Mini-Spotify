using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo
{
    class AllPlaylists
    {
        List<Playlist> playlists { get; set; }

        public AllPlaylists()
        {
            playlists = new List<Playlist>();
        }
    }
}
