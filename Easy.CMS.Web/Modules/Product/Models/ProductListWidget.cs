using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;

namespace Easy.CMS.Product.Models
{
    [DataConfigure(typeof(ProductListWidgetMetaData))]
    public class ProductListWidget : WidgetBase
    {

    }

    class ProductListWidgetMetaData : WidgetMetaData<ProductListWidget>
    {

    }

}