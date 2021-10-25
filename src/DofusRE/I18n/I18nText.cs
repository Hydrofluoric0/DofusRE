using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.I18n
{
    public abstract class I18nText
    {
        public object Key { get; set; }
        public string Text { get; set; }

        public I18nText(object key, string text)
        {
            this.Key = key;
            this.Text = text;
        }
    }
}
