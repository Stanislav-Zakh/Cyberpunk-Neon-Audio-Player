using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Audio_Player_NightWalk
{
    public static class TagReader
    {

        private static readonly string EMPTY = "unknown";

        public static AudioFileInfo SelectedFile = new AudioFileInfo();

        public static event Action NewFileSelected = () => { };

        public static void ReadTagsFromPath(string filePath)
        {

            if (!filePath.EndsWith(".mp3"))
                return;


           var file = TagLib.File.Create(filePath);

            FormatTagsAndAssignToSelected(file);

            NewFileSelected.Invoke();

        }

        /// <summary>
        /// Read Tags and set them to the 
        /// </summary>
        /// <param name="file"></param>
        private static void FormatTagsAndAssignToSelected(TagLib.File file)
        {



            SelectedFile = new AudioFileInfo(
                file.Tag.Title ?? EMPTY,
                file.Properties.Duration.ToString() ?? EMPTY,
                file.Tag.FirstAlbumArtist ?? EMPTY,
                file.Tag.FirstGenre ?? EMPTY
                )
            {
                Cover = GetImage(file) 

            };

            

        }

        private static AudioFileInfo ReadAndFormatTags(TagLib.File file)
        {
             return new AudioFileInfo(
                file.Tag.Title ?? EMPTY,
                file.Properties.Duration.TotalMinutes.ToString() ?? EMPTY,
                file.Tag.FirstAlbumArtist ?? EMPTY,
                file.Tag.FirstGenre ?? EMPTY
                
                )
            {
                Cover = GetImage(file)

            };



        }



        private static void ReadAndFormatTags(TagLib.File file, PlayListViewModel parent)
        {

            parent.Title = file.Tag.Title ?? EMPTY;
            parent.Duration = file.Properties.Duration.ToString() ?? EMPTY;
            parent.Artist = file.Tag.FirstAlbumArtist ?? EMPTY;
            parent.Genre = file.Tag.FirstGenre ?? EMPTY;
            parent.Cover = GetImage(file);

        }


        /// <summary>
        /// Get the first image if exists, if not return null.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static ImageSource? GetImage(TagLib.File file)
        {
            if (file.Tag.Pictures.Length < 1)
                return null;


            return (ImageSource?)new ImageSourceConverter().ConvertFrom(file.Tag.Pictures[0].Data.Data);
        }



        #region Using TagReader for TreeView 


        public static void ReadTagsAndAssignToTheParent(TrackViewModel track, PlayListViewModel playlist)
        {
            var filepath = Path.Combine(playlist.Path, track.Name);


            if (!filepath.EndsWith(".mp3"))
                return;


            var file = TagLib.File.Create(filepath);

            ReadAndFormatTags(file, playlist);

         

        }

        /// <summary>
        /// Returns a struct with audio file info.
        /// </summary>
        /// <param name="track"></param>
        /// <param name="playlist"></param>
        /// <returns></returns>

        public static AudioFileInfo ReadTagsAndReturnInfo(string filepath)
        {

            if (!filepath.EndsWith(".mp3"))
                throw new IOException("File is not .mp3"); 
                


            var file = TagLib.File.Create(filepath);

            return ReadAndFormatTags(file);
        }







        #endregion


    }


    public struct AudioFileInfo
    {

        public string Title { get; }
        public string Duration { get; }

        public string Artist { get; }

        public string Genre { get; }

        public ImageSource? Cover { get; set; }


        public AudioFileInfo(string title, string duration, string artist, string genre)
        {
            Title = title;
            Duration = duration;
            Artist = artist;
            Genre = genre;
        }
    }

}
