using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM.Shell.Attributes {

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CalendarEditorForAttribute : Attribute {

        public Type CalendarType {
            get;
            private set;
        }
        public CalendarEditorForAttribute(Type calendarType) {
            this.CalendarType = calendarType;
        }

    }
}
