using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using WPF_EoB.Classes;

namespace WPF_EoB.DataConverters
{
    public class WindowSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter == null)
                throw new ArgumentNullException("WindowSizeConverter Parameter cannot be null");            
            if (value == null)
                throw new ArgumentNullException("WindowSizeConverter requires a non-null value");
            if (!(value is double))
                throw new ArgumentException("WindowSizeConverter requires a value of type double");
            if (!(parameter is string))
                throw new ArgumentException("WindowSizeConverter parameter must be of type string");
            
            bool isWidth = false;
            double returnValue = 0.0;

            bool.TryParse((string)parameter, out isWidth);
            double input = (double)value;

            if(isWidth)
            {
                if(input > Constants.UnscaledWidth)
                {
                    uint scaleFactor = (uint)input / Constants.UnscaledWidth;
                    returnValue = (double)(scaleFactor * Constants.UnscaledWidth);
                }
                else { returnValue = Constants.UnscaledWidth; }
            }
            else
            {
                if (input > Constants.UnscaledHeight)
                {
                    uint scaleFactor = (uint)input / Constants.UnscaledHeight;
                    returnValue = (double)(scaleFactor * Constants.UnscaledHeight);
                }
                else { returnValue = Constants.UnscaledHeight; }
            }

            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
