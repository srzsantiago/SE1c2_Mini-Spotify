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


            
            if (playQueueController.playQueue.CurrentTrack != null)
            {
                Button currentTrackBar = new Button() { Height = 30 };
                currentTrackBar.Click += OuterClick;

                Button playCurrentButton = new Button() { Content = "Play" };
                playCurrentButton.Click += InnerClick;

                StackPanel panelCurrentrack = new StackPanel() { Orientation = Orientation.Horizontal };
                panelCurrentrack.Children.Add(playCurrentButton);
                panelCurrentrack.Children.Add(new Label() { Content = "Naam" });
                panelCurrentrack.Children.Add(new Label() { Content = "Artist" });
                panelCurrentrack.Children.Add(new Label() { Content = "Album" });
                panelCurrentrack.Children.Add(new Label() { Content = "Duration" });

                currentTrackBar.Content = panelCurrentrack;
                PlayingNowStackPanel.Children.Add(currentTrackBar);

               
            }



            if (playQueueController.playQueue.TrackQueue.Count > 0)
            {
                foreach (var item in playQueueController.playQueue.TrackQueue)
                {
                    NextInQueueStackPanel.Children.Add(new Label() { Content = item.Name });
                }
            }
            if (playQueueController.playQueue.TrackWaitingList.Count > 0)
            {
                foreach (var item in playQueueController.playQueue.TrackWaitingList)
                {
                    NextUpStackPanel.Children.Add(new Label() { Content = item.Name });
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
