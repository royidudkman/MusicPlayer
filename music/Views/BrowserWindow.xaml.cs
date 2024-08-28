using MusicPlayer.Services;
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
	/// Interaction logic for BrowserWindow.xaml
	/// </summary>
	public partial class BrowserWindow : Window
	{
		private YoutubeViewModel m_YoutubeViewModel;
		public BrowserWindow()
		{
			InitializeComponent();
			InitializeBrowser();

			m_YoutubeViewModel = new YoutubeViewModel();

		}

		private async void InitializeBrowser()
		{
			await browserYoutube.EnsureCoreWebView2Async(null);
			browserYoutube.CoreWebView2.Navigate("http://www.youtube.com");
		}

		private void btnDownload_Click(object sender, RoutedEventArgs e)
		{
			string currentUrl = browserYoutube.Source.ToString();
			m_YoutubeViewModel.DownloadFromYoutube(currentUrl);

        }
    }
}
