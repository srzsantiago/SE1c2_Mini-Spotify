﻿using System.Windows;
using System.Windows.Controls;


namespace Ritmo.Views
{
    /// <summary>
    /// Interaction logic for MyQueueView.xaml
    /// </summary>
    public partial class MyQueueView : UserControl
    {
        public MyQueueView()
        {
            InitializeComponent();
        }

        private void AddPresetButton_Click(object sender, RoutedEventArgs e)
        {
            var addButton = sender as FrameworkElement;
            if (addButton != null)
            {
                addButton.ContextMenu.IsOpen = true;
            }
        }



    }
    }
