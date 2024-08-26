using MusicPlayer.Models;
using MusicPlayer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.ViewModels
{
	internal class MainViewModel
	{
        public ObservableCollection<Song> Songs { get; set; } = new ObservableCollection<Song>();
		private FileService m_FileService;

        public MainViewModel()
        {
            m_FileService = new FileService();
        }

        public void LoadSongs()
        {
            List<Song> loadedSongs = m_FileService.LoadMusicFiles();
            foreach (Song song in loadedSongs)
            {
                Songs.Add(song);
            }
        }
    }
}
