using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Audio_Player_NightWalk
{
    public abstract class BaseAttachedProperty<Parent, Property> 
        where Parent : BaseAttachedProperty<Parent, Property>, new()
    {
        /// <summary>
        /// Instance that we refer in xaml
        /// </summary>
        public static Parent _Instance { get; private set; } = new Parent();

        /// <summary>
        /// Register Dependency Property with default value and on property changed callback
        /// </summary>

        public static readonly DependencyProperty ValueProperty = DependencyProperty.RegisterAttached("Value", typeof(Property), typeof(BaseAttachedProperty<Parent, Property>), new PropertyMetadata(OnPropertyChangedCallBack));


        public static Property GetValue(DependencyObject o) => (Property)o.GetValue(ValueProperty);

        public static void SetValue(DependencyObject o, Property value) => o.SetValue(ValueProperty, value);


        private static void OnPropertyChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            _Instance.ExecuteOnPropertyChanged(d, e);
        }

        protected virtual void ExecuteOnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

    }
}
