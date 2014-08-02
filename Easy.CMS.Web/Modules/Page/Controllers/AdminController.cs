using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Attribute;
using Easy.Web.Controller;
using Easy.CMS.Filter;
using Easy.CMS.Widget;
using Easy.Extend;
using Easy.Constant;
using Easy.RepositoryPattern;

namespace Easy.CMS.Page.Controllers
{

    public class AdminController : BasicController<PageEntity, string, PageService>
    {
        [AdminTheme]
        public override System.Web.Mvc.ActionResult Index()
        {
            return base.Index();
        }
        [AdminTheme]
        public JsonResult GetPageTree(string id)
        {
            id = id ?? string.Empty;
            var pages = Service.Get(new Data.DataFilter());
            var node = new Easy.HTML.jsTree.Tree<PageEntity>().Source(pages).ToNode(m => m.ID, m => m.PageName, m => m.ParentId, "0");
            return Json(node, JsonRequestBehavior.AllowGet);
        }
        [AdminTheme]
        public override ActionResult Create()
        {
            return base.Create();
        }
        [AdminTheme]
        public override ActionResult Create(PageEntity entity)
        {
            return base.Create(entity);
        }
        [AdminTheme]
        public override ActionResult Edit(string id)
        {
            return base.Edit(id);
        }
        [AdminTheme]
        [HttpPost]
        public override ActionResult Edit(PageEntity entity)
        {
            return base.Edit(entity);
        }
        [EditWidget]
        public ActionResult Design()
        {
            return View();
        }
        [HttpPost]
        public JsonResult DeleteWidget(string ID)
        {
            WidgetService widgetService = new WidgetService();
            WidgetBase widget = widgetService.Get(ID);
            if (widget != null)
            {
                widget.CreateServiceInstance().Delete(ID);
                return Json(true);
            }
            return Json(false);
        }
        public JsonResult SaveWidgetPosition(List<WidgetBase> widgets)
        {
            WidgetService widgetService = new WidgetService();
            widgets.Each(m =>
            {
                widgetService.Update(m, new Data.DataFilter(new List<string> { "Position" }).Where<WidgetBase>(n => n.ID, OperatorType.Equal, m.ID));
            });
            return Json(true);
        }
    }
}
