using Audio_Player_NightWalk.BaseClasses;
using Audio_Player_NightWalk.DataModel.Enums;
using System.Windows.Input;


namespace Audio_Player_NightWalk
{
    public class ApplicationViewModel : BaseViewModel
    {

        public static ApplicationViewModel Instance { get; private set; } = new ApplicationViewModel();

        private CurrentPageType _selectedPage = CurrentPageType.ImageViewer;

        public CurrentPageType SelectedPage
        {
			get { return _selectedPage; }
			set {
                
                _selectedPage = value; 
                OnPropertyChanged(nameof(SelectedPage));

            }
		}
    

        public ICommand DisplayAlbum { get; set; }
        public ICommand DisplayDetails { get; set; }
        public ICommand DisplayImages { get; set; }
    

        private ApplicationViewModel()
        {
            DisplayAlbum = new RelayCommand(() => this.SelectedPage = CurrentPageType.ImageViewer);
            DisplayImages = new RelayCommand(() => this.SelectedPage = CurrentPageType.LocalMedia);
            DisplayDetails =  new RelayCommand(() => this.SelectedPage = CurrentPageType.Details);

        }



    }
}
