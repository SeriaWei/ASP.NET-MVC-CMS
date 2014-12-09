using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
using Easy.Models;

namespace BPP.Modules.Product
{
    [DataConfigure(typeof(ProductEntityMetaData))]
    public class ProductEntity : EditorEntity, IImage
    {
        public long ID { get; set; }
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
        public string TypeCD { get; set; }
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
        public string Remark { get; set; }

        public string SEOTitle { get; set; }
        public string SEOKeyWord { get; set; }
        public string SEODescription { get; set; }
        public string UpImage { get; set; }
        public int OrderIndex { get; set; }
        public string SourceFrom { get; set; }
        public string Url { get; set; }
        public bool IsPassed { get; set; }
        public string TargetFrom { get; set; }
        public string TargetUrl { get; set; }
    }
    class ProductEntityMetaData : DataViewMetaData<ProductEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Product");
            DataConfig(m => m.UpImage).Ignore();
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Title).AsTextBox().Required().Order(0);
            ViewConfig(m => m.BrandCD).AsHidden();
            ViewConfig(m => m.TypeCD).AsDropDownList().DataSource("Product_TypeCD", Easy.Constant.SourceType.Dictionary);
            ViewConfig(m => m.ShelfLife).AsHidden();
            ViewConfig(m => m.Norm).AsHidden();
            ViewConfig(m => m.Color).AsHidden();
            ViewConfig(m => m.PurchasePrice).AsHidden();
            ViewConfig(m => m.UpImage).AsFileUp().HideInGrid();
            ViewConfig(m => m.Remark).AsMutiLineTextBox().Ignore().HideInGrid();
            ViewConfig(m => m.Description).AsMutiLineTextBox();
            ViewConfig(m => m.IsPassed).AsCheckBox();
        }
    }

}
