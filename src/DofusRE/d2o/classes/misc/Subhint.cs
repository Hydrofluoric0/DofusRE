

using System;
using System.Collections.Generic;
using DofusRE.io;
using DofusRE.d2o.classes;

namespace DofusRE.d2o.classes
{
    public class Subhint : GameDataClass
    {
        public const String MODULE = "Subhints";
        public int hint_id;
        public String hint_parent_uid;
        public String hint_anchored_element;
        public int hint_anchor;
        public int hint_position_x;
        public int hint_position_y;
        public int hint_width;
        public int hint_height;
        public String hint_highlighted_element;
        public int hint_order;
        public uint hint_tooltip_text;
        public int hint_tooltip_position_enum;
        public String hint_tooltip_url;
        public int hint_tooltip_offset_x;
        public int hint_tooltip_offset_y;
        public int hint_tooltip_width;
        public double hint_creation_date;
    }
}