using Caliburn.Micro;
using Ritmo.Database;
using Ritmo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //DisplayRootViewFor<Login>();
            //DisplayRootViewFor<RegisterViewModel>();
            //DisplayRootViewFor<ArtistRegisterViewModel>();
            DisplayRootViewFor<MainWindowViewModel>();
            //DatabaseConnector.ConnectSSH();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            //DatabaseConnector.DisconnectSSH();
        }

    }
}
