using Caliburn.Micro;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Ritmo.ViewModels
{
    class LoginViewModel : Screen
    {
        public ICommand LoginCommand { get; set; }
        public ICommand NewAccountCommand { get; set; }

        IWindowManager Manager = new WindowManager();

        #region XAML Properties
        private string _filledPassword;
        private string _filledEmail;
        private string _loginMessage;
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
            LoginCommand = new RelayCommand<object>(Login);
            NewAccountCommand = new RelayCommand<Window>(NewAccount);
        }

        //LoginView is given as object parameters which contains the loginView and the passwordbox from the xaml
        private void Login(object parameters)
        {
            var values = parameters as List<object>; //set the multiple parameters in one array
            var LoginView = values[0] as Window; //set the first value in the array as passwordbox
            var PasswordBox = values[1] as PasswordBox;//set the second value in the array as confirmpasswordbox

            Login LoginAttempt = new Login(FilledEmail, PasswordBox.Password); //try to login with the given mail and password

            ErrorColor = Brushes.LightYellow;

            if (LoginAttempt.loggedin == true)//Authentication is succesful.
            {
                LoginMessage = "Success";
                Manager.ShowWindow(new MainWindowViewModel());
                LoginView.Close();
            }
            else
                LoginMessage = "Failed, incorrect email or password";
        }

        //LoginView is given as argument to close the view in code
        private void NewAccount(Window LoginView)
        {
            Manager.ShowWindow(new RegisterViewModel());

            LoginView.Close();
        }
        
    }
}
