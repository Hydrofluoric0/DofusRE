using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.I18n
{
    public class I18nNamedText : I18nText
    {
        public new string Key
        {
            get => (string) base.Key;
            set => base.Key = value;
        }

        public I18nNamedText(string key, string text) : base(key, text)
        {
            this.Key = key;
            this.Text = text;
        }
    }
}
