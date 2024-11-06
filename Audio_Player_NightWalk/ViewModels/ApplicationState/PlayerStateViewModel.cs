using Audio_Player_NightWalk.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace Audio_Player_NightWalk
{
    /// <summary>
    /// This is temporary solution. 
    /// We will change static Instance to the proper apllication ViewModel with Ninjection.  
    /// </summary>
    public class PlayerStateViewModel : BaseViewModel
    {


        private AudioPlayer _player;

        private DispatcherTimer timer;

        private bool isDragged = false;

        public Trackends TrackNaturalEnd = Trackends.NEXT;

        public static PlayerStateViewModel Instance { get; private set; } = new PlayerStateViewModel();


        public PlayListViewModel? SelectedPlayList { get; set; } = null;

        #region Full Properties

        private TrackViewModel? _selectedTrack;

        public TrackViewModel? SelectedTrack
        {
            get { return _selectedTrack; }
            set { 
                _selectedTrack = value;

                if (_selectedTrack == null)
                    return;
               
               this.PlayerState = PlayerState.PLAYING;
               
               this._player.PlayNewTrack(_selectedTrack.GetFullPath());

                OnPropertyChanged(nameof(SelectedTrack));
            }
        }

        private PlayerState _playerState = PlayerState.INACTIVE;

        public PlayerState PlayerState
        {
            get { return _playerState ; }
            set { 
                _playerState = value;
                OnPropertyChanged(nameof(PlayerState));
            }
        }



        private float _trackDuration = 0; 

        public float TrackDuration
        {
            get
            {
                if (!isDragged)
                {
                    _trackDuration = _player.GetDuration();
                    if (_trackDuration > 99.99f)
                        TrackEndsNaturally();


                }
                    

                return _trackDuration;
            }

            set
            {
                _trackDuration = value;
                OnPropertyChanged(nameof(TrackDuration));
            }
        }

        #endregion


        #region Commands

        public ICommand PlayOrPauseTrack { get; set; }

        public ICommand PlayNextTrack { get; set; }

        public ICommand PlayPreviousTrack { get; set; }

        public ICommand Repeat { get; set; }

        #endregion

        private PlayerStateViewModel()
        {
            this._player = new AudioPlayer();

            PlayOrPauseTrack = new RelayCommand(() =>
               this.PlayerState = this._player.PlayWithPauseInform()
            );
            
            Repeat = new RelayCommand(() => this._player.RepeatAudio());

            PlayNextTrack =  new RelayCommand(() => this.SelectedPlayList?.GetNextTrack());

            PlayPreviousTrack = new RelayCommand(() => this.SelectedPlayList?.GetPreviousTrack());


            /* Timer */

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.1);

            timer.Tick += (o, e) => 
            {
                if (this.PlayerState == PlayerState.INACTIVE || this.PlayerState == PlayerState.PAUSED || this.isDragged || _selectedTrack == null)
                    return;
                OnPropertyChanged(nameof(TrackDuration)); 
            };
            timer.Start();
        }

        public void setDragged()
        {
            this.isDragged = true;
        }

        
        public void AdjustDuration(float value)
        {
            _player.AdjustDuration(((float)value) / 100);
            this.isDragged = false;
            OnPropertyChanged(nameof(TrackDuration));
        }

        public void SetNextTrack()
        {
            if (SelectedTrack == null)
                return;


        }


        #region Track/Playlist selectors

        public void SelectTrack(TrackViewModel track)
        {
            this.SelectedTrack = track;
        }

        public void SelectPlaylist(PlayListViewModel playlist)
        {
            this.SelectedPlayList = playlist;
        }

        #endregion

        /// <summary>
        /// When track ends naturally (e.g. player did not select next track), execute one of the following functions based on the setting selected.
        /// </summary>
        private void TrackEndsNaturally()
        {
           switch(this.TrackNaturalEnd)
            {

                case Trackends.NEXT: 
                     this.SelectedPlayList?.GetNextTrack();
                     break;
                case Trackends.REPEAT:
                    this._player.RepeatAudio();
                    break;
                case Trackends.PAUSE:
                    this.PlayerState = PlayerState.PAUSED;
                    break;
            }
                
        }


    }
}
