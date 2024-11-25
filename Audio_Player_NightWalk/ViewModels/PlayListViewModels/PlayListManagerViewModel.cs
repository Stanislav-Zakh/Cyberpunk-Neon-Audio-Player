using Audio_Player_NightWalk.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Audio_Player_NightWalk
{
    class PlayListManagerViewModel : SingeltonViewModel<PlayListManagerViewModel>
    {

        private AudioTreeItem? focusedtrack = null;

        public DisplayPlayListViewModel PlayList { get; set; }


        public DisplayFormViewModel Form { get; set; }


        private int _index;

        public int Index
        {
            get { return _index; }
            set {


                AssistTabTransition(value);

                _index = value;
                
                
                void AssistTabTransition(int value)
                {

                    if (_index == 2 || value == -1)
                        return;

                    if (_index == 1 && focusedtrack != null)
                         Form.SaveData(focusedtrack);

                    if (_index == 0 && value == 1 && focusedtrack != null)
                         Form.LoadData(focusedtrack);
                }
            }
        }

        


        /// <summary>
        /// Add new Playlist to the library.
        /// </summary>
        public ICommand AddNewPlayList { get; set; }

        public ICommand AddTracks { get; set; }


         

        private PlayListManagerViewModel()
        {

            PlayList = new DisplayPlayListViewModel();

            Form =  new DisplayFormViewModel();

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

        public AudioTreeItem? GetFocusedTrack()
        {
            return this.focusedtrack;
        }




    }
}
