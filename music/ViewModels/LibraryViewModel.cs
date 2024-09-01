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

		public List<string> LoadAlbumNames()
		{
			List<Song> songs = m_FileService.LoadSongsFromLibrary();
			List<string> albumNames = new List<string>();	

			foreach (Song song in songs)
			{
				string songAlbum = song.Album ?? "Unknown";

				if (!albumNames.Contains(songAlbum))
				{
					albumNames.Add(songAlbum);	
				}
			}

			return albumNames;
		}

		public List<Song> GetSongsFromAlbum(string albumName)
		{
			List<Song> songs = m_FileService.LoadSongsFromLibrary();
			List<Song> songsOfAlbum = new List<Song>();

			foreach (Song song in songs)
			{
				string songAlbum = song.Album ?? "Unknown";

				if (songAlbum == albumName)
				{
					songsOfAlbum.Add(song);
				}
			}

			return songsOfAlbum;
		}

		public List<string> LoadArtistNames()
		{
			List<Song> songs = m_FileService.LoadSongsFromLibrary();
			List<string> artistNames = new List<string>();

			foreach (Song song in songs)
			{
				if (song.Artist != null && song.Artist.Length > 0)
				{
					string songArtist = song.Artist[0];
					if (!string.IsNullOrWhiteSpace(songArtist) && !artistNames.Contains(songArtist))
					{
						artistNames.Add(songArtist);
					}
				}
				else
				{
					// Handle case where Artist is null or empty
					if (!artistNames.Contains("Unknown"))
					{
						artistNames.Add("Unknown");
					}
				}
			}

			return artistNames;
		}

		public List<Song> GetSongsFromArtist(string artistName)
		{
			List<Song> songs = m_FileService.LoadSongsFromLibrary();
			List<Song> songsOfArtist = new List<Song>();

			foreach (Song song in songs)
			{
				if (string.Equals(artistName, "Unknown", StringComparison.OrdinalIgnoreCase))
				{
					// Check if the Artist array is empty or contains only an empty string
					if (song.Artist == null || song.Artist.Length == 0 || string.IsNullOrEmpty(song.Artist[0]))
					{
						songsOfArtist.Add(song);
					}
				}
				else
				{
					// Check if the artist matches any of the artists in the song's Artist array
					if (song.Artist != null && song.Artist.Any(artist => string.Equals(artist, artistName, StringComparison.OrdinalIgnoreCase)))
					{
						songsOfArtist.Add(song);
					}
				}
			}

			return songsOfArtist;
		}
	}
}