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
using XamlAnimatedGif;

namespace Audio_Player_NightWalk
{
    /// <summary>
    /// Interaction logic for ImageLibrary.xaml
    /// </summary>
    public partial class ImageLibrary : Page
    {
        public ImageLibrary()
        {
            InitializeComponent();

            /* Package: XamlAnimatedGif 
             * Solution to: Prevents UI from Freezing.
             * Both Author of the package and I, have no idea why this works...
             */
            AnimationBehavior.SetCacheFramesInMemory(DisplayedImage, true);
        }
    }
}
