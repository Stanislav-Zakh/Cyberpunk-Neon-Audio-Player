using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace Audio_Player_NightWalk
{
    public static class TagReader
    {

        private static Stream? stream = null;

        /// <summary>
        /// Substitude Value for the empty Tags. 
        /// </summary>
        private static readonly string EMPTY = "unknown";

        /// <summary>
        /// File that is being edited and soon to be closed.
        /// </summary>
        private static TagLib.File? _editFile = null;


        #region Read Tags (to display in Track)


        /// <summary>
        /// Returns a struct with audio file info.
        /// </summary>
        /// <param name="track"></param>
        /// <param name="playlist"></param>
        /// <returns></returns>

        public static AudioFileInfo LoadTagInfo(string filepath)
        {

            if (!filepath.EndsWith(".mp3"))
                throw new IOException("File is not .mp3");



            using (var file = TagLib.File.Create(filepath))
            {
                return ReadAndFormatTags(file);
            }     
        }

        private static AudioFileInfo ReadAndFormatTags(TagLib.File file)
        {
            return new AudioFileInfo(
               file.Tag.Title ?? EMPTY,
               file.Tag.FirstPerformer ?? EMPTY,
               FormatRunningTime(file.Properties.Duration),
               file.Tag.Album ?? EMPTY,
               file.Tag.Year != 0 ? file.Tag.Year.ToString() : EMPTY,
               file.Tag.FirstGenre ?? EMPTY
               )
            { Cover = GetImage(file) };
        }



        /// <summary>
        /// Async read running time for each track in the playlist and computes total running time of the playlist.
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static async Task GetAudioDurationAsync(PlayListViewModel playlist)
        {
            await Task.Run
                (
                  () =>
                  {
                      TimeSpan totalTime = TimeSpan.FromSeconds(0);

                      foreach (var track in playlist.Tracks)
                      {
                          using (var file = TagLib.File.Create(track.GetFullPath()))
                          {
                              totalTime = totalTime.Add(file.Properties.Duration);
                              track.TrackTime = FormatRunningTime(file.Properties.Duration);
                          }
                      }

                       playlist.PlaylistTotalTime = FormatRunningTime(totalTime);
                  });  
        }

        /// <summary>
        /// Async Read Duration data for all Playlists and waits for them in parallel to complete.  
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static async Task GetDurationAsync(ObservableCollection<PlayListViewModel> collection)
        {
            var duration = collection.Select((playlist) => GetAudioDurationAsync(playlist));

            await Task.WhenAll(duration.ToArray());
        }





        #endregion


        #region Load Full Tags()


        /// <summary>
        /// Load full info and free resources. 
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static FullAudiFileInfo LoadFullTagInfo(string filepath)
        {
            using (var file = TagLib.File.Create(filepath))
            {
                return ExtractAllTags(file);
            }

        }

        /// <summary>
        /// Load full info and retain file open file.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static FullAudiFileInfo LoadFullTagInfoAndEdit(string filepath)
        {

            _editFile = TagLib.File.Create(new StreamAbstraction(filepath, FileManager.GetStream(filepath, FileAccess.ReadWrite)));
     
            return ExtractAllTags(_editFile);
            
        }

        /// <summary>
        /// Extract tags From the file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static FullAudiFileInfo ExtractAllTags(TagLib.File file)
        {
            return new FullAudiFileInfo
                (
                /// firtstAlbum artis != performers[0]
                file.Tag.FirstAlbumArtist ?? EMPTY,
                file.Tag.Title ?? EMPTY,
                file.Tag.Album ?? EMPTY,
                file.Tag.Year.ToString() ?? EMPTY,
                file.Tag.FirstGenre ?? EMPTY,
                file.Tag.RemixedBy ?? EMPTY
                );
        }


        #endregion


        #region Write Tags to file

        /// <summary>
        /// Save Edited tags to the File 
        /// </summary>
        /// <param name="info"></param>
        /// <exception cref="Exception"></exception>

        public static void SaveFullInfo(FullAudiFileInfo info, string filepath)
        {
            
            if (_editFile == null)
                throw new Exception("Cannot be NULL");

            if (_editFile.Tag.Performers.Length > 0)
            {
                var perf = _editFile.Tag.Performers;
                perf[0] = info.Artist;

                _editFile.Tag.Performers = perf;
            }

            _editFile.Tag.Title = info.Title;
            _editFile.Tag.Album = info.Album;

            ///TODO: add regex o the form and potentially remove this thing.
            if (UInt32.TryParse(info.Date, out uint date))
            {
                _editFile.Tag.Year = date > 9999 ? 9999 : date; // years greater 9999 cannot be stored by the most tagging formats.
            }
            
            if (_editFile.Tag.Genres.Length > 0)
            {
                var genre = _editFile.Tag.Genres;

                genre[0] = info.Genre;

                _editFile.Tag.Genres = genre;
            }
                

            _editFile.Tag.RemixedBy = info.Remix;

            _editFile.Save();

            _editFile.Dispose();

            _editFile = null;
        }


        #endregion


        #region Utility (ex. get images or format numbers)

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

        private static string FormatRunningTime(TimeSpan time)
        {
            var h = "";

            h = formatTime(time.Hours, h);
            h = formatTime(time.Minutes, h);

            var l = time.Seconds > 9 ? "" : "0";
            return $"{h}{l}{time.Seconds}";


            string formatTime(int val, string h)
            {
                
                if (val > 0)
                    h += val > 9 ? $"{val}:" : $"0{val}:";
                return h;
            }
        } 



        #endregion


        #region Other (unfinished but interesting ideas)

        public static AudioFileInfo SelectedFile = new AudioFileInfo();

        public static event Action NewFileSelected = () => { };

        /// <summary>
        /// Method that assigns tags to the selected file filed and informs any other class that want to consume info on the newly selected track. 
        /// </summary>
        /// <param name="filePath"></param>

        public static void ReadTagsFromPath(string filePath)
        {

            if (!filePath.EndsWith(".mp3"))
                return;


            using (var file = TagLib.File.Create(filePath))
            {
                SelectedFile = ReadAndFormatTags(file);

                NewFileSelected.Invoke();
            }
        }

        #endregion

    }

    #region Tag DTO

    /// <summary>
    /// Smaller class for transfering tag data to the track. 
    /// </summary>


    public class AudioFileInfo
    {

        public string Title { get; set; }

        public string Artist { get; set; }

        public string Duration { get; set; }

        public string Album { get; set; }
       
        public string Genre { get; set; }

        public string Year { get; set; }

        public ImageSource? Cover { get; set; }


        public AudioFileInfo()
        {
            
        }

        public AudioFileInfo(string title, string artist, string duration, string album,  string genre, string year)
        {
            Title = title;
            Duration = duration;
            Artist = artist;
            Album = album;
            Genre = genre;
            Year = year;
        }

    }

    public class FullAudiFileInfo
    {

        public string Artist { get; set; }

        public string Title { get; set; }
        public string Album { get; set; }

        public string Date { get; set; }

        public string Genre { get; set; }

        public string Remix { get; set; }


        public FullAudiFileInfo(string artist, string title, string album, string date, string genre, string remix)
        {
            Artist = artist;
            Title = title;
            Album = album;
            Date = date;
            Genre = genre;
            Remix = remix;
        }



        public List<FormRowData> GetRows()
        {
            return new List<FormRowData>()
            {
                new FormRowData("Artis Name", this.Artist),
                new FormRowData("Track Title", this.Title),
                new FormRowData("Album Title", this.Album),
                new FormRowData("Date", this.Date),
                new FormRowData("Genre", this.Genre),
                new FormRowData("Remix", this.Remix)

            };
        }

        public void UpdateData(List<FormRowData> rows)
        {
            this.Artist = rows[0].Value;
            this.Title = rows[1].Value;
            this.Album = rows[2].Value;
            this.Date = rows[3].Value;
            this.Genre = rows[4].Value;
            this.Remix = rows[5].Value;
        }


    }
    public class FormRowData
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public bool Modifiable { get; private set; }


        public FormRowData(string key, string value, bool mod = true)
        {
            Key = key;
            Value = value;
            Modifiable = mod;
        }

    }


    #endregion



    public class StreamAbstraction : TagLib.File.IFileAbstraction
    {
        private string _path = String.Empty;
        private Stream _stream;

        public StreamAbstraction(string path, Stream stream)
        {

            this._path = path;
            this._stream = stream;       

        }

        public string Name => this._path;

        public Stream ReadStream => this._stream;

        public Stream WriteStream => this._stream;

        /// <summary>
        /// We should leave this method empty. see github docs. 
        /// </summary>
        /// <param name="stream"></param>
        public void CloseStream(Stream stream)
        {
            
        }
    }

}
