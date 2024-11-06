using Audio_Player_NightWalk.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Audio_Player_NightWalk
{
    class PlayListManagerViewModel : BaseViewModel
    {

        private TrackViewModel? focusedtrack = null;


        

        




        public static PlayListManagerViewModel Instance { get; private set; } = new PlayListManagerViewModel();


        public DisplayPlayListViewModel PlayList { get; set; }


        /// <summary>
        /// Add new Playlist to the library.
        /// </summary>
        public ICommand AddNewPlayList { get; set; }

        public ICommand AddTracks { get; set; }


        private PlayListManagerViewModel()
        {

            PlayList = new DisplayPlayListViewModel();

            AddNewPlayList = new RelayCommand(() =>
                {
                    PlayListViewModel? playlist = FileManager.AddNewPlaylist();

                    if (playlist != null)
                        PlayList.testCollection.Add(playlist);
                }           
            );

            AddTracks = new RelayCommand(() =>
            {
                if (PlayerStateViewModel.Instance.SelectedPlayList == null)
                    // Show Message
                    return;

                FileManager.OpenFileDialogAddTracks(PlayerStateViewModel.Instance.SelectedPlayList);

            });



        }


        public void SetFocusedTrack(TrackViewModel track)
        {
            this.focusedtrack = track;
        }

        public TrackViewModel? GetFocusedTrack()
        {
            return this.focusedtrack;
        }




    }
}
