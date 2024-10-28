using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Player_NightWalk
{
    /// <summary>
    /// This is temporary solution. 
    /// We will change static Instance to the proper apllication ViewModel with Ninjection.  
    /// </summary>
    public class PlayerStateViewModel : BaseViewModel
    {

        public static PlayerStateViewModel PlayerState { get; set; } = new PlayerStateViewModel();


        public PlayListViewModel? SelectedPlaylist { get; set; } = null;

        public TrackViewModel? SelectedTrack { get; set; } = null;

        public PlayerStateViewModel()
        {

                
        }







    }
}
