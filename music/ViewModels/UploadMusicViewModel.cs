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
	internal class UploadMusicViewModel
	{
        public ObservableCollection<Song> Songs { get; set; } = new ObservableCollection<Song>();
		private FileService m_FileService;

        public UploadMusicViewModel()
        {
            m_FileService = new FileService();
        }

        public void UploadSongsToLibrary()
        {
            m_FileService.SaveMusicFilesToLibrary();
        }
    }
}