using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DofusRE.I18n
{
    public sealed record I18nNamedText(string Key, string Text) : I18nText(Text);
}
