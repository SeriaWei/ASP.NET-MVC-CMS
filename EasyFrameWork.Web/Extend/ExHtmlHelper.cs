/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.ViewPort;
using Easy.Web.HTML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Easy.Web.Extend
{
    public static class ExHtmlHelper
    {
        public static Grid<T> Grid<T>(this HtmlHelper htmlHelper) where T : class
        {
            return new Grid<T>(htmlHelper.ViewContext);
        }

        public static Grid<TModel> Grid<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : class
        {
            return new Grid<TModel>(htmlHelper.ViewContext);
        }

        public static Tree<T> Tree<T>(this HtmlHelper htmlHelper) where T : class
        {
            return new Tree<T>(htmlHelper.ViewContext);
        }

        public static Tree<TModel> Tree<TModel>(this HtmlHelper<TModel> htmlHelper) where TModel : class
        {
            return new Tree<TModel>(htmlHelper.ViewContext);
        }
    }
}
