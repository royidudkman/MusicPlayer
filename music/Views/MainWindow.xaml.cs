using MusicPlayer.Models;
using MusicPlayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace MusicPlayer.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainViewModel m_MainViewModel;
		private bool isPlaying = false;
		public MainWindow()
		{
			InitializeComponent();
			m_MainViewModel = new MainViewModel();
			DataContext = m_MainViewModel;	
			
		}

		private void buttonUpload_Click(object sender, RoutedEventArgs e)
		{
			m_MainViewModel.LoadSongs();
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

		private void PlaySong(Song song)
		{
			if (song != null)
			{
				mediaElement.Source = new Uri(song.FilePath, UriKind.Absolute);
				isPlaying = true;
				mediaElement.Play();
			}
		}

		private void btnPrevious_Click(object sender, RoutedEventArgs e)
		{
			if(listBoxSongs.Items.Count > 0 && listBoxSongs.SelectedIndex > 0)
			{
				listBoxSongs.SelectedIndex--;

				Song selectedSong = listBoxSongs.SelectedItem as Song;

				if(selectedSong != null)
				{
					PlaySong(selectedSong);
				}
			}
		}

		private void btnNext_Click(object sender, RoutedEventArgs e)
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

		private void btnPlayPause_Click(object sender, RoutedEventArgs e)
		{
			if (isPlaying)
			{
				isPlaying = false;
				mediaElement.Pause();
			}

			else
			{
				isPlaying = true;
				mediaElement.Play();
			}
		}

	}
}
