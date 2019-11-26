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
using Microsoft.VisualBasic;
using System.Windows.Forms;
using UserControl = System.Windows.Controls.UserControl;
using Button = System.Windows.Controls.Button;

namespace Ritmo.Views
{
    /// <summary>
    /// Interaction logic for MyPlaylistsView.xaml
    /// </summary>
    public partial class MyPlaylistsView : UserControl
    {
        Playlist p1 = new Playlist("FirstPlaylist");
        AllPlaylistsController allplaylistcontroller = new AllPlaylistsController();
        bool menuPenalIsOpen = false;


        public MyPlaylistsView()
        {
            InitializeComponent();
            TestTrack();
            foreach (var item in allplaylistcontroller.allplaylists.playlists) // goes through all the playlists that exist and adds their name to the list in my playlists
            {
                Button button = new Button();
                button.Content = item.Name;
                button.Click += PlaylistClick;

                NameColumn.Children.Add(button);
            }
        }

        //LEGACY: Moet omgezet worden naar MVVM.
        public void PlaylistClick(Object sender, EventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            //ns.Navigate(new Uri("Views/PlaylistView.xaml", UriKind.Relative));
            ns.Navigate(new PlaylistView(p1));
        }

        public void TestTrack()
        {
            allplaylistcontroller.AddTrackList(p1);
        }

        // maakt playlists aan en voegt de playlists toe aan de lijst, waarna deze zullen geladen worden in AllPlayListsView window in de GUI
        /*public void testAllPlayLists()
        {
            Playlist testplaylist1 = new Playlist("playlist1");
            Playlist testplaylist2 = new Playlist("playlist2");
            Playlist testplaylist3 = new Playlist("playlist3");
            Playlist testplaylist4 = new Playlist("playlist4");
            Playlist testplaylist5 = new Playlist("playlist5");


            allplaylistcontroller.AddTrackList(testplaylist1);
            allplaylistcontroller.AddTrackList(testplaylist2);
            allplaylistcontroller.AddTrackList(testplaylist3);
            allplaylistcontroller.AddTrackList(testplaylist4);
            allplaylistcontroller.AddTrackList(testplaylist5);

        }*/

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (!menuPenalIsOpen)
            {
                MenuPanel.Height = +120;
                menuPenalIsOpen = true;
            }
            else
            {
                MenuPanel.Height = 0;
                menuPenalIsOpen = false;
            }
        }


        private void AddPlayList_Click(object sender, RoutedEventArgs e)
        {

            string x = Interaction.InputBox("Please insert a name:", "Create playlist", "playlist name", 10, 10);

            if(x == "")
            {
                System.Windows.MessageBox.Show("Did not make a playlist");
            } else
            {
                System.Windows.MessageBox.Show("Playlist " + x + " is made!");
                Playlist playlist = new Playlist(x);
                allplaylistcontroller.AddTrackList(playlist);

                Button button = new Button();
                button.Content = playlist.Name;
                button.Click += PlaylistClick;
                NameColumn.Children.Add(button);


            }



        }
    }
}
