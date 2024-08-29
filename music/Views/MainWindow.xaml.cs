using Microsoft.Toolkit.Wpf.UI.Controls;
using MusicPlayer.Models;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace MusicPlayer.Views
{
	public partial class MainWindow : Window
	{
		private UploadMusicViewModel m_UploadMusicViewModel;
		private LibraryViewModel m_LibraryViewModel;
		private bool isPlaying = false;
		private CancellationTokenSource cts = new CancellationTokenSource();

		public MainWindow()
		{
			InitializeComponent();
			m_UploadMusicViewModel = new UploadMusicViewModel();
			m_LibraryViewModel = new LibraryViewModel();
			loadLibrary();
		}

		private void buttonUpload_Click(object sender, RoutedEventArgs e)
		{
			m_UploadMusicViewModel.UploadSongsToLibrary();
			loadLibrary();		
		}

		private void buttonLibrary_Click(object sender, RoutedEventArgs e)
		{
			loadLibrary();
		}

		private void buttonBrowse_Click(object sender, RoutedEventArgs e)
		{
			BrowserWindow browser = new BrowserWindow();
			browser.Show();
		}

		private void btnPrevious_Click(object sender, RoutedEventArgs e)
		{
			if (mediaElement.Position.TotalSeconds < 5)
			{
				SkipToPreviousSong();

			}

			else
			{
				mediaElement.Position = TimeSpan.FromSeconds(0);
			}
		}

		private void btnNext_Click(object sender, RoutedEventArgs e)
		{
			SkipToNextSong();
		}

		private void btnPlayPause_Click(object sender, RoutedEventArgs e)
		{
			if (isPlaying)
			{
				isPlaying = false;
				mediaElement.Pause();
				cts.Cancel();
			}

			else
			{
				isPlaying = true;
				mediaElement.Play();
				cts = new CancellationTokenSource();
				_ = UpdateUIAsync(cts.Token);
			}
		}

		private void OnSongSelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox listBox = sender as ListBox;
			Song selectedSong = listBox?.SelectedItem as Song;

			if (selectedSong != null)
			{
				PlaySong(selectedSong);
			}
		}

		private void positionSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (mediaElement.NaturalDuration.HasTimeSpan)
			{
				mediaElement.Position = TimeSpan.FromSeconds(positionSlider.Value);
			}
		}

		private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
		{
			SkipToNextSong();
		}





		private void loadLibrary()
		{
			listBoxSongs.ItemsSource = m_LibraryViewModel.LoadSongsFromLibrary();
		}

		private async void PlaySong(Song song)
		{
			if (song != null)
			{
				mediaElement.Source = new Uri(song.FilePath, UriKind.Absolute);
				isPlaying = true;
				mediaElement.Play();
				await UpdateUIAsync(cts.Token);
			}
		}

		private async Task UpdateUIAsync(CancellationToken token)
		{
			try
			{
				while (isPlaying)
				{
					if (mediaElement.NaturalDuration.HasTimeSpan)
					{
						positionSlider.Value = mediaElement.Position.TotalSeconds;
						textCurrentTime.Text = mediaElement.Position.ToString(@"mm\:ss");
						
					}

					await Task.Delay(1000, token); 
				}
			}
			catch (TaskCanceledException)
			{
				// Handle the cancellation if needed
			}
		}

		private void SkipToNextSong()
		{
			if (listBoxSongs.Items.Count > 0)
			{
				if (listBoxSongs.SelectedIndex < listBoxSongs.Items.Count - 1)
				{
					listBoxSongs.SelectedIndex++;
				}
				else
				{
					listBoxSongs.SelectedIndex = 0;
				}

				Song selectedSong = listBoxSongs.SelectedItem as Song;

				if (selectedSong != null)
				{
					PlaySong(selectedSong);
				}
			}
		}

		private void SkipToPreviousSong()
		{
			if (listBoxSongs.Items.Count > 0 && listBoxSongs.SelectedIndex > 0)
			{
				listBoxSongs.SelectedIndex--;

				Song selectedSong = listBoxSongs.SelectedItem as Song;

				if (selectedSong != null)
				{
					PlaySong(selectedSong);
				}
			}
		}

		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			Button btnEdit = sender as Button;
			Song selectedSong = btnEdit.DataContext as Song;

			if (selectedSong != null)
			{
				MusicEditorWindow musicEditorWindow = new MusicEditorWindow(selectedSong);
				musicEditorWindow.ShowDialog();
			}

			loadLibrary();
		}
	}
}