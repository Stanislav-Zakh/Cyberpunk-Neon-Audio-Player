using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Audio_Player_NightWalk
{
    public class ArtViewModel : BaseViewModel
    {

        public BitmapImage Art { get; set; }


        
        /// <summary>
        /// Path to the Image on the disk
        /// </summary>
        /// 
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Name of the Image
        /// </summary>
        public string Name { get; set; } =  string.Empty;



        #region User click interaction

        private bool clicked;

        /// <summary>
        /// If user single-clicked on the art, display it.
        /// </summary>
        public bool Clicked
        {
            get { return clicked; }
            set
            {
                clicked = value;
                if (clicked)
                    ImageLibraryViewModel.Instance.DisplayArt(this.Path);
            }
        }

        #endregion




        public ArtViewModel(string path, string name, BitmapImage art)
        {
            Path = path;
            Name = name;
            Art = art;

            //Art.DecodeFailed += (o, e) => Trace.WriteLine("Decode Failde"); 
        }



    }
}
