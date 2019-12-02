using Ritmo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ritmo.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        PlayQueueController playqueuecontroller = new PlayQueueController();
        AllPlaylistsController allplaylistcontroller;
        
        private int clickedbuttonvalue;
        PlaylistController testplaylist1 = new PlaylistController("playlist1");

        public HomeView()
        {
            InitializeComponent();
        }

        




        

       

        //private void Playlistboxes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    foreach (var item in allTestTrack)
        //    {
        //        if (item.TrackId == clickedbuttonvalue)
        //        {
        //            for (int i = 0; i < allplaylistcontroller.allplaylists.playlists.Count; i++)
        //            {
        //                var selecteditem = Playlistboxes.SelectedItem;
        //                if (selecteditem.Equals(allplaylistcontroller.allplaylists.playlists.ElementAt(i).Name))
        //                {
        //                    allplaylistcontroller.allplaylists.playlists.ElementAt(i).Tracks.AddLast(item);
        //                }
        //            }

        //        }

        //    }
        //}
    }
}
