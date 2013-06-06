using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace VoronoiMapGen.CustomConverters
{
    [ValueConversion(typeof(float), typeof(int))]
    class MapSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            return (2 << (int)(double)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return (int)Math.Log((int)(double)value, 2);
        }
    }
}
