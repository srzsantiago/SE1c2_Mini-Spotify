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
using System.Windows.Threading;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Ritmo.Views
{
    /// <summary>
    /// Interaction logic for AllPlayListsView.xaml
    /// </summary>
    public partial class AllPlayListsView : UserControl
    {
        AllPlaylistsController allplaylistcontroller;
        PlaylistView playlistview = new PlaylistView();
        bool menuPenalIsOpen = false;
        int count = 0; // this is for playlists ids. will be replaced with sql query later
        
        public AllPlayListsView()
        {
            
            allplaylistcontroller = AllPlaylistsViewModel.AllPlaylistsController;
            InitializeComponent();
            //testAllPlayLists();
            GetPlayListsGUI();

        }

        public void ClearItems()
        {
            NameColumn.Children.Clear();
            CreationDateColumn.Children.Clear();
            DeleteButtonColumn.Children.Clear();
            DurationColumn.Children.Clear();
        }

        public void GetPlayListsGUI()
        {
            ClearItems();
            foreach (var item in allplaylistcontroller.allplaylists.playlists) // goes through all the playlists that exist and adds their name to the list in my playlists
                {
                

                    Button DeleteButton = new Button();
                    Button NameButton = new Button();
                    Label DurationLabel = new Label();
                    Label CreationDateLabel = new Label();
                
                    DeleteButton.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    NameButton.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                    DurationLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                    CreationDateLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;

                    NameButton.Height = 27;
                    NameButton.Content = item.Name;
                    NameButton.Tag = count;
                    count++;

                    DeleteButton.Height = 27;
                    DeleteButton.Width = 27;
                    DeleteButton.Content = item.TrackListID;
                    

                    DurationLabel.Content = item.TrackListDuration;
                    CreationDateLabel.Content = item.CreationDate;

                    NameButton.Click += PlaylistClick;
                    DeleteButton.Click += DeletePlayList_Click;

                    DeleteButtonColumn.Children.Add(DeleteButton);
                    NameColumn.Children.Add(NameButton);
                    DurationColumn.Children.Add(DurationLabel);
                    CreationDateColumn.Children.Add(CreationDateLabel);
                   

                }
            
        }

       

        //LEGACY: Moet omgezet worden naar MVVM.
        public void PlaylistClick(Object sender, EventArgs e)
        {
            
            NavigationService ns = NavigationService.GetNavigationService(this);
            //ns.Navigate(new Uri("Views/PlaylistView.xaml", UriKind.Relative));
  

            Button clickedButton = sender as Button; // checks which button is pressed
            int playlistamount = allplaylistcontroller.allplaylists.playlists.Count; // counts the amount of playlists
            string buttoncontent = (string)clickedButton.Content; // puts the content of the clicked button onto an int
            int index = (int)clickedButton.Tag; 
            Playlist playlist = allplaylistcontroller.allplaylists.playlists.ElementAt(index);
            

            for (int i = 0; i < playlistamount; i++) // FIX: when 2 playlists have the same name(this can be fixed when playlist has their own id in SQL)
            {
                if (allplaylistcontroller.allplaylists.playlists[i].Name == buttoncontent) // checks if i is equal to the pressed buttons content
                {
                    //playlistview.ChangePlaylist(playlist);
                    ns.Navigate(playlistview); // navigates to the desired playlist
                }
            }

        }

        // maakt playlists aan en voegt de playlists toe aan de lijst, waarna deze zullen geladen worden in AllPlayListsView window in de GUI
        public void testAllPlayLists()
        {

            Playlist testplaylist1 = new Playlist(0, "playlist1", 100, DateTime.Today);
            Playlist testplaylist2 = new Playlist(1, "playlist2", 200, DateTime.Today);
            //Playlist testplaylist3 = new Playlist(2, "playlist3", 400, DateTime.Today.AddDays(1));
            //Playlist testplaylist4 = new Playlist(3, "playlist4", 5000, DateTime.Today.AddMonths(4));
            //Playlist testplaylist5 = new Playlist(4, "playlist5", 2222, DateTime.Today);
            Track t1 = new Track("name");
            Track track2 = new Track("lol");
            testplaylist1.Tracks.AddLast(t1);
            testplaylist2.Tracks.AddLast(track2);

            allplaylistcontroller.AddTrackList(testplaylist1);
            allplaylistcontroller.AddTrackList(testplaylist2);

            //allplaylistcontroller.AddTrackList(testplaylist3);
            //allplaylistcontroller.AddTrackList(testplaylist4);
            //allplaylistcontroller.AddTrackList(testplaylist5);

        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e) // makes it so that when you click the menu button it opens up, otherwise its closed
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


        private void AddPlayList_Click(object sender, RoutedEventArgs e) // creates a pop up that asks the user to input a name for the new playlist
        {
            MenuPanel.Height = 0;
            menuPenalIsOpen = false;
            string x = Interaction.InputBox("Please insert a name:", "Create playlist", "playlist name", 10, 10);

            if(x == "") // if the name field is empty or they click cancel the pop up returns an empty string
            {
                
            } else
            {
                Playlist playlist = new Playlist(1 , x, 100, DateTime.Today); // id is going to be unique when we use the database
                allplaylistcontroller.AddTrackList(playlist);
                GetPlayListsGUI();
            }



        }

        private void DeletePlayList_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.Height = 0;
            menuPenalIsOpen = false;
            Button clickedButton = sender as Button; // checks which button is pressed
            int playlistamount = allplaylistcontroller.allplaylists.playlists.Count; // counts the amount of playlists
            int buttoncontent = (int)clickedButton.Content; // puts the content of the clicked button onto an int

            
            if (MessageBox.Show("Are you sure you want to delete this playlist?", "Deleting playlist", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                for (int i = 0; i < playlistamount; i++)
                {

                    if (allplaylistcontroller.allplaylists.playlists[i].TrackListID == buttoncontent) // checks if i is equal to the pressed buttons content
                    {
                        allplaylistcontroller.RemovePlaylist(allplaylistcontroller.allplaylists.playlists[i]); // removes the button with the id of i

                        GetPlayListsGUI(); // refreshes the page
                        break; // stops the loop, if you count 5 playlists and delete one then the loop still goes on to the 5th playlist, this gives an error
                    }
                }
            }

        }
    }
}
