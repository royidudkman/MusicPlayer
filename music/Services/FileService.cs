using Microsoft.Win32;
using MusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TagLib;
using System.Threading.Tasks;
using System.Windows;

namespace MusicPlayer.Services
{
	internal class FileService
	{
		public List<Song> LoadMusicFiles()
		{
			List<Song> allSongs = new List<Song>();

			OpenFileDialog openFileDialog = new OpenFileDialog 
			{ 
				Multiselect = true,
				Filter = "Music Files|*.mp3;*.wav;*.flac"
			};

			bool? result = openFileDialog.ShowDialog();

			if (result == true)
			{
				foreach(string filePath in openFileDialog.FileNames)
				{
					Song song = LoadSongFromFile(filePath);
					if(song != null)
					{
						allSongs.Add(song);
					}
				}
			}

			return allSongs;
		}

		private Song LoadSongFromFile(string filePath)
		{
			try
			{
				File file = TagLib.File.Create(filePath);
				string title = file.Tag.Title;
				if(string.IsNullOrEmpty(title))
				{
					title = System.IO.Path.GetFileNameWithoutExtension(filePath);
				}

				return new Song
				{
					Title = title,
					Artist = file.Tag.Artists,
					Album = file.Tag.Album,
					Genres = file.Tag.Genres,
					Duration = file.Properties.Duration,
					FilePath = filePath,
				};
			}
			catch(Exception ex)
			{
				MessageBox.Show($"Error loading song: {ex.Message}");
				return null;
			}
		}
	}
}