using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace WPF_EoB.DataConverters
{
    public class RotationToViewPortConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Classes.Enumerations.Direction))
                throw new ArgumentException("RotationToViewPortConverter attempted to convert a value which was not a direction enumeration.");

            Rect returnValue = new Rect();
            byte texIndex = 0;

            switch (((Classes.Enumerations.Direction)value))
            {
                case Classes.Enumerations.Direction.East:
                    texIndex = 1;
                    break;
                case Classes.Enumerations.Direction.South:
                    texIndex = 2;
                    break;
                case Classes.Enumerations.Direction.West:
                    texIndex = 3;
                    break;
                default:
                    texIndex = 0;
                    break;
            }

            returnValue.X = 0.25 * texIndex;
            returnValue.Y = 0;
            returnValue.Width = 0.25;
            returnValue.Height = 1;

            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
