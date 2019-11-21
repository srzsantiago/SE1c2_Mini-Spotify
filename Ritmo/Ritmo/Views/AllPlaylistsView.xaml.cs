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
    /// Interaction logic for MyPlaylistsView.xaml
    /// </summary>
    public partial class MyPlaylistsView : UserControl
    {
        AllPlaylistsController allplaylistcontroller = new AllPlaylistsController();
        

        public MyPlaylistsView()
        {
            
            InitializeComponent();
            testAllPlayLists();
            foreach (var item in allplaylistcontroller.allplaylists.playlists) // goes through all the playlists that exist and adds their name to the list in my playlists
            {
                Button button = new Button();
                button.Content = item.Name;
                
                NameColumn.Children.Add(button);               
            }
        }

        // maakt playlists aan en voegt de playlists toe aan de lijst, waarna deze zullen geladen worden in AllPlayListsView window in de GUI
        public void testAllPlayLists()
        {
            Playlist testplaylist1 = new Playlist("playlist1");
            Playlist testplaylist2 = new Playlist("playlist2");
            Playlist testplaylist3 = new Playlist("playlist3");
            Playlist testplaylist4 = new Playlist("playlist4");
            Playlist testplaylist5 = new Playlist("playlist5");
            Playlist testplaylist6 = new Playlist("playlist6");
            Playlist testplaylist7 = new Playlist("playlist7");
            Playlist testplaylist8 = new Playlist("playlist8");
            Playlist testplaylist9 = new Playlist("playlist9");
            Playlist testplaylist10 = new Playlist("playlist10");
            Playlist testplaylist11 = new Playlist("playlist11");
            Playlist testplaylist12 = new Playlist("playlist12");
            Playlist testplaylist13 = new Playlist("playlist13");
            Playlist testplaylist14 = new Playlist("playlist14");
            Playlist testplaylist15 = new Playlist("playlist15");
            Playlist testplaylist16 = new Playlist("playlist16");

            allplaylistcontroller.AddTrackList(testplaylist1);
            allplaylistcontroller.AddTrackList(testplaylist2);
            allplaylistcontroller.AddTrackList(testplaylist3);
            allplaylistcontroller.AddTrackList(testplaylist4);
            allplaylistcontroller.AddTrackList(testplaylist5);
            allplaylistcontroller.AddTrackList(testplaylist6);
            allplaylistcontroller.AddTrackList(testplaylist7);
            allplaylistcontroller.AddTrackList(testplaylist8);
            allplaylistcontroller.AddTrackList(testplaylist9);
            allplaylistcontroller.AddTrackList(testplaylist10);
            allplaylistcontroller.AddTrackList(testplaylist11);
            allplaylistcontroller.AddTrackList(testplaylist12);
            allplaylistcontroller.AddTrackList(testplaylist13);
            allplaylistcontroller.AddTrackList(testplaylist14);
            allplaylistcontroller.AddTrackList(testplaylist15);
            allplaylistcontroller.AddTrackList(testplaylist16);
            

            allplaylistcontroller.AddTrackList(testplaylist4);
            allplaylistcontroller.AddTrackList(testplaylist4);
            allplaylistcontroller.AddTrackList(testplaylist4);
        }
    }
}
