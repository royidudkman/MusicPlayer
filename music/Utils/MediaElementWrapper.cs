using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MusicPlayer.Utils
{
	public class MediaElementWrapper : MediaElement
	{
		public static readonly DependencyProperty PositionChangedProperty =
		DependencyProperty.Register("PositionChanged", typeof(TimeSpan), typeof(MediaElementWrapper), new PropertyMetadata(TimeSpan.Zero));

		public TimeSpan PositionChanged
		{
			get { return (TimeSpan)GetValue(PositionChangedProperty); }
			set { SetValue(PositionChangedProperty, value); }
		}

		public MediaElementWrapper()
        {
			CompositionTarget.Rendering += OnRendering;
        }
       

		private void OnRendering(object sender, EventArgs e)
		{
			PositionChanged = Position;
		}
	}
}
