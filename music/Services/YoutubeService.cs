
using MediaToolkit;
using MediaToolkit.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using VideoLibrary;

namespace MusicPlayer.Services
{
	public class YoutubeService
	{
		private readonly string musicLibraryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MusicLibrary");

		public async Task DownloadFromYoutube(string youtubeUrl)
		{
			try
			{
				// Create a YouTube instance and get the video
				YouTube youtube = YouTube.Default;
				var vid = await youtube.GetVideoAsync(youtubeUrl);

				// Ensure musicLibraryPath exists
				if (!Directory.Exists(musicLibraryPath))
				{
					Directory.CreateDirectory(musicLibraryPath);
				}

				// Use MemoryStream to handle video data
				using (var videoStream = new MemoryStream(vid.GetBytes()))
				{
					// Save the video to a temporary file
					string tempVideoFilePath = Path.Combine(Path.GetTempPath(), "tempVideo.mp4");

					using (var tempFileStream = new FileStream(tempVideoFilePath, FileMode.Create, FileAccess.Write))
					{
						videoStream.CopyTo(tempFileStream);
					}

					// Prepare for conversion
					var inputFile = new MediaFile { Filename = tempVideoFilePath };
					var outputFile = new MediaFile { Filename = Path.Combine(musicLibraryPath, $"{Path.GetFileNameWithoutExtension(vid.FullName)}.mp3") };

					// Convert video to MP3
					using (var engine = new Engine())
					{
						engine.GetMetadata(inputFile);
						engine.Convert(inputFile, outputFile);
					}

					// Clean up temporary file
					File.Delete(tempVideoFilePath);
				}

				Console.WriteLine("Download and conversion complete!");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An error occurred: {ex.Message}");
			}
		}
	}
}