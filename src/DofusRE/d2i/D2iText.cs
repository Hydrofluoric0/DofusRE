using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.d2i
{
    public class D2iText
    {
        public int Key;
        public string Text;
        public bool isDiacritical;
        public string UndiacriticalText;

        public D2iText(int key, string text, bool is_diac, string undiac_text = null)
        {
            this.Key = key;
            this.Text = text;
            this.isDiacritical = is_diac;
            this.UndiacriticalText = undiac_text;
        }

        public static bool operator == (D2iText a, D2iText b)
        {
            var keyMatch = a.Key == b.Key;
            var textMatch = a.Text == b.Text;
            var isDiacMatch = a.isDiacritical == b.isDiacritical;
            if (isDiacMatch)
            {
                return a.UndiacriticalText == b.UndiacriticalText;
            }
            return keyMatch && textMatch && isDiacMatch;
        }
        public static bool operator !=(D2iText a, D2iText b)
        {
            return !(a == b);
        }
    }
}
