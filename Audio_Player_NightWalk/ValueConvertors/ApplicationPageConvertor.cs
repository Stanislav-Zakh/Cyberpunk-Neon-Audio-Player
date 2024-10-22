using Audio_Player_NightWalk;
using Audio_Player_NightWalk.DataModel.Enums;
using Audio_Player_NightWalk.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Audio_Player_NightWalk
{
    public class ApplicationPageConvertor : BaseValueConvertor<ApplicationPageConvertor>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch((CurrentPageType) value)
            {
                case CurrentPageType.LocalMedia:
                    return new LocalMediaPage(); 


                default:
                    Debugger.Break(); // <- Stop Debugger
                    return null;
                
            }



           
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
