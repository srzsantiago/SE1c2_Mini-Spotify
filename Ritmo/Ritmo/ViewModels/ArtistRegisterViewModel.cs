using System;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;

namespace Ritmo.ViewModels
{
    public class ArtistRegisterViewModel : Screen
    {
        private IWindowManager _manager = new WindowManager();

        #region commands
        public ICommand CancelCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand GotFocusCommand { get; set; }
        public ICommand LostFocusCommand { get; set; }
        #endregion

        #region labeltext
        private string _filledMail;
        private string _errorMessage;

        public string FilledMail
        {
            get
            {
                if (_filledMail == null)
                    _filledMail = "Enter your mail here...";
                return _filledMail;
            }
            set
            {
                _filledMail = value;
                NotifyOfPropertyChange("FilledMail");
            }
        }

        public string ErrorMessage
        {
            get
            {
                if (_errorMessage == null)
                    _errorMessage = "";
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange("ErrorMessage");
            }
        }
        #endregion


        public ArtistRegisterViewModel()
        {
            CancelCommand = new RelayCommand(CancelClick);
            SendCommand = new RelayCommand(SendClick);
            GotFocusCommand = new RelayCommand(GotFocusClick);
            LostFocusCommand = new RelayCommand(LostFocusClick);
        }

        private void LostFocusClick()//set placeholder of Email Texbox if its empty
        {
            if (string.IsNullOrWhiteSpace(FilledMail))
                FilledMail = "Enter your mail here...";
        }

        private void GotFocusClick()//placeholder of Email Textbox
        {
            if (FilledMail.Equals("Enter your mail here..."))
            {
                FilledMail = "";
            }
            if (ErrorMessage.Equals("Please, enter a valid email."))
                ErrorMessage = "";
        }

        private void SendClick()//send request for artist account
        {
            string email = FilledMail;

            if (IsValidEmail(email))//check the email format
            {
                //Ritmo.Register register = new Ritmo.Register(email);
                TryClose();
            }
            else
            {
                ErrorMessage = "Please, enter a valid email.";
            }


        }

        bool IsValidEmail(string email)//check the email format
        {
            try
            {
                Regex rx = new Regex
                    (@"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
                return rx.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        private void CancelClick()//cancel button
        {
            _manager.ShowWindow(new RegisterViewModel());
            TryClose();
        }
    }
}
