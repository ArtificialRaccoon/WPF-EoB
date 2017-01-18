using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace WPF_EoB.DataConverters
{
    public class NPCToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!(value is Classes.NPCClass))
                throw new ArgumentException("NPCToVisibilityConverter attempted to convert a value which was not an NPC.");

            Visibility returnValue = Visibility.Hidden;
            Classes.NPCClass inputNPC = (Classes.NPCClass)value;

            if (inputNPC.Name != string.Empty)
                returnValue = Visibility.Visible;

            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
