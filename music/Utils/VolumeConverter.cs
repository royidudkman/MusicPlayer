using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MusicPlayer.Utils
{
	internal class VolumeConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double volume = (double)value;
			return volume / 100;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			double volume = (double)value;
			return volume * 100;
		}
	}
}
