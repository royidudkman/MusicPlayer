using MusicPlayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.ViewModels
{
	internal class YoutubeViewModel
	{
		private YoutubeService m_YoutubeService;
		public YoutubeViewModel()
        {
            m_YoutubeService = new YoutubeService();
        }
        public async void DownloadFromYoutube(string url)
		{
			await m_YoutubeService.DownloadFromYoutube(url);
		}
	}
}
