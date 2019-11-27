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
using CheckBox = System.Windows.Controls.CheckBox;
using System.Windows.Forms.VisualStyles;
using Label = System.Windows.Controls.Label;

namespace Ritmo.Views
{
    /// <summary>
    /// Interaction logic for MyPlaylistsView.xaml
    /// </summary>
    public partial class MyPlaylistsView : UserControl
    {
        AllPlaylistsController allplaylistcontroller;

        bool menuPenalIsOpen = false;

        int count = -1;

        public MyPlaylistsView()
        {
            allplaylistcontroller = new AllPlaylistsController();
            InitializeComponent();
            testAllPlayLists();
            GetPlayListsGUI();
            

        }

        public void GetPlayListsGUI()
        {


                foreach (var item in allplaylistcontroller.allplaylists.playlists) // goes through all the playlists that exist and adds their name to the list in my playlists
                {
                    count++;
                    CheckBox checkbox = new CheckBox(); // 
                    Button button = new Button();
                    Label DurationLabel = new Label();
                    Label CreationDateLabel = new Label();
                    checkbox.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                    button.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                    DurationLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                    CreationDateLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;

                    button.Height = 27;
                    button.Content = item.Name;
                    DurationLabel.Content = item.TrackListDuration;
                    CreationDateLabel.Content = item.CreationDate;

                    button.Click += PlaylistClick;

                    CheckboxColumn.Children.Add(checkbox);
                    NameColumn.Children.Add(button);
                    DurationColumn.Children.Add(DurationLabel);
                    CreationDateColumn.Children.Add(CreationDateLabel);
                   

                }
            
        }

       

        //LEGACY: Moet omgezet worden naar MVVM.
        public void PlaylistClick(Object sender, EventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            //ns.Navigate(new Uri("Views/PlaylistView.xaml", UriKind.Relative));
            ns.Navigate(new PlaylistView(testplaylist1));
        }

        // maakt playlists aan en voegt de playlists toe aan de lijst, waarna deze zullen geladen worden in AllPlayListsView window in de GUI
        public void testAllPlayLists()
        {
            Playlist testplaylist1 = new Playlist(1, "playlist1", 100, DateTime.Today);
            Playlist testplaylist2 = new Playlist(2, "playlist2", 200, DateTime.Today);
            Playlist testplaylist3 = new Playlist(3, "playlist3", 400, DateTime.Today.AddDays(1));
            Playlist testplaylist4 = new Playlist(4, "playlist4", 5000, DateTime.Today.AddMonths(4));
            Playlist testplaylist5 = new Playlist(5, "playlist5", 2222, DateTime.Today);


            allplaylistcontroller.AddTrackList(testplaylist1);
            allplaylistcontroller.AddTrackList(testplaylist2);
            allplaylistcontroller.AddTrackList(testplaylist3);
            allplaylistcontroller.AddTrackList(testplaylist4);
            allplaylistcontroller.AddTrackList(testplaylist5);

        }

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
            MenuPanel.Height = 0;
            menuPenalIsOpen = false;
            string x = Interaction.InputBox("Please insert a name:", "Create playlist", "playlist name", 10, 10);

            if(x == "")
            {
                
            } else
            {
                Playlist playlist = new Playlist(1 , x, 100, DateTime.Today);
                allplaylistcontroller.AddTrackList(playlist);
                
            }



        }

        private void DeletePlayList_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Height = 0;
            menuPenalIsOpen = false;

            //foreach (var item in collection)
            //{
            //    allplaylistcontroller.RemovePlaylist();
            //}

        }
    }
}
