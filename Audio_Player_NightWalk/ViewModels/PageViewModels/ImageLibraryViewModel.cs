using Audio_Player_NightWalk.BaseClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using XamlAnimatedGif;

namespace Audio_Player_NightWalk
{
    public class ImageLibraryViewModel : SingeltonViewModel<ImageLibraryViewModel>
    {

        private string _displayedArt = string.Empty;


        /// <summary>
        /// URi to the art 
        /// </summary>

        public string DisplayedArt
        {
            get { return _displayedArt; }
            set
            {
                _displayedArt = value;
                OnPropertyChanged(nameof(DisplayedArt));

            }
        }

        public ObservableCollectionRange<ArtViewModel> ArtWorks { get; set; }


        #region Commands

        public ICommand AddArtWorks { get; set; }

        /// <summary>
        /// Flips the visibility of the ArtSelector.
        /// </summary>
        public ICommand XORSelectorVisibility { get; set; }

        #endregion


        private ImageLibraryViewModel()
        {

            ArtWorks = FileManager.GetArtWorks();

            AddArtWorks = new RelayCommand(() => FileManager.OpenFileDialogAddArtWorks(ArtWorks));

            XORSelectorVisibility = new RelayCommand(() => ApplicationViewModel.Instance.ArtSelectorVisible ^= System.Windows.Visibility.Collapsed);

        }


        public void DisplayArt(string path)
        {
            DisplayedArt = path;
        }


        




    }
}
