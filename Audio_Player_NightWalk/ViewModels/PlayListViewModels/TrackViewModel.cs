using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Audio_Player_NightWalk
{
    public class TrackViewModel : BaseViewModel, AudioTreeItem
    {

        #region private fields

        private bool _firstTimeTags = true;

        private AudioFileInfo _audiFileInfo;

        #endregion


        #region User click interaction

        private bool clicked;

        /// <summary>
        /// If user single-clicked on the track, get ad display information.
        /// </summary>
        public bool Clicked
        {
            get { return clicked; }
            set
            {

                clicked = value;
                if (clicked)
                    readTagData(true);

                PlayListManagerViewModel.Instance.SetFocusedTrack(this);

            }
        }

        private bool _doubleClicked = false;

        public bool DoubleClicked
        {
            get { return _doubleClicked; }
            set { 
                _doubleClicked = value;

                OnPropertyChanged(nameof(DoubleClicked));

                if (!DoubleClicked)
                    return;

                ParentPlayList.setSelectedTrack(this);
                
            }
        }


        #endregion

        public PlayListViewModel ParentPlayList {  get; set; }
        public string Name { get; set; }

        public string FileName { get; set; }

        private string _trackTime;

        public string TrackTime
        {
            get => _trackTime;
            set
            {
                _trackTime = value;
                OnPropertyChanged(nameof(TrackTime));
            }
        }

        #region Constructor
        public TrackViewModel(string name, PlayListViewModel parentRef)
        {

            this.FileName = name;

            this.Name = name.Substring(0, name.LastIndexOf("."));

            this.ParentPlayList = parentRef;
        }

        #endregion


        #region Working with Tags
        /// <summary>
        /// Reads tag Data from the file and (if passed in param is true) assigns data to the parent Playlist. 
        /// </summary>
        /// <param name="assignToParent"></param>
        private void readTagData(bool assignToParent)
        {
            if (_firstTimeTags)
            {
                this._audiFileInfo = TagReader.LoadTagInfo(this.GetFullPath());
                _firstTimeTags = false;
            }

            if (assignToParent)
                assignTagDataToPlaylist();

        }

        /// <summary>
        /// Get Tag Data from the Track ViewModel
        /// </summary>
        /// <returns></returns>
        public AudioFileInfo getTagData()
        {
            if (_firstTimeTags)
            {
                this._audiFileInfo = TagReader.LoadTagInfo(this.GetFullPath());
                _firstTimeTags = false;
            }
                
                
            return _audiFileInfo;
        }

        #endregion


        #region Utility

        /// <summary>
        /// Assign Tag data to the parent playlist
        /// </summary>
        private void assignTagDataToPlaylist()
        {
           
            ParentPlayList.Title = this._audiFileInfo.Title;
            ParentPlayList.Artist = this._audiFileInfo.Artist;
            ParentPlayList.Duration = this._audiFileInfo.Duration;
            ParentPlayList.Album = this._audiFileInfo.Album;
            ParentPlayList.Year = this._audiFileInfo.Year;
            ParentPlayList.Genre = this._audiFileInfo.Genre;
            ParentPlayList.Cover = this._audiFileInfo.Cover;

        }

        public string GetFullPath()
        {
            return Path.Combine(ParentPlayList.Path, this.FileName);
        }

        #endregion


        /// <summary>
        /// Update tag Data from the external command.
        /// </summary>
        /// <param name="info"></param>
        public void UpdateData(FullAudiFileInfo info)
        {
            this._audiFileInfo.Artist = info.Artist;
            this._audiFileInfo.Title = info.Title;
            this._audiFileInfo.Genre = info.Genre;

            assignTagDataToPlaylist();
        }

    }
}
