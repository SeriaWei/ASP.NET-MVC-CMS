using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.Grid
{
    public class GridSetting
    {
        public GridSetting()
        {
            ColumnWidth = 150;
            Searchable = true;
            Visiable = true;
        }
        public int ColumnWidth { get; set; }
        public bool Searchable { get; set; }
        public bool Visiable { get; set; }
    }
}
