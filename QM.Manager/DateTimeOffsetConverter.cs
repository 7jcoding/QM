using System;
using System.Windows.Data;

namespace QM.Manager {
    public class DateTimeOffsetConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if (value == null)
                return null;

            var df = (DateTimeOffset)value;
            if (df != null) {
                return df.LocalDateTime;
            } else
                return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
