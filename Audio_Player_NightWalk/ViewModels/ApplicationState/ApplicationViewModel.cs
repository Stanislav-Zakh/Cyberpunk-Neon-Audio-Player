using Audio_Player_NightWalk.BaseClasses;
using Audio_Player_NightWalk.DataModel.Enums;
using System.Windows;
using System.Windows.Input;


namespace Audio_Player_NightWalk
{
    public class ApplicationViewModel : BaseViewModel
    {

        public static ApplicationViewModel Instance { get; private set; } = new ApplicationViewModel();

        private CurrentPageType _selectedPage = CurrentPageType.Album;

        public CurrentPageType SelectedPage
        {
			get { return _selectedPage; }
			set {
                
                _selectedPage = value; 
                OnPropertyChanged(nameof(SelectedPage));

            }
		}

        /// <summary>
        /// 
        /// </summary>

        private Visibility _artSelectorVisible = Visibility.Collapsed;

        public Visibility ArtSelectorVisible
        {
            get { return _artSelectorVisible; }
            set
            {
                _artSelectorVisible = value;
                OnPropertyChanged(nameof(ArtSelectorVisible));

            }
        }


        public ICommand DisplayAlbum { get; set; }
        public ICommand DisplayDetails { get; set; }
        public ICommand DisplayImages { get; set; }
    

        private ApplicationViewModel()
        {
            DisplayAlbum = new RelayCommand(() => this.SelectedPage = CurrentPageType.Album);
            DisplayImages = new RelayCommand(() => this.SelectedPage = CurrentPageType.ImageLibrary);
            DisplayDetails =  new RelayCommand(() => this.SelectedPage = CurrentPageType.Visualizer);

        }



    }
}
