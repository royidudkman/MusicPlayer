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
using System.Windows.Shapes;

namespace MusicPlayer.Views
{
	/// <summary>
	/// Interaction logic for MusicEditorWindow.xaml
	/// </summary>
	public partial class MusicEditorWindow : Window
	{
		private Song m_Song;
		private MusicViewModel m_MusicViewModel;
		public MusicEditorWindow(Song i_Song)
		{
			InitializeComponent();
			m_Song = i_Song;
			m_MusicViewModel = new MusicViewModel();
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			m_Song.Title = string.IsNullOrEmpty(textBoxTitle.Text) ? m_Song.Title : textBoxTitle.Text;
			m_Song.Album = string.IsNullOrEmpty(textBoxAlbum.Text) ? m_Song.Album : textBoxAlbum.Text;

			if (!string.IsNullOrEmpty(textBoxArtist.Text))
			{
				if (m_Song.Artist == null || m_Song.Artist.Length == 0)
				{
					m_Song.Artist = new string[1];
				}

				m_Song.Artist[0] = textBoxArtist.Text;
			}

			if (!string.IsNullOrEmpty(textBoxGenre.Text))
			{
				if (m_Song.Genres == null || m_Song.Genres.Length == 0)
				{
					m_Song.Genres = new string[1];
				}

				m_Song.Genres[0] = textBoxGenre.Text;
			}

			m_MusicViewModel.UpdateSongProperties(m_Song);

			Close();
		}
	}
}
