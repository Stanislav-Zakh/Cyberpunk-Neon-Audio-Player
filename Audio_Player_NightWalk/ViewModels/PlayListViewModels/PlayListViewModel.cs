using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Audio_Player_NightWalk
{
    public class PlayListViewModel : BaseViewModel
    {

        #region Current Track Display

        private string _title = "Not Selected";

        public string Title
        {
            get { return _title; }
            set
            {
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


        #region 

        private bool _doubleClicked = false;

        /// <summary>
        /// Response to the user double-clicking on the playlist. 
        /// Sets Playlist as selected.
        /// </summary>
        public bool DoubleClicked
        {
            get { return _doubleClicked; }
            set 
            { 
                _doubleClicked = value;
                OnPropertyChanged(nameof(DoubleClicked));

                if (_doubleClicked)
                {
                    PlayerStateViewModel.PlayerState.SelectedPlaylist = this;
                  
                }

            }
        }


        #endregion

        public string Path { get; set; } = string.Empty;

        public string Name { get; set; }

        public TimeSpan PlaylistTotalTime { get; set; }

        public ObservableCollectionRange<TrackViewModel> Tracks { get; set; }


        private bool clicked;

        public bool Clicked
        {
            get { return clicked; }
            set
            {
                
                
                clicked = value;
                if (clicked)
                    // call tag reader to extract info
                    // call ios to update dassboard viewmodel view
                   Trace.WriteLine($"{Name}: I am Clicked");
            }
        }


        public PlayListViewModel(string path,  string name)
        {
            this.Path = path;  

            this.Name = name;

            this.PlaylistTotalTime = new TimeSpan(0, 0, 0);

            Tracks = FileManager.GetTracks(Path, this);          
        }



        public void AddTracks(List<TrackViewModel> newTracks)
        {
            Tracks.AddRange(newTracks);
        }



    }
}
