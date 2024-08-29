using MusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer.Services
{
	internal class MusicService
	{
		public TagLib.File GetFile(string filePath)
		{		
			if(System.IO.File.Exists(filePath))
			{
				return TagLib.File.Create(filePath);
			}

			return null;
		}

		public void UpdateFileProperties(Song i_Song)
		{
			var file = GetFile(i_Song.FilePath); 
			if(file != null)
			{
				file.Tag.Title = i_Song.Title;
				file.Tag.Artists =i_Song.Artist;
				file.Tag.Album = i_Song.Album;
				file.Tag.Genres = i_Song.Genres;
				file.Save();
			}
		}

		public void DeleteFile(string filePath) 
		{
			if (System.IO.File.Exists(filePath))
			{
				System.IO.File.Delete(filePath);
			}
		}
	}
}
