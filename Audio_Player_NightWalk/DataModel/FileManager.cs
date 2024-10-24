using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Audio_Player_NightWalk
{
    public static class FileManager
    {

        private static string _DEFAULT_FILEPATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "NightWalk");
        private static string _PLAYLIST_FOLDER = Path.Combine(_DEFAULT_FILEPATH, "Playlists");
        private static string _DEFAULT_NEW_PLAYLIST_NAME = Path.Combine(_PLAYLIST_FOLDER, "New Playlist");


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
            Directory.CreateDirectory(_PLAYLIST_FOLDER);
            Directory.CreateDirectory(Path.Combine(_PLAYLIST_FOLDER, "Default Playlist"));
        } 


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

        public static ObservableCollection<TrackViewModel> GetTracks(string filepath, PlayListViewModel parent)
        {

          
            var collection = new ObservableCollection<TrackViewModel>();


            try
            {
                foreach (string path in Directory.GetFiles(filepath))
                {
                    collection.Add(new TrackViewModel(path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1), parent));
                }
            }
            catch (IOException e)
            {

                Debug.WriteLine(e);
                Debugger.Break();

            }

            return collection;
        }



    }
}
