using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Audio_Player_NightWalk
{
    public class MonitorDoubleClick : BaseAttachedProperty<MonitorDoubleClick, bool>
    {
        protected override void ExecuteOnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Control control = (Control)d;

            if (control == null)
                return;

            control.MouseDoubleClick -= ExecuteOnDoubleClick;

            if ((bool)e.NewValue)
               control.MouseDoubleClick += ExecuteOnDoubleClick; 

        }

        private void ExecuteOnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            isDoubleClicked.SetValue((DependencyObject) sender, true);
        }
    }

    public class isDoubleClicked : BaseAttachedProperty<isDoubleClicked, bool>
    {

    }


}
