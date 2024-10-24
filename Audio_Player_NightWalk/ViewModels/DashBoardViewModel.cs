using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Audio_Player_NightWalk
{
    public class DashBoardViewModel : BaseViewModel
    {

        public static readonly DashBoardViewModel Instance = new DashBoardViewModel();

      
        #region Properties

        private string _title = "Not Selected";

        public string Title 
        {
            get { return _title; }
            set {     
                _title = value; 
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _duration = "Not Selected";

        public string Duration
        {
            get { return _duration; }
            set
            {
                _duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        private string _artist = "Not Selected";

        public string Artist
        {
            get { return _artist; }
            set
            {
                _artist = value;
                OnPropertyChanged(nameof(Artist));
            }
        }

        private string _genre = "Not Selected";

        public string Genre
        {
            get { return _genre; }
            set
            {
                _genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }


        private ImageSource _cover = null;

        public ImageSource Cover
        {
            get { return _cover; }
            set
            {
                _cover = value;
                OnPropertyChanged(nameof(Cover));
            }
        }


        #endregion



        public DashBoardViewModel()
        {
            
            TagReader.NewFileSelected += () =>
            {
                Title = TagReader.SelectedFile.Title;
                Duration = TagReader.SelectedFile.Duration;
                Artist = TagReader.SelectedFile.Artist;
                Genre = TagReader.SelectedFile.Genre;

                if (TagReader.SelectedFile.Cover != null)
                    Cover = TagReader.SelectedFile.Cover;
            };
   
        }







    }
}
