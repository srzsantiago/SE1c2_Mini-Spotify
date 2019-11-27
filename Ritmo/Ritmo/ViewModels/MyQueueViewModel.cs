using Caliburn.Micro;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class MyQueueViewModel : Screen
    {
        PlayQueueController playQueueController = new PlayQueueController();
        int count;

        public MyQueueViewModel()
        {
            TestCurrentTrack();
            _outerClickCommand = new RelayCommand<object>(this.OuterClick);
            _innerClickCommand = new RelayCommand<object>(this.InnerClick);
        }

        #region Observable collections
        private ObservableCollection<MyQueueItem> _playingNowItems;
        private ObservableCollection<MyQueueItem> _nextInQueueItems;
        private ObservableCollection<MyQueueItem> _nextUpItems;

        public ObservableCollection<MyQueueItem> PlayingNowItems
        {
            get { return _playingNowItems; }
            set { _playingNowItems = value; }
        }

        public ObservableCollection<MyQueueItem> NextInQueueItems
        {
            get { return _nextInQueueItems; }
            set { _nextInQueueItems = value; }
        }

        public ObservableCollection<MyQueueItem> NextUpItems
        {
            get { return _nextUpItems; }
            set { _nextUpItems = value; }
        }

        #endregion


        private ICommand _outerClickCommand;

        public ICommand OuterClickCommand
        {
            get
            {
                return _outerClickCommand;
            }
            set
            {
                _outerClickCommand = value;
            }
        }

        private ICommand _innerClickCommand;

        public ICommand InnerClickCommand
        {
            get
            {
                return _innerClickCommand;
            }
            set
            {
                _innerClickCommand = value;
            }
        }

        public void TestCurrentTrack()
        {
            //commands
            //initializate item

            if (playQueueController.PQ.CurrentTrack != null)
            {
                PlayingNowItems = new ObservableCollection<MyQueueItem>()
                {
                new MyQueueItem()
                {
                    Name= playQueueController.PQ.CurrentTrack.Name,
                    Artist=playQueueController.PQ.CurrentTrack.Artist,
                    Album= "Album",
                    Duration= playQueueController.PQ.CurrentTrack.Duration
                }
                };
            }

            if (playQueueController.PQ.TrackQueue.Count > 0)
            {
                NextInQueueItems = new ObservableCollection<MyQueueItem>();
                count = 0;
                foreach (var item in playQueueController.PQ.TrackQueue)
                {
                    NextInQueueItems.Add(new MyQueueItem()
                    {
                        playButtonID = count,
                        Name = item.Name,
                        Artist = item.Artist,
                        Album = "Album",
                        Duration = item.Duration
                    }); ;
                }
            }

            if (playQueueController.PQ.TrackWaitingList.Count > 0)
            {
                NextUpItems = new ObservableCollection<MyQueueItem>();
                count = 0;
                foreach (var item in playQueueController.PQ.TrackWaitingList)
                {
                    NextUpItems.Add(new MyQueueItem()
                    {
                        playButtonID = count,
                        Name = item.Name,
                        Artist = item.Artist,
                        Album = "Album",
                        Duration = item.Duration
                    });
                }
            }
            //    Label name = new Label() { Content = playQueueController.PQ.CurrentTrack.Name };
            //    Label artist = new Label() { Content = playQueueController.PQ.CurrentTrack.Artist };
            //    Label album = new Label() { Content = "Album" };
            //    Label duration = new Label() { Content = playQueueController.PQ.CurrentTrack.Duration };




            //if (playQueueController.PQ.CurrentTrack != null)
            //{
            //    Grid CurrentTrackPanel = new Grid() { HorizontalAlignment = HorizontalAlignment.Stretch };
            //    Button CurrentTrackBar = new Button() { HorizontalContentAlignment = HorizontalAlignment.Stretch, Content = CurrentTrackPanel };
            //    CurrentTrackBar.MouseDoubleClick += delegate (object sender, MouseButtonEventArgs e) { OuterClick(sender, e, "CurrentTrack"); };


            //    Button playCurrentTrackButton = new Button() { Content = "Play" };
            //    playCurrentTrackButton.Click += delegate (object sender, RoutedEventArgs e) { InnerClick(sender, e, "CurrentTrack"); };



            //    CurrentTrackPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
            //    CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2.5, GridUnitType.Star) });
            //    CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
            //    CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4.5, GridUnitType.Star) });
            //    CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Star) });
            //    CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });

            //    Label name = new Label() { Content = playQueueController.PQ.CurrentTrack.Name };
            //    Label artist = new Label() { Content = playQueueController.PQ.CurrentTrack.Artist };
            //    Label album = new Label() { Content = "Album" };
            //    Label duration = new Label() { Content = playQueueController.PQ.CurrentTrack.Duration };

            //    CurrentTrackPanel.Children.Add(playCurrentTrackButton);
            //    Grid.SetColumn(playCurrentTrackButton, 0);
            //    CurrentTrackPanel.Children.Add(name);
            //    Grid.SetColumn(name, 1);
            //    CurrentTrackPanel.Children.Add(artist);
            //    Grid.SetColumn(artist, 2);
            //    CurrentTrackPanel.Children.Add(album);
            //    Grid.SetColumn(album, 3);
            //    CurrentTrackPanel.Children.Add(duration);
            //    Grid.SetColumn(duration, 4);

            //    PlayingNowStackPanel.Children.Add(CurrentTrackBar);
            //}
        }

        private void OuterClick(Object sender)
        {
            
            System.Windows.MessageBox.Show($"OuterButton is double clicked at {(string)sender}");

            //try
            //{
            //    MessageBox.Show($"OuterButton is double clicked at {type} index");
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show($"OuterButton is double clicked at {type} ");
            //}

            //if (type.Equals("TrackQueue"))
            //    //invoke event to play the song
            //    if (type.Equals("TrackWaitingList"))
            //        //invoke event to play the song
            //        if (type.Equals("CurrentTrack"))
            //            //invoke event to play the song
            //            Console.WriteLine();
        }

        private void InnerClick(object sender)
        {
            System.Windows.MessageBox.Show($"Innerclick is clicked at {(string)sender}");


            //try
            //{
            //    MessageBox.Show($"InnerButton is clicked at {type} index {((Button)sender).Tag.ToString()}");
            //}
            //catch (Exception)
            //{
            //    MessageBox.Show($"InnerButton is clicked at {type} ");
            //}

            //if (type.Equals("TrackQueue"))
            //    //invoke event to play the song
            //    if (type.Equals("TrackWaitingList"))
            //        //invoke event to play the song
            //        if (type.Equals("CurrentTrack"))
            //            //invoke event to play the song
            //            Console.WriteLine();

        }

    }

    public class MyQueueItem
    {
        public int playButtonID { get; set; }
        public String Name { get; set; }
        public String Artist { get; set; }
        public String Album { get; set; }
        public int Duration { get; set; }
    }
}
