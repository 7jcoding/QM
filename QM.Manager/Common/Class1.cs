using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace QM.Manager.Common {
    public class FontAwesome : MarkupExtension {
        public FontDescription Font { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            var charactor = typeof(FontDescription).GetField(Font.ToString())
                                .GetCustomAttribute<CharAttribute>()
                                .Value;

            return charactor.ToString();
        }
    }


    [AttributeUsage(AttributeTargets.Field)]
    sealed class CharAttribute : Attribute {
        public char Value { get; private set; }

        public CharAttribute(char value) {
            this.Value = value;
        }
    }

    public enum FontDescription {
        [Char('\uf021')]
        Refresh,
        [Char('\uf28e')]
        Stop,
        [Char('\uf044')]
        Edit,
        [Char('\uf014')]
        Remove,
        [Char('\uf01d')]
        Play,
        [Char('\uf28c')]
        Pause
    }
}
