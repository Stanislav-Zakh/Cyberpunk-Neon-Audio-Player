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

        public static PlayListManagerViewModel Instance { get; set; } = new PlayListManagerViewModel();


        public DisplayPlayListViewModel PlayList { get; set; }


        /// <summary>
        /// Add new Playlist to the library.
        /// </summary>
        public ICommand AddNewPlayList { get; set; }

        public PlayListManagerViewModel()
        {

            PlayList = new DisplayPlayListViewModel();

            AddNewPlayList = new RelayCommand(() =>
                {
                    PlayListViewModel? playlist = FileManager.AddNewPlaylist();

                    if (playlist != null)
                        PlayList.testCollection.Add(playlist);
                }           
            );

        }




    }
}
