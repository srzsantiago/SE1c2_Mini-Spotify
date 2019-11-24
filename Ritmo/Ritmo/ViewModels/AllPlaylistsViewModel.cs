﻿using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    class AllPlaylistsViewModel : Screen
    {
        public MainWindowViewModel MainWindow { get; set; }
        public PlaylistViewModel PlaylistViewModel { get; set; } = new PlaylistViewModel();
        public ICommand OpenPlaylistViewModelCommand { get; set; }

        public AllPlaylistsViewModel(MainWindowViewModel MainWindow)
        {
            OpenPlaylistViewModelCommand = new RelayCommand<object>(OpenPlaylistViewModel);
            this.MainWindow = MainWindow;
        }

        //Sets CurrentViewModel in the MainWindow to a PlaylistViewModel
        public void OpenPlaylistViewModel(object PlaylistViewModel)
        {
            MainWindow.ChangeViewModel((Screen)PlaylistViewModel);
        }
    }
}
