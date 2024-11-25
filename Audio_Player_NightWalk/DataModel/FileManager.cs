using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Audio_Player_NightWalk
{
    public static class FileManager
    {

        private static string _DEFAULT_FILEPATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NightWalk");
        private static string _PLAYLIST_FOLDER = Path.Combine(_DEFAULT_FILEPATH, "Playlists");
        private static string _ART_FOLDER = Path.Combine(_DEFAULT_FILEPATH, "UserArt");
        private static string _DEFAULT_NEW_PLAYLIST_NAME = Path.Combine(_PLAYLIST_FOLDER, "New Playlist");



        #region Execute on the application first startup

        /// <summary>
        /// Instantiates the default directory with content that is required on the first startup of the application. 
        /// </summary>
        public static void CreateDefaultDirectoryIfNotExist()
        {

            if (Directory.Exists(_DEFAULT_FILEPATH))
                  return;

            try
            {                
               Directory.CreateDirectory(_DEFAULT_FILEPATH);
                CreatePlaylistsFolder();
                CreateArtFolder();
            } catch(IOException e)
            {
                Trace.WriteLine(e);
                Debugger.Break();
            }

        }

        /// <summary>
        /// Creates Directory that will hold user playlists and instatiates default user playlist.
        /// </summary>
        private static void CreatePlaylistsFolder()
        {
            if (Directory.Exists(_PLAYLIST_FOLDER))
                return;

            Directory.CreateDirectory(_PLAYLIST_FOLDER);
            Directory.CreateDirectory(Path.Combine(_PLAYLIST_FOLDER, "Default Playlist"));
        }

        /// <summary>
        /// Creates a folder to hold user artworks for the 'Image Library' section of the player.  
        /// Can be called when checking for files. 
        /// </summary>
        private static void CreateArtFolder()
        {
            if (Directory.Exists(_ART_FOLDER))
                return;

            Directory.CreateDirectory(_ART_FOLDER);
        }



        #endregion

        #region Get Paths to the Directory/Files

        public static ObservableCollection<PlayListViewModel> GetPlayLists()
        {

            var collection = new ObservableCollection<PlayListViewModel>();

            try
            {
                foreach (string path in Directory.GetDirectories(_PLAYLIST_FOLDER))
                {
                    collection.Add(new PlayListViewModel(path, path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1)));
                }
            }
            catch (IOException e)
            {

                Debug.WriteLine(e);
                Debugger.Break();

            }

             return collection;
        }

        public static ObservableCollectionRange<TrackViewModel> GetTracks(string filepath, PlayListViewModel parent)
        {


            var collection = new ObservableCollectionRange<TrackViewModel>();


            try
            {
                foreach (string path in Directory.GetFiles(filepath))
                {
                    collection.Add(new TrackViewModel(ReadTrackName(path), parent));
                }
            }
            catch (IOException e)
            {

                Debug.WriteLine(e);
                Debugger.Break();

            }

            return collection;
        }

        public static ObservableCollectionRange<ArtViewModel> GetArtWorks()
        {

            var collection = new ObservableCollectionRange<ArtViewModel>();


            try
            {
                foreach (string path in Directory.GetFiles(_ART_FOLDER))
                {
                    collection.Add(new ArtViewModel(path, path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1), GetDownscaledImage(path, 60, 60)));
                }
            }
            catch (IOException e)
            {

                Debug.WriteLine(e);
                Debugger.Break();

            }

            return collection;

        }


        #endregion


        #region Add new Playlist

        /// <summary>
        /// Create New PlayList with Unique Name.
        /// </summary>
        /// <returns></returns>

        internal static PlayListViewModel? AddNewPlaylist()
        {
            var count = 2;
            string NewFolderName = _DEFAULT_NEW_PLAYLIST_NAME;

            while (Directory.Exists(NewFolderName))
            {
                NewFolderName = $"{_DEFAULT_NEW_PLAYLIST_NAME} ({count})";

                count++;
            }
            try
            {
               var info = Directory.CreateDirectory(Path.Combine(NewFolderName));

                return new PlayListViewModel(NewFolderName, info.Name);
            }
            catch (IOException e)
            {
                Debug.WriteLine(e);
                Debugger.Break();
            }

            return null;
        }

        #endregion


        #region Add new Tracks


        public static string[] OpenFileDialog(string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = $"D:{Path.DirectorySeparatorChar}Down{Path.DirectorySeparatorChar}";
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = filter;

            if (!openFileDialog.ShowDialog() ?? true)
                return [];

             return openFileDialog.FileNames;
        }


        public static void OpenFileDialogAddTracks(PlayListViewModel PlayList)
        {
            string[] filePaths = OpenFileDialog("Audio files (*.mp3)|*.mp3|All files (*.*)|*.*");

            if (filePaths.Length < 1)
                return;

            // move filepaths
            List<TrackViewModel> tracks = new List<TrackViewModel>();

            foreach (string path in filePaths)
            {
                var name = path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1);


                MoveTrack(path, PlayList.Path, name);


                tracks.Add(new TrackViewModel(name, PlayList));
            }

            PlayList.AddTracks(tracks);
        }

        public static void OpenFileDialogAddArtWorks(ObservableCollectionRange<ArtViewModel> arts)
        {
            string[] filePaths = OpenFileDialog("Images and Gifs (*.png, *.gif) |*.gif; *.png |All files (*.*)|*.*");

            if (filePaths.Length < 1)
                return;

            List<ArtViewModel> artworks = new List<ArtViewModel>();

            foreach (string path in filePaths)
            {
                var name = path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1);


               var filepath = MoveTrack(path, _ART_FOLDER, name);

                artworks.Add(new ArtViewModel(filepath, name, GetDownscaledImage(path, 60, 60)));
            }

            arts.AddRange(artworks);
        }

       
        private static string MoveTrack(string OldPath, string NewPath, string name)
        {
            var count = 2;
            string NewFileName = $"{NewPath}{Path.DirectorySeparatorChar}{name}";

            while (File.Exists(NewFileName))
            {
                NewFileName = $"{NewPath}{Path.DirectorySeparatorChar}({count}) {name}";

                count++;
            }
            try
            {
                File.Copy(OldPath, NewFileName);
                return NewFileName;
            }
            catch (IOException e)
            {
                Debug.WriteLine(e);
                Debugger.Break();
                return null;
            }
        }

        #endregion


        #region Read data from the file

        public static Stream GetStream(string filePath, FileAccess access = FileAccess.Read, FileShare share = FileShare.ReadWrite)
        {
           return new FileStream(filePath, FileMode.Open, access, share);
        }

        public static BitmapImage GetDownscaledImage(string filePath, int decodedWidth, int decodedHeight)
        {

            var bitmap = new BitmapImage(new Uri(filePath));
            bitmap.DecodePixelWidth = decodedWidth;
            bitmap.DecodePixelHeight = decodedHeight;

            return bitmap;
        }



        #endregion

        private static string ReadTrackName(string path)
        {
            return path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1);
        }


    }
}
