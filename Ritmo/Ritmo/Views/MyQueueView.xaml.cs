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
        PlayQueueController playQueueController = new PlayQueueController();
        public MyQueueView()
        {
            InitializeComponent();
            this.DataContext = playQueueController;


            
            if (playQueueController.PQ.CurrentTrack != null)
            {
                Grid CurrentTrackPanel = new Grid() { HorizontalAlignment = HorizontalAlignment.Stretch };
                Button CurrentTrackBar = new Button() { HorizontalContentAlignment = HorizontalAlignment.Stretch, Content = CurrentTrackPanel };
                CurrentTrackBar.Click += OuterClick;

                Button playCurrentTrackButton = new Button() { Content = "Play" };
                playCurrentTrackButton.Click += InnerClick;

                CurrentTrackPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                CurrentTrackPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });

                Label name = new Label() { Content = "" };
                Label artist = new Label() { Content = "" };
                Label album = new Label() { Content = "Album" };
                Label duration = new Label() { Content = "" };

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



            if (playQueueController.PQ.TrackQueue.Count > 0)
            {
                foreach (var item in playQueueController.PQ.TrackQueue)
                {
                    Grid QueueItemPanel = new Grid() { HorizontalAlignment = HorizontalAlignment.Stretch };
                    Button QueueItemBar = new Button() { HorizontalContentAlignment = HorizontalAlignment.Stretch, Content = QueueItemPanel };
                    QueueItemBar.Click += OuterClick;

                    Button playQueueItemButton = new Button() { Content = "Play" };
                    playQueueItemButton.Click += InnerClick;

                    QueueItemPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                    QueueItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                    QueueItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                    QueueItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                    QueueItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                    QueueItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });

                    Label name = new Label() { Content = item.Name };
                    Label artist = new Label() { Content = item.Artist };
                    Label album = new Label() { Content = "Album" };
                    Label duration = new Label() { Content = item.Duration };

                    QueueItemPanel.Children.Add(playQueueItemButton);
                    Grid.SetColumn(playQueueItemButton, 0);
                    QueueItemPanel.Children.Add(name);
                    Grid.SetColumn(name, 1);
                    QueueItemPanel.Children.Add(artist);
                    Grid.SetColumn(artist, 2);
                    QueueItemPanel.Children.Add(album);
                    Grid.SetColumn(album, 3);
                    QueueItemPanel.Children.Add(duration);
                    Grid.SetColumn(duration, 4);

                    NextInQueueStackPanel.Children.Add(QueueItemBar);
                }
            }
            if (playQueueController.PQ.TrackWaitingList.Count > 0)
            {
                foreach (var item in playQueueController.PQ.TrackWaitingList)
                {
                    Grid waitingListItemPanel = new Grid() { HorizontalAlignment = HorizontalAlignment.Stretch };
                    Button waitingListItemBar = new Button() { HorizontalContentAlignment = HorizontalAlignment.Stretch, Content = waitingListItemPanel };
                    waitingListItemBar.Click += OuterClick;
                    
                    Button playWaitingListItemButton = new Button() { Content = "Play"};
                    playWaitingListItemButton.Click += InnerClick;

                    waitingListItemPanel.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30) });
                    waitingListItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                    waitingListItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                    waitingListItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                    waitingListItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                    waitingListItemPanel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Star) });
                    
                    Label name = new Label() { Content = item.Name };
                    Label artist = new Label() { Content = item.Artist };
                    Label album = new Label() { Content = "Album" };
                    Label duration = new Label() { Content = item.Duration };

                    waitingListItemPanel.Children.Add(playWaitingListItemButton);
                    Grid.SetColumn(playWaitingListItemButton, 0);
                    waitingListItemPanel.Children.Add(name);
                    Grid.SetColumn(name, 1);
                    waitingListItemPanel.Children.Add(artist);
                    Grid.SetColumn(artist, 2);
                    waitingListItemPanel.Children.Add(album);
                    Grid.SetColumn(album, 3);
                    waitingListItemPanel.Children.Add(duration);
                    Grid.SetColumn(duration, 4);
                   
                    NextUpStackPanel.Children.Add(waitingListItemBar);
                }
            }
        }

        private void OuterClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("OuterButton is clicked!");
        }
        private void InnerClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("InnerButton is clicked!");
            e.Handled = true;
           
        }


    }
}
