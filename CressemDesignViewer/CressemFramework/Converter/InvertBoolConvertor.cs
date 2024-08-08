using System;
using System.Globalization;
using System.Windows.Data;

namespace CressemFramework.Converter
{
	public class InvertBoolConvertor : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool boolValue)
			{
				return !boolValue;
			}

			return null;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool boolValue)
			{
				return !boolValue;
			}

			return null;
		}
	}
}
