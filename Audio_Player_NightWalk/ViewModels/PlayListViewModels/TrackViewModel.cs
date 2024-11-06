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
    public class TrackViewModel : BaseViewModel
    {

        #region private fields

        private bool _firstTimeTags = true;

        private AudioFileInfo _audiFileInfo;

        #endregion



        #region

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

        public TimeSpan TrackTime { get; set; }

        private bool clicked;

        /// <summary>
        /// If user single-clicked on the track, get ad display information.
        /// </summary>
        public bool Clicked
        {
            get { return clicked; }
            set {  
                 
               clicked = value;
                if (clicked)
                    readTagData(true);
                           
            }
        }

        public TrackViewModel(string name, PlayListViewModel parentRef)
        {
            this.Name = name;

            this.TrackTime = new TimeSpan(0, 0, 0);

            this.ParentPlayList = parentRef;
        }

        /// <summary>
        /// Reads tag Data from the file and (if passed in param is true) assigns data to the parent Playlist. 
        /// </summary>
        /// <param name="assignToParent"></param>
        public void readTagData(bool assignToParent)
        {
            if (_firstTimeTags)
            {
                this._audiFileInfo = TagReader.ReadTagsAndReturnInfo(this.GetFullPath());
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
                this._audiFileInfo = TagReader.ReadTagsAndReturnInfo(this.GetFullPath());
                _firstTimeTags = false;
            }
                
                
            return _audiFileInfo;
        } 

        public void assignTagDataToPlaylist()
        {


            ParentPlayList.Title = this._audiFileInfo.Title;
            ParentPlayList.Duration = this._audiFileInfo.Duration;
            ParentPlayList.Artist = this._audiFileInfo.Artist;
            ParentPlayList.Genre = this._audiFileInfo.Genre;
            ParentPlayList.Cover = this._audiFileInfo.Cover;
        }

        public string GetFullPath()
        {
            return Path.Combine(ParentPlayList.Path, this.Name);
        }
        




    }
}
