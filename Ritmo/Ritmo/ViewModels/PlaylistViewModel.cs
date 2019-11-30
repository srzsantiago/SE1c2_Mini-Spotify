using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ritmo.ViewModels
{
    public class PlaylistViewModel : Screen
    {
        private static PlaylistController _playlistController;

        public static PlaylistController PlaylistsController
        {

            get { return _playlistController; }
            set { _playlistController = value; }
        }
    }
}
