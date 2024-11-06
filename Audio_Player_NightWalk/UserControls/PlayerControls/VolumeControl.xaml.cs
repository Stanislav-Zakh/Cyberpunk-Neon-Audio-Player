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

namespace Audio_Player_NightWalk.UserControls.PlayerControls
{
    /// <summary>
    /// Interaction logic for VolumeControl.xaml
    /// </summary>
    public partial class VolumeControl : UserControl
    {
        public VolumeControl()
        {
            InitializeComponent();
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string PropertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);



        }

        private double _angle = 0;

        public double Angle
        {

            get
            {
                return _angle;
            }

            set
            {
                _angle = value;
                OnPropertyChanged(nameof(Angle));

                UpdateBorderGradient(_angle);
            }


        }

        private void UpdateBorderGradient(double angle)
        {

        }

        public double HandleAngle
        {
            get { return (double)GetValue(HandleAngleProperty); }
            set { Angle = value; }
        }

        // Using a DependencyProperty as the backing store for Angle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HandleAngleProperty =
            DependencyProperty.Register("HandleAngle", typeof(double), typeof(PlayListManager), new PropertyMetadata(0));


    }
}
