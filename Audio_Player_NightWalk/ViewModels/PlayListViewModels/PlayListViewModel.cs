using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Player_NightWalk
{
    public class PlayListViewModel : BaseViewModel
    {

        public string Path { get; set; } = string.Empty;

        public string Name { get; set; }

        public TimeSpan PlaylistTotalTime { get; set; }

        public ObservableCollection<TrackViewModel> Tracks { get; set; }


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



    }
}
