using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CressemFramework.Converter
{
	public class BoolToVisibilityConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool boolValue)
			{
				return boolValue ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
			}

			return System.Windows.Visibility.Collapsed;
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is System.Windows.Visibility visibility)
			{
				return visibility == System.Windows.Visibility.Visible;
			}

			return false;
		}
	}
}
