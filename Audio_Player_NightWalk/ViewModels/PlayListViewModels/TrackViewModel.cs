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

        PlayListViewModel ParentPlayList {  get; set; }
        public string Name { get; set; }

        public TimeSpan TrackTime { get; set; }

        private bool clicked;

        public bool Clicked
        {
            get { return clicked; }
            set {  
                 
               clicked = value;
                if (clicked)
                    TagReader.ReadTagsFromPath(Path.Combine(this.ParentPlayList.Path, Name));           
            }
        }

        public TrackViewModel(string name, PlayListViewModel parentRef)
        {
            this.Name = name;

            this.TrackTime = new TimeSpan(0, 0, 0);

            this.ParentPlayList = parentRef;
        }



    }
}
