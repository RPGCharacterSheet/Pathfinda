using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Pathfinda.Converters
{
    public class BABConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // expected input: List<int>
            // output: +6/+1, or +2, or +14/+11/+4
            List<int> thing = value as List<int>;
            if (thing != null)
            {
                string returnMe = "";
                foreach (int t in thing)
                {
                    returnMe += $"+{t}/";
                }
                return returnMe.Trim('/');
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
