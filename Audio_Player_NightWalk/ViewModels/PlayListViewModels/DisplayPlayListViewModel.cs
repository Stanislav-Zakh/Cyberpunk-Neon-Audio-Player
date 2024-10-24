using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Player_NightWalk
{
    class DisplayPlayListViewModel : BaseViewModel
    {
        

        public ObservableCollection<PlayListViewModel> testCollection { get; set; }


        public DisplayPlayListViewModel()
        {
            
            testCollection = FileManager.GetPlayLists();

        }

    }
}
