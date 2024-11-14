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
    public class SelectedTrackConverter : BaseValueConvertor<SelectedTrackConverter>
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Binding.DoNothing;

            AudioFileInfo info = ((TrackViewModel)value).getTagData();

            if (parameter != null)
                return $"{info.Title} by {info.Artist}";
            else
            {
                return $"{info.Album} | {info.Genre} | {info.Year}";
            }


        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
