using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    class MyQueueViewModel : Screen
    {
        PlayQueueController playQueueController = new PlayQueueController();

        private Uri _currentrackElement;

        public Uri CurrentrackElement
        {
            get { return _currentrackElement; }
            set { _currentrackElement = value; }
        }

        private StackPanel _playingNowStackPanel;


        public StackPanel PlayingNowStackPanel
        {
            get 
            { 
                if(_playingNowStackPanel == null)
                {
                    _playingNowStackPanel = new StackPanel();
                }
                return _playingNowStackPanel;
            }
            set
            {
                _playingNowStackPanel = value;
                NotifyOfPropertyChange(() => PlayingNowStackPanel);
            }
        }

        public MyQueueViewModel()
        {
            TestCurrentTrack();
        }


        public void TestCurrentTrack()
        {
            if (playQueueController.PQ.CurrentTrack != null)
            {
                Grid CurrentTrackPanel = new Grid() { HorizontalAlignment = HorizontalAlignment.Stretch };
                Button CurrentTrackBar = new Button() { HorizontalContentAlignment = HorizontalAlignment.Stretch, Content = CurrentTrackPanel };
                CurrentTrackBar.MouseDoubleClick += delegate (object sender, MouseButtonEventArgs e) { OuterClick(sender, e, "CurrentTrack"); };


                Button playCurrentTrackButton = new Button() { Content = "Play" };
                playCurrentTrackButton.Click += delegate (object sender, RoutedEventArgs e) { InnerClick(sender, e, "CurrentTrack"); };



                CurrentTrackPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2.5, GridUnitType.Star) });
                CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
                CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4.5, GridUnitType.Star) });
                CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Star) });
                CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });

                Label name = new Label() { Content = playQueueController.PQ.CurrentTrack.Name };
                Label artist = new Label() { Content = playQueueController.PQ.CurrentTrack.Artist };
                Label album = new Label() { Content = "Album" };
                Label duration = new Label() { Content = playQueueController.PQ.CurrentTrack.Duration };

                CurrentTrackPanel.Children.Add(playCurrentTrackButton);
                Grid.SetColumn(playCurrentTrackButton, 0);
                CurrentTrackPanel.Children.Add(name);
                Grid.SetColumn(name, 1);
                CurrentTrackPanel.Children.Add(artist);
                Grid.SetColumn(artist, 2);
                CurrentTrackPanel.Children.Add(album);
                Grid.SetColumn(album, 3);
                CurrentTrackPanel.Children.Add(duration);
                Grid.SetColumn(duration, 4);

                PlayingNowStackPanel.Children.Add(CurrentTrackBar);
            }
        }

        private void OuterClick(object sender, MouseButtonEventArgs e, string type)
        {

            try
            {
                MessageBox.Show($"OuterButton is double clicked at {type} index {((Button)sender).Tag.ToString()}");
            }
            catch (Exception)
            {
                MessageBox.Show($"OuterButton is double clicked at {type} ");
            }

            if (type.Equals("TrackQueue"))
                //invoke event to play the song
                if (type.Equals("TrackWaitingList"))
                    //invoke event to play the song
                    if (type.Equals("CurrentTrack"))
                        //invoke event to play the song
                        Console.WriteLine();
        }

        private void InnerClick(object sender, RoutedEventArgs e, string type)
        {
            try
            {
                MessageBox.Show($"InnerButton is clicked at {type} index {((Button)sender).Tag.ToString()}");
            }
            catch (Exception)
            {
                MessageBox.Show($"InnerButton is clicked at {type} ");
            }

            if (type.Equals("TrackQueue"))
                //invoke event to play the song
                if (type.Equals("TrackWaitingList"))
                    //invoke event to play the song
                    if (type.Equals("CurrentTrack"))
                        //invoke event to play the song
                        Console.WriteLine();

        }

    }
}
