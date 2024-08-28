using Microsoft.Win32;
using MusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TagLib;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace MusicPlayer.Services
{
	internal class FileService
	{
		private readonly string musicLibraryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MusicLibrary");

		public void SaveMusicFilesToLibrary()
		{
			if(!Directory.Exists(musicLibraryPath))
			{
				Directory.CreateDirectory(musicLibraryPath);
			}

			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Multiselect = true,
				Filter = "Music Files|*.mp3;*.wav;*.flac"
			};

			bool? result = openFileDialog.ShowDialog();

			if (result == true)
			{
				foreach (string filePath in openFileDialog.FileNames)
				{
					SaveSongToLibrary(filePath);
				}
			}
		}

		private void SaveSongToLibrary(string filePath)
		{
			try
			{
				string fileName = Path.GetFileName(filePath);
				string destinationPath = Path.Combine(musicLibraryPath, fileName);

                // Copy file to MusicLibrary directory
                System.IO.File.Copy(filePath, destinationPath, overwrite: true);

				Song song = LoadSongFromFile(destinationPath);
				if (song != null)
				{
					// Optionally, you can log the saved song details or update a UI, etc.
					Console.WriteLine($"Saved song: {song.Title} by {string.Join(", ", song.Artist)}");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error saving song: {ex.Message}");
			}
		}

		public List<Song> LoadSongsFromLibrary()
		{
			List<Song> songs = new List<Song>();

			if (Directory.Exists(musicLibraryPath))
			{
				var files = Directory.EnumerateFiles(musicLibraryPath, "*.mp3");

				foreach (var file in files)
				{
					Song song = LoadSongFromFile(file);
					songs.Add(song);
				}
			}

			return songs;
		}

		private Song LoadSongFromFile(string filePath)
		{
			try
			{
                TagLib.File file = TagLib.File.Create(filePath);
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