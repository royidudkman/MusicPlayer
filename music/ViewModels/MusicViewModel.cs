using MusicPlayer.Models;
using MusicPlayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.ViewModels
{
	internal class MusicViewModel
	{
		MusicService m_MusicService;

        public MusicViewModel()
        {
			m_MusicService = new MusicService();
		}

		public void UpdateSongProperties(Song i_Song)
		{
			m_MusicService.UpdateFileProperties(i_Song);
		}

		public void DeleteSong(string filePath)
		{
			m_MusicService.DeleteFile(filePath);
		}
    }
}
