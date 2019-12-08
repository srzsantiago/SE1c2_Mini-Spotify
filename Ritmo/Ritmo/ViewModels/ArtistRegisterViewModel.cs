using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;
using GalaSoft.MvvmLight.Command;

namespace Ritmo.ViewModels
{
    public class ArtistRegisterViewModel : Screen
    {
        public ICommand CancelCommand { get; set; }
        public ICommand SendCommand { get; set; }
        public ICommand GotFocusCommand { get; set; }

        public ICommand LostFocusCommand { get; set; }

        private String _filledMail;

        public String FilledMail
        {
            get { if (_filledMail == null)
                    _filledMail = "Enter your mail here...";
                return _filledMail; }
            set { _filledMail = value;
                NotifyOfPropertyChange("FilledMail");
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { if (_errorMessage == null)
                    _errorMessage = "";
                return _errorMessage; }
            set { _errorMessage = value;
                NotifyOfPropertyChange("ErrorMessage");
            }
        }



        public ArtistRegisterViewModel()
        {
            CancelCommand = new RelayCommand(this.CancelClick);
            SendCommand = new RelayCommand(this.SendClick);
            GotFocusCommand = new RelayCommand(this.GotFocusClick);
            LostFocusCommand = new RelayCommand(this.LostFocusClick);
        }

        private void LostFocusClick()
        {
            if (string.IsNullOrWhiteSpace(FilledMail))
                FilledMail = "Enter your mail here...";
        }

        private void GotFocusClick()
        {
            if (FilledMail.Equals("Enter your mail here..."))
            {
                FilledMail = "";
            }
            if (ErrorMessage.Equals("Please, enter a valid email."))
                ErrorMessage = "";
        }

        private void SendClick()
        {
            string email = FilledMail;

            if (IsValidEmail(email))
            {
                //Ritmo.Register register = new Ritmo.Register(email);
                this.TryClose();
            }
            else
            {
                ErrorMessage = "Please, enter a valid email.";
            }

            
        }

        bool IsValidEmail(string email)
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

        private void CancelClick()
        {
            this.TryClose();
        }
    }
}
