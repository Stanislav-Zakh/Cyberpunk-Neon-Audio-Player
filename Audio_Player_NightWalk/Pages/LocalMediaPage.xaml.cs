using Audio_Player_NightWalk.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Audio_Player_NightWalk.Pages
{
    /// <summary>
    /// Interaction logic for LocalMediaPage.xaml
    /// </summary>
    public partial class LocalMediaPage : Page
    {
        public LocalMediaPage()
        {
            InitializeComponent();

            this.DataContext = new LocalMediaViewModel();
        }
    }
}
