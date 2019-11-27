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
    /// Interaction logic for MyQueueView.xaml
    /// </summary>
    public partial class MyQueueView : UserControl
    {
        //PlayQueueController playQueueController = new PlayQueueController();
        //int count;
        public MyQueueView()
        {
            InitializeComponent();
            //    this.DataContext = playQueueController;







            //    if (playQueueController.PQ.TrackQueue.Count > 0)
            //    {
            //        count = 0;
            //        foreach (var item in playQueueController.PQ.TrackQueue)
            //        {
            //            Grid QueueItemPanel = new Grid() { HorizontalAlignment = HorizontalAlignment.Stretch };
            //            Button QueueItemBar = new Button() { HorizontalContentAlignment = HorizontalAlignment.Stretch, Content = QueueItemPanel };
            //            QueueItemBar.MouseDoubleClick += delegate (object sender, MouseButtonEventArgs e) { OuterClick(sender, e, "TrackQueue"); };
            //            QueueItemBar.Tag = count;


            //            Button playQueueItemButton = new Button() { Content = "Play" };
            //            playQueueItemButton.Click += delegate (object sender, RoutedEventArgs e) { InnerClick(sender, e, "TrackQueue"); };
            //            playQueueItemButton.Tag = count;

            //            QueueItemPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
            //            QueueItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2.5, GridUnitType.Star) });
            //            QueueItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
            //            QueueItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4.5, GridUnitType.Star) });
            //            QueueItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Star) });
            //            QueueItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });

            //            Label name = new Label() { Content = item.Name };
            //            Label artist = new Label() { Content = item.Artist };
            //            Label album = new Label() { Content = "Album" };
            //            Label duration = new Label() { Content = item.Duration };

            //            QueueItemPanel.Children.Add(playQueueItemButton);
            //            Grid.SetColumn(playQueueItemButton, 0);
            //            QueueItemPanel.Children.Add(name);
            //            Grid.SetColumn(name, 1);
            //            QueueItemPanel.Children.Add(artist);
            //            Grid.SetColumn(artist, 2);
            //            QueueItemPanel.Children.Add(album);
            //            Grid.SetColumn(album, 3);
            //            QueueItemPanel.Children.Add(duration);
            //            Grid.SetColumn(duration, 4);

            //            NextInQueueStackPanel.Children.Add(QueueItemBar);
            //            count++;
            //        }
            //    }
            //    if (playQueueController.PQ.TrackWaitingList.Count > 0)
            //    {
            //        count = 0;
            //        foreach (var item in playQueueController.PQ.TrackWaitingList)
            //        {
            //            Grid waitingListItemPanel = new Grid() { HorizontalAlignment = HorizontalAlignment.Stretch };
            //            Button waitingListItemBar = new Button() { HorizontalContentAlignment = HorizontalAlignment.Stretch, Content = waitingListItemPanel };
            //            waitingListItemBar.MouseDoubleClick += delegate (object sender, MouseButtonEventArgs e) { OuterClick(sender, e, "TrackQueue"); };
            //            waitingListItemBar.Tag = count;

            //            Button playWaitingListItemButton = new Button() { Content = "Play"};
            //            playWaitingListItemButton.Click += delegate (object sender, RoutedEventArgs e) { InnerClick(sender, e, "TrackWaitingList"); };
            //            playWaitingListItemButton.Tag = count;

            //            waitingListItemPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
            //            waitingListItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2.5, GridUnitType.Star) });
            //            waitingListItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(6, GridUnitType.Star) });
            //            waitingListItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(4.5, GridUnitType.Star) });
            //            waitingListItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(5, GridUnitType.Star) });
            //            waitingListItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });

            //            Label name = new Label() { Content = item.Name };
            //            Label artist = new Label() { Content = item.Artist };
            //            Label album = new Label() { Content = "Album" };
            //            Label duration = new Label() { Content = item.Duration };

            //            waitingListItemPanel.Children.Add(playWaitingListItemButton);
            //            Grid.SetColumn(playWaitingListItemButton, 0);
            //            waitingListItemPanel.Children.Add(name);
            //            Grid.SetColumn(name, 1);
            //            waitingListItemPanel.Children.Add(artist);
            //            Grid.SetColumn(artist, 2);
            //            waitingListItemPanel.Children.Add(album);
            //            Grid.SetColumn(album, 3);
            //            waitingListItemPanel.Children.Add(duration);
            //            Grid.SetColumn(duration, 4);

            //            NextUpStackPanel.Children.Add(waitingListItemBar);
            //            count++;
            //        }
            //    }
            //}

            //private void OuterClick(object sender, MouseButtonEventArgs e, string type)
            //{

            //    try
            //    {
            //        MessageBox.Show($"OuterButton is double clicked at {type} index {((Button)sender).Tag.ToString()}");
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show($"OuterButton is double clicked at {type} ");
            //    }

            //    if(type.Equals("TrackQueue"))
            //        //invoke event to play the song
            //    if(type.Equals("TrackWaitingList"))
            //        //invoke event to play the song
            //    if(type.Equals("CurrentTrack"))
            //        //invoke event to play the song
            //        Console.WriteLine();
            //}

            //private void InnerClick(object sender, RoutedEventArgs e, string type)
            //{
            //    try
            //    {
            //        MessageBox.Show($"InnerButton is clicked at {type} index {((Button)sender).Tag.ToString()}");
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show($"InnerButton is clicked at {type} ");
            //    }

            //    if (type.Equals("TrackQueue"))
            //        //invoke event to play the song
            //    if (type.Equals("TrackWaitingList"))
            //        //invoke event to play the song
            //    if (type.Equals("CurrentTrack"))
            //        //invoke event to play the song
            //        Console.WriteLine();

        }




    }
    }
