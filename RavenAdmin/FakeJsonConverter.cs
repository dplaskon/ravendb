namespace RavenAdmin
{
	using System;
	using System.Globalization;
	using System.Windows.Data;
	using Framework;

	public class FakeJsonConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string output = "{ ";
			var props = value.GetType().GetProperties();
			props.Apply(p => output += "\"" + p.Name + "\": \"" + p.GetValue(value, null) + "\", ");
			return output + " }";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}