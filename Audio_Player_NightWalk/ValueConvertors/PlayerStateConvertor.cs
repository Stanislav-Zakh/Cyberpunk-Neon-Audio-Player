using Audio_Player_NightWalk.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Player_NightWalk
{
    public class PlayerStateConvertor : BaseValueConvertor<PlayerStateConvertor>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            switch ((PlayerState)value)
            {
                case PlayerState.INACTIVE:
                    return true;

                case PlayerState.PLAYING:
                    return false;

                case PlayerState.PAUSED:
                    return true;


                default:
                    Debugger.Break(); // <- Stop Debugger
                    return null;

            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
