using MusicPlayer.Models;
using MusicPlayer.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MusicPlayer.ViewModels
{
	internal class LibraryViewModel
	{
		private FileService m_FileService;

        public LibraryViewModel()
        {
			m_FileService = new FileService();
        }

		public List<Song> LoadSongsFromLibrary()
		{
			return m_FileService.LoadSongsFromLibrary();
		}
	}
}