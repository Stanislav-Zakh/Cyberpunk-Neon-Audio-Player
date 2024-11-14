using NAudio.Wave;
using NAudio.WaveFormRenderer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Player_NightWalk
{
    public class AudioPlayer
    {

        private WaveFormRenderer renderer = null;

        private PeakProvider peakProvider = null;

        private WaveFormRendererSettings renderSetting = null; 

        private WaveOutEvent? outputDevice = null;

        private Mp3FileReader? audioFile = null;

        private Stream? stream = null;

        private bool PAUSED = false;



        private void CreateRenderer()
        {

            this.peakProvider = new MaxPeakProvider();

            this.renderSetting = new StandardWaveFormRendererSettings();

            renderSetting.Width = 800;
            renderSetting.TopHeight = 10;
            renderSetting.BottomHeight = 10;



            this.renderer = new WaveFormRenderer();
        }


        public void getRenderSnapShot()
        {
            //return this.renderer.Render()
        }

      
        /// <summary>
        /// Dispose of the previous track (if exist) and start playing a new track
        /// </summary>
        /// <param name="filePath"></param>
        public void PlayNewTrack(string filePath)
        {
            this.StopAudio();

            outputDevice = new WaveOutEvent();
            this.stream = FileManager.GetStream(filePath);
            audioFile = new Mp3FileReader(stream);
            outputDevice.Init(audioFile);

            PAUSED = false;
            outputDevice.Play();
        }

       


        /// <summary>
        /// Pause or Play current track.  
        /// </summary>
        /// <param name="filePath"></param>
        /// 

        public void PlayWithPause()
        {
            if (outputDevice == null)
                return;

            if (PAUSED)
            {
                PlayAudio();

            } else
            {
                PauseAudio();
            }
        }

        public PlayerState PlayWithPauseInform()
        {
            if (outputDevice == null)
                return PlayerState.INACTIVE;

            if (PAUSED)
            {
                PlayAudio();
                return PlayerState.PLAYING;

            }
            else
            {
                PauseAudio();
                return PlayerState.PAUSED;
            }
        }


        public void PlayAudio()
        {
            outputDevice.Play();
            this.PAUSED = false;
        }



        public void PauseAudio()
        {
            outputDevice.Pause(); 
            this.PAUSED = true;   
        }

        /// <summary>
        /// Stop/Dispose Audio
        /// </summary>
        public void StopAudio()
        {
            if (outputDevice == null)
                return;

            this.outputDevice.Stop();

            this.outputDevice.Dispose();
            this.outputDevice = null;
            this.audioFile.Dispose();
            this.audioFile = null;

            stream.Dispose();
            stream = null;
        }


        public void RepeatAudio()
        {
            if (audioFile == null)
                return;

            audioFile.Position = 0;

        }

        /// <summary>
        /// Move to the specific part of the audio. Values betwean 0.0 <-> 1.0.
        /// </summary>
        /// <param name="value"></param>
        public void AdjustDuration(float value)
        {
            if (audioFile == null)
                return;

            if (value > 1.0f || value < 0.0f)
                value = Math.Abs(value % 1.0f);

            audioFile.Position = (long) (audioFile.Length * value);

        }

        public float GetDuration()
        {
            if (audioFile == null || audioFile.Length == 0)
                return 0;


            return (audioFile.Position * (100f / audioFile.Length));
        }

        /// <summary>
        /// Adjust volume of the audio
        /// </summary>
        /// <param name="value"></param>
        public void AdjustVolume(float value)
        {
            

        }


        
















    }
}
