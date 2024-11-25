using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace Audio_Player_NightWalk
{
    public abstract class BaseValueConvertor<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {

        private static T _convertor = null; 

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return _convertor ?? (_convertor = new T());
        }

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);


        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        
    }
}
