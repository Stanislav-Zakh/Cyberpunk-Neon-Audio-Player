using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Audio_Player_NightWalk
{
    /// <summary>
    /// Interaction logic for VolumeControl.xaml
    /// </summary>
    public partial class VolumeControl : UserControl, INotifyPropertyChanged
    {

        public  event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string Propertyname)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Propertyname));

        }

        public VolumeControl()
        {
            InitializeComponent();
        }


        public float TrackProgress
        {
            get { return (float)GetValue(TrackProgressProperty); }
            set { SetValue(TrackProgressProperty, value); }
        }


        public static readonly DependencyProperty TrackProgressProperty =
            DependencyProperty.Register("TrackProgress", typeof(float), typeof(VolumeControl), new FrameworkPropertyMetadata(0.5f, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ProgressChanged));


        public static void ProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((VolumeControl) d).DisplayValue = float.Round(((float) e.NewValue) * 100, 2);
        } 


        private float _displayValue = 0.0f;

        public float DisplayValue
        {
            get { return _displayValue; }
            set { 
                _displayValue = value;

                OnPropertyChanged(nameof(DisplayValue));
            }
        }




        
    }
}
