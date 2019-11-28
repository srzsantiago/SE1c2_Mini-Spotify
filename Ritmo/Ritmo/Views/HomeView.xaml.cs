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
        AllPlaylistsController allplaylistcontroller;
        public HomeView()
        {
            allplaylistcontroller = AllPlaylistsViewModel.AllPlaylistsController;
            testAllPlayLists();
            InitializeComponent();
            
        
         }

        public void testAllPlayLists()
        {

            Playlist testplaylist1 = new Playlist(0, "playlist1", 100, DateTime.Today);
            Playlist testplaylist2 = new Playlist(1, "playlist2", 200, DateTime.Today);
            Playlist testplaylist3 = new Playlist(2, "playlist3", 400, DateTime.Today.AddDays(1));
            Playlist testplaylist4 = new Playlist(3, "playlist4", 5000, DateTime.Today.AddMonths(4));
            Playlist testplaylist5 = new Playlist(4, "playlist5", 2222, DateTime.Today);
            

            allplaylistcontroller.AddTrackList(testplaylist1);
            allplaylistcontroller.AddTrackList(testplaylist2);
            allplaylistcontroller.AddTrackList(testplaylist3);
            allplaylistcontroller.AddTrackList(testplaylist4);
            allplaylistcontroller.AddTrackList(testplaylist5);

        }
        private void AddToPlayListClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddToQueueClick(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            foreach (var item in allplaylistcontroller.allplaylists.playlists)
            {
               // Listbox.Item
               //Listbox.Items.Add(item + item.Name);
            }
            Listbox.Items.Refresh();
        }
    }
}
