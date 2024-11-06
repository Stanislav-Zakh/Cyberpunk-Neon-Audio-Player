using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Audio_Player_NightWalk
{
    public class LocalMediaViewModel : BaseViewModel
    {

        public Duration AnimationDuration { get; set; } = new Duration(new TimeSpan(0, 0, 0, 0, 1000));



    }
}
