/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.ViewPort.Grid
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
