using Easy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(CarouselItemEntityMeta)), Serializable]
    public class CarouselItemEntity : EditorEntity
    {
        public long? ID { get; set; }

        public long? CarouselID { get; set; }
        public string CarouselWidgetID { get; set; }

        public string TargetLink { get; set; }
        public string ImageUrl { get; set; }

    }
    class CarouselItemEntityMeta : DataViewMetaData<CarouselItemEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("CarouselItem");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
            DataConfig(m => m.ActionType).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.CarouselID).AsHidden();
            ViewConfig(m => m.CarouselWidgetID).AsHidden();
            ViewConfig(m => m.Description).AsHidden();
            ViewConfig(m => m.TargetLink).AsTextBox().AddClass("select").AddProperty("data-url", Urls.SelectPage);
            ViewConfig(m => m.ImageUrl).AsTextBox().AddClass(StringKeys.SelectImageClass).AddProperty("data-url", Urls.SelectMedia);
        }
    }

}