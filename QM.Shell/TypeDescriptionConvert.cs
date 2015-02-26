using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QM.Shell {
    public class TypeDescriptionConvert : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            try {
                var t = (Type)value;
                var desc = t.GetCustomAttributes(false).OfType<DescriptionAttribute>().FirstOrDefault();
                return desc == null ? t.Name : desc.Description;
            } catch {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
