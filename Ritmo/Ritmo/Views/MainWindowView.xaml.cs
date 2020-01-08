using System.Windows;
using System.Windows.Input;
using System.Windows.Shell;

namespace Ritmo.Views
{
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            SetWindowChrome();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight - 9; //Fixes fullscreen bug
            MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth - 9;
        }

        //Sets the style of the exterior of the window
        private void SetWindowChrome()
        {
            WindowChrome windowChrome = new WindowChrome();
            windowChrome.ResizeBorderThickness = new Thickness(4);
            windowChrome.CaptionHeight = 0;
            windowChrome.GlassFrameThickness = new Thickness(0);
            windowChrome.CornerRadius = new CornerRadius(2);
            WindowChrome.SetWindowChrome(this, windowChrome);
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Title_Bar_Drag(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
