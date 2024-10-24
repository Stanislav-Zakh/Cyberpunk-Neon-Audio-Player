using System.Configuration;
using System.Data;
using System.Windows;

namespace Audio_Player_NightWalk
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            FileManager.CreateDefaultDirectoryIfNotExist();

            this.MainWindow = new MainWindow();
            this.MainWindow.Show();

        }
    }

}
