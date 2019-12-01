using Caliburn.Micro;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Ritmo.ViewModels
{
    class LoginViewModel : Screen
    {
        public ICommand LoginCommand { get; set; }
        public ICommand NewAccountCommand { get; set; }

        #region Properties
        private string _filledPassword;
        private string _filledEmail;
        private string _loginMessage = "kaas";
        private Brush _errorColor;

        public Brush ErrorColor
        {
            get { return _errorColor; }
            set { _errorColor = value;
                NotifyOfPropertyChange();
            }
        }
        public string LoginMessage
        {
            get { return _loginMessage; }
            set
            {
                _loginMessage = value;
                NotifyOfPropertyChange();
            }
        }
        public string FilledEmail
        {
            get { return _filledEmail; }
            set { _filledEmail = value; }
        }
        public string FilledPassword
        {
            get { return _filledPassword; }
            set { _filledPassword = value; }
        }
        #endregion

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
            NewAccountCommand = new RelayCommand<Window>(NewAccount);
        }

        private void Login()
        {
            Login loginAttemp = new Login(FilledEmail, FilledPassword);

            ErrorColor = Brushes.LightYellow;

            if (loginAttemp.loggedin == true)
                LoginMessage = "Success";
            else
                LoginMessage = "Failed, incorrect email or password";
        }

        private void NewAccount(Window LoginView)
        {
            //Window RegisterView = new Window() { Content = new RegisterViewModel() };
            //RegisterView.Show();

            //LoginView.Close();
        }
        
    }
}
