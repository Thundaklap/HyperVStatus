using System.Windows;


namespace HyperVStatus
{
    public partial class App : Application
    {
        void AppStartup(object sender, StartupEventArgs args)
        {
            Window1 mainWindow = new Window1();
            mainWindow.Show();
        }
    }
}