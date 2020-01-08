using Caliburn.Micro;
using Ritmo.Database;
using Ritmo.ViewModels;
using System;
using System.Windows;

namespace Ritmo
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DatabaseConnector.ConnectSSH();
            if (true) //This is used when the user still has a running session on his device
                DisplayRootViewFor<LoginViewModel>();
            else
                DisplayRootViewFor<MainWindowViewModel>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            DatabaseConnector.DisconnectSSH();
        }

    }
}
