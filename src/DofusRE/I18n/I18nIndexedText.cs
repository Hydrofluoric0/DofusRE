using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.I18n
{
    public sealed class I18nIndexedText : I18nText
    {
        public new int Key
        {
            get => (int) base.Key;
            set => base.Key = value;
        }

        public bool IsDiacritical
        {
            get;
            set;
        }

        public string UndiacriticalText
        {
            get;
            set;
        }
        
        public I18nIndexedText(int key, string text, bool isDiacritical, string undiacriticalText = null) : base(key, text)
        {
            this.Key = key;
            this.Text = text;
            this.IsDiacritical = isDiacritical;
            this.UndiacriticalText = undiacriticalText;
        }
    }
}
