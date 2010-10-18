namespace RavenAdmin
{
	using System;
	using System.Globalization;
	using System.Linq;
	using System.Windows.Data;
	using System.Windows.Media;
	using Expression.Blend.SampleData.CollectionsData;

	public class MagnitudeToListConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var item = value as Item;
			if (item == null)
			{
				return new[] {new SolidColorBrush(Colors.Blue), new SolidColorBrush(Colors.Green)};
			}
			var count = System.Convert.ToInt16(item.Count);

			var brush = DocumentTypeToBrushConverter.Convert(item.Name);
			return Enumerable.Range(1, count/100).Select(x => brush);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}