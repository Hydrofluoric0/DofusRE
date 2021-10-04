using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.d2i
{
    public sealed record I18nIndexedText(int Key, string Text, bool IsDiacritical, string UndiacriticalText=null) : I18nText(Text);
}
