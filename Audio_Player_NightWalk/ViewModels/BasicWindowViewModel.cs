using Audio_Player_NightWalk.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Audio_Player_NightWalk.ViewModels
{
    public class BasicWindowViewModel : BaseViewModel
    {
        #region Private _members

        private MainWindow _window;


        #endregion



        #region Public Properties


        public string PlayerTitle { get; set; } = "StormFront";

        

        public int ChromeResizeBorderThinkness { get; set; } = 10;

        /// <summary>
        /// Size of the invisible area around the visible window.
        /// </summary>
        public int OuterPadding 
        {
            get => this._window.WindowState.Equals(WindowState.Maximized) ? 0 : 10;

        }

        /// <summary>
        /// Size of the Application Header
        /// </summary>
        public int ChromeCaptionHeight { get; set; } = 30;

        /// <summary>
        /// Corner Radious of the visible window.
        /// </summary>
        public int WindowCornerRadious
        {
            get => this._window.WindowState.Equals(WindowState.Maximized) ? 0 : 10;

        }


        #endregion


        #region Commands

        public ICommand MinimizeWindowCommand { get; set; }
        public ICommand MaximizeWindowCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        #endregion



        #region Constructor

        public BasicWindowViewModel(MainWindow window)
        {
            this._window = window;


            this.MinimizeWindowCommand = new RelayCommand(() => this._window.WindowState = WindowState.Minimized);
            this.MaximizeWindowCommand = new RelayCommand(() => this._window.WindowState ^= WindowState.Maximized); // // <- bit wise XOR 0000 <-> 0010 = 0010
            this.CloseWindowCommand = new RelayCommand(() => this._window.Close());



            this._window.StateChanged += (o, e) =>
            {
                OnPropertyChanged(nameof(OuterPadding));
                OnPropertyChanged(nameof(WindowCornerRadious));
                
            };

        }



        #endregion







    }
}
