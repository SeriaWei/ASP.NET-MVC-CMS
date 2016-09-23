/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using Easy.MetaData;
using Easy.Models;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(CarouselEntityMetaData))]
    public class CarouselEntity : EditorEntity
    {
        public long? ID { get; set; }

        public int? Height { get; set; }

        public List<CarouselItemEntity> CarouselItems { get; set; }

    }
    class CarouselEntityMetaData : DataViewMetaData<CarouselEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Carousel");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
            DataConfig(m => m.CarouselItems).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Title).AsTextBox().Required().Order(1);
            ViewConfig(m => m.CarouselItems).AsListEditor().Order(2);
            ViewConfig(m => m.Height).AsHidden();
        }
    }

}