using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Audio_Player_NightWalk
{
    public class ImageViewerViewModel : BaseViewModel
    {


        public ImageViewerViewModel()
        {
            PlayerStateViewModel.Instance.PropertyChanged += CoverChanged;

            this.CoverChanged(this, new PropertyChangedEventArgs(nameof(PlayerStateViewModel.Instance.SelectedTrack)));

        }

        private void CoverChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(PlayerStateViewModel.Instance.SelectedTrack) || PlayerStateViewModel.Instance.SelectedTrack == null)
                return;

            this.Cover = PlayerStateViewModel.Instance.SelectedTrack.getTagData().Cover;
        }

        private ImageSource _cover = null;

        public ImageSource Cover
        {
            get { return _cover; }
            set
            {
                _cover = value;
                OnPropertyChanged(nameof(Cover));
    
            }
        }

    }
}
