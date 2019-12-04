using Caliburn.Micro;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ritmo.ViewModels
{
    public class PopUpWindowViewModel : Screen
    {
        IWindowManager WindowManager = new WindowManager();

        #region Command
        public ICommand TransferCommand;
        #endregion

        #region Popupwindow Attributes
        private AllPlaylistsViewModel _viewModel;
        private string _transferString;
        private string _message;
        private string _textBoxMessage;
        private string _buttonContent;

        public string ButtonContent
        {
            get { return _buttonContent; }
            set { _buttonContent = value; }
        }

        public string TextBoxMessage
        {
            get { return _textBoxMessage; }
            set { _textBoxMessage = value; }
        }

        public string TransferString
        {
            get { return _transferString; }
            set { _transferString = value; }
        }

        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        #endregion

        public AllPlaylistsViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        //Constructor for AllPlaylistsViewModel
        public PopUpWindowViewModel(AllPlaylistsViewModel viewModel)
        {
            ViewModel = viewModel;

            TransferCommand = new RelayCommand(Transfer);

            ButtonContent = "Add playlist";
            TextBoxMessage = "Enter playlist name...";
        }

        private void Transfer()
        {
            ViewModel.AddPlaylist("Test");
        }

    }
}
