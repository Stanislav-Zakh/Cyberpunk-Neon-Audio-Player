using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Audio_Player_NightWalk
{
    public class PlayListViewModel : BaseViewModel, AudioTreeItem
    {

        #region Public Properties to display info

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

        private string _album = string.Empty;

        public string Album
        {
            get { return _album; }
            set
            {
                _album = value;
                OnPropertyChanged(nameof(Album));
            }
        }

        private string _year = string.Empty;

        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChanged(nameof(Year));
            }
        }

        private string _genre = string.Empty;

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


        #region User click interaction

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
                    PlayerStateViewModel.Instance.SelectPlaylist(this);                  
                }

            }
        }


        #endregion

        /// <summary>
        /// Path to the folder on the disk
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// Name of the Playlist
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Total Time of all Tracks in the Playlist
        /// </summary>
        /// 
        private string _playlistTotalTime;
        public string PlaylistTotalTime {

            get => _playlistTotalTime;
                
            set
            {
                _playlistTotalTime = value;
                OnPropertyChanged(nameof(PlaylistTotalTime));
            } 
        
        
        }

        #region Contained Track's references

        public ObservableCollectionRange<TrackViewModel> Tracks { get; set; }

        private TrackViewModel _selectedTrack;

        public TrackViewModel SelectedTrack
        {
            get { return _selectedTrack; }
            set
            {
                _selectedTrack = value;
                PlayerStateViewModel.Instance.SelectTrack(_selectedTrack);
                
            }
        }

        #endregion


        #region Constructor
        public PlayListViewModel(string path,  string name)
        {
            this.Path = path;  

            this.Name = name;

            //this.PlaylistTotalTime = new TimeSpan(0, 0, 0);

            Tracks = FileManager.GetTracks(Path, this);
        }

        #endregion



        

        #region Selecting Tracks

        public void setSelectedTrack(TrackViewModel track)
        {
            if (SelectedTrack != null)
                this.SelectedTrack.DoubleClicked = false;

            this.SelectedTrack = track;
        }


        public void GetNextTrack()
        {
            if (this.SelectedTrack == null)
                return;

           
            var ind = this.Tracks.IndexOf(this.SelectedTrack);

            if ((ind += 1) == Tracks.Count)
            {
                Tracks[0].DoubleClicked = true;
            } else
            {
                Tracks[ind].DoubleClicked = true; // TODO hardCoded remove later
            }
 
        }

        public void GetPreviousTrack()
        {
            if (this.SelectedTrack == null)
                return;

           

            var ind = this.Tracks.IndexOf(this.SelectedTrack);

            if (ind == 0)
            {
                Tracks[Tracks.Count - 1].DoubleClicked = true;
            }
            else
            {
                Tracks[ind - 1].DoubleClicked = true; // TODO hardCoded remove later
            }
        }

        #endregion


        public void AddTracks(List<TrackViewModel> newTracks)
        {
            Tracks.AddRange(newTracks);
        }


        /// <summary>
        /// Update itself and contained tracks
        /// </summary>
        /// <param name="info"></param>
        public void UpdateData(FullAudiFileInfo info)
        {

        }

        public string GetFullPath()
        {
            return this.Path;
        }


        




    }
}
