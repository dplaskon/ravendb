namespace RavenAdmin
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Windows.Data;
	using System.Windows.Media;

	public class DocumentTypeToBrushConverter : IValueConverter
	{
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return Convert(value.ToString());
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}

		#endregion

		public static SolidColorBrush Convert(string type)
		{
			if (type.EndsWith("s"))
				type = string.Concat(type.Take(type.Length - 1));
			switch (type.ToLower())
			{
				case "album":
					return new SolidColorBrush(new Color {R = 0x85, G = 0x85, B = 0xE0, A = 0xFF});
				case "artist":
					return new SolidColorBrush(new Color {R = 0xE0, G = 0xE0, B = 0x85, A = 0xFF});
				case "genre":
					return new SolidColorBrush(new Color {R = 0xE0, G = 0x85, B = 0x85, A = 0xFF});
				case "order":
					return new SolidColorBrush(new Color {R = 0x85, G = 0xE0, B = 0xE0, A = 0xFF});
				default:
					return new SolidColorBrush(new Color {R = 0x85, G = 0x85, B = 0xE0, A = 0xFF});
			}
		}
	}
}