using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
using Easy.Models;
using Easy.Web.CMS;

namespace Easy.CMS.Product.Models
{
    [DataConfigure(typeof(ProductMetaData))]
    public class Product : EditorEntity, IImage
    {
        public long? ID { get; set; }
        /// <summary>
        /// 产品图
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 产品缩略图
        /// </summary>
        public string ImageThumbUrl { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public int? BrandCD { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public int? ProductCategory { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }
        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// 折扣价格
        /// </summary>
        public decimal? RebatePrice { get; set; }
        /// <summary>
        /// 进价，成本价
        /// </summary>
        public decimal? PurchasePrice { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string Norm { get; set; }
        /// <summary>
        /// 保质期
        /// </summary>
        public string ShelfLife { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string ProductContent { get; set; }

        public string SEOTitle { get; set; }
        public string SEOKeyWord { get; set; }
        public string SEODescription { get; set; }
        public int? OrderIndex { get; set; }
        public string SourceFrom { get; set; }
        public string Url { get; set; }
        public bool IsPublish { get; set; }
        public DateTime? PublishDate { get; set; }
        public string TargetFrom { get; set; }
        public string TargetUrl { get; set; }
    }
    class ProductMetaData : DataViewMetaData<Product>
    {
        protected override void DataConfigure()
        {
            DataTable("Product");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Title).AsTextBox().Required().Order(0);
            ViewConfig(m => m.BrandCD).AsHidden();
            ViewConfig(m => m.ProductCategory).AsDropDownList().DataSource(ViewDataKeys.ProductCategory, Constant.SourceType.ViewData);
            ViewConfig(m => m.ShelfLife).AsHidden();
            ViewConfig(m => m.Norm).AsHidden();
            ViewConfig(m => m.Color).AsHidden();
            ViewConfig(m => m.PurchasePrice).AsHidden();
            ViewConfig(m => m.ProductContent).AsMutiLineTextBox().AddClass("html").HideInGrid();
            ViewConfig(m => m.Description).AsMutiLineTextBox();
            ViewConfig(m => m.IsPublish).AsTextBox().Hide();
        }
    }

}
