using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace WPF_EoB.DataConverters
{
    public class TextureIndexToViewPortConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is byte))
                throw new ArgumentException("TextureIndexToViewPortConverter attempted to convert a value which was not a byte.");

            Rect returnValue = new Rect();
            
            if ((byte)value > 0)
            {
                byte texIndex = (byte)value;
                texIndex = --texIndex;

                returnValue.X = 0.5 * texIndex;
                returnValue.Y = 0;
                returnValue.Width = 0.5;
                returnValue.Height = 1;
            }

            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
