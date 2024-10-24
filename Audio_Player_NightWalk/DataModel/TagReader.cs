using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Audio_Player_NightWalk
{
    public static class TagReader
    {

        public static AudiFileInfo SelectedFile = new AudiFileInfo(); 

        private static readonly string EMPTY = "unknown";

        public static event Action NewFileSelected = () => { }; 


        public static void ReadTagsFromPath(string filePath)
        {

            if (!filePath.EndsWith(".mp3"))
                return;


           var file = TagLib.File.Create(filePath);

            ReadAndFormatTags(file);

            NewFileSelected.Invoke();

        }


        private static void ReadAndFormatTags(TagLib.File file)
        {



            SelectedFile = new AudiFileInfo(
                file.Tag.Title ?? EMPTY,
                file.Tag.Length ?? EMPTY,
                file.Tag.FirstAlbumArtist ?? EMPTY,
                file.Tag.FirstGenre ?? EMPTY
                )
            {
                Cover = GetImage(file) 

            };

            

        }


        private static ImageSource? GetImage(TagLib.File file)
        {
            if (file.Tag.Pictures.Length < 1)
                return null;


            return (ImageSource?)new ImageSourceConverter().ConvertFrom(file.Tag.Pictures[0].Data.Data);
        }



        #region Using TagReader for TreeView 


        





        #endregion


    }


    public struct AudiFileInfo
    {

        public string Title { get; }
        public string Duration { get; }

        public string Artist { get; }

        public string Genre { get; }

        public ImageSource? Cover { get; set; }


        public AudiFileInfo(string title, string duration, string artist, string genre)
        {
            Title = title;
            Duration = duration;
            Artist = artist;
            Genre = genre;
        }
    }

}
