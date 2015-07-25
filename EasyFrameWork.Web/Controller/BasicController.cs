using System.Reflection;
using Easy.Data;
using Easy.Extend;
using Easy.HTML.Grid;
using Easy.Models;
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy.Web.Extend;
using Easy.Constant;
using Easy.HTML.Tags;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.Controller
{
    /// <summary>
    /// 基本控制器，增删改查
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="P">主键类型</typeparam>
    /// <typeparam name="S">Service类型</typeparam>
    public class BasicController<T, S> : System.Web.Mvc.Controller
        where T : EditorEntity
        where S : ServiceBase<T>
    {
        /// <summary>
        /// 缩略图宽
        /// </summary>
        public int? ImageThumbWidth { get; set; }
        /// <summary>
        /// 缩略图高
        /// </summary>
        public int? ImageThumbHeight { get; set; }
        /// <summary>
        /// 业务Service
        /// </summary>
        public S Service;
        public BasicController(S service)
        {
            Service = service;
        }
        protected IImage UpLoadImage(IImage entity)
        {
            if (!string.IsNullOrEmpty(entity.ImageUrl) && string.IsNullOrEmpty(entity.ImageThumbUrl))
            {
                entity.ImageThumbUrl = entity.ImageUrl;
            }
            string filePath = Request.SaveImage();
            if (!string.IsNullOrEmpty(filePath))
            {
                entity.ImageUrl = filePath;
                string fileName = Easy.ImageUnity.SetThumb(Server.MapPath(filePath), ImageThumbWidth ?? 64, ImageThumbHeight ?? 64);
                entity.ImageThumbUrl = filePath.Replace(System.IO.Path.GetFileName(filePath), fileName);
            }
            if (string.IsNullOrEmpty(entity.ImageUrl) || string.IsNullOrEmpty(entity.ImageThumbUrl))
            {
                entity.ImageUrl = string.Empty;
                entity.ImageThumbUrl = string.Empty;
            }
            return entity;
        }

        protected object[] GetPrimaryKeys(T entity)
        {
            var primaryKey = Easy.MetaData.DataConfigureAttribute.GetAttribute<T>().MetaData.Primarykey;
            object[] primaryKeys = new object[primaryKey.Count];
            for (int i = 0; i < primaryKey.Count; i++)
            {
                primaryKeys[i] = Reflection.ClassAction.GetPropertyValue<T>(entity, primaryKey[i]);
            }
            return primaryKeys;
        }

        protected void QueryStringBindToEntity(T entity)
        {
            Type t = typeof(T);
            t.GetProperties().Each(m =>
            {
                if (m.CanWrite && Request.QueryString.AllKeys.Contains(m.Name))
                {
                    Easy.Reflection.ClassAction.SetPropertyValue(entity, m.Name, Request.QueryString[m.Name]);
                }
            });

        }
        public virtual ActionResult Index(ParamsContext context)
        {
            return View();
        }
        public virtual ActionResult Create(ParamsContext context)
        {
            var entity = Activator.CreateInstance<T>();
            QueryStringBindToEntity(entity);
            entity.Status = (int)RecordStatus.Active;
            ViewBag.Title = "添加";
            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Create(T entity)
        {
            if (ModelState.IsValid)
            {
                if (entity is IImage)
                {
                    UpLoadImage(entity as IImage);
                }
                Service.Add(entity);
                return RedirectToAction("Index");
            }
            return View(entity);
        }
        public virtual ActionResult Edit(ParamsContext context)
        {
            T entity = Service.Get(context.ID);
            return View(entity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public virtual ActionResult Edit(T entity)
        {
            if (entity.ActionType == ActionType.Delete)
            {
                Service.Delete(GetPrimaryKeys(entity));
                return RedirectToAction("Index");
            }

            ViewBag.Title = entity.Title;
            if (ModelState.IsValid)
            {
                if (entity is IImage)
                {
                    UpLoadImage(entity as IImage);
                }
                Service.Update(entity);
                return RedirectToAction("Index");
            }
            return View(entity);
        }

        [HttpPost]
        public virtual JsonResult Delete(string ids)
        {
            try
            {
                string[] id = ids.Split(',');
                List<object> listIds = new List<object>();
                foreach (string item in id)
                {
                    long test = 0;
                    if (long.TryParse(item, out test))
                    {
                        listIds.Add(test);
                    }
                    else
                    {
                        listIds.Add(item);
                    }
                }
                string primary = Easy.MetaData.DataConfigureAttribute.GetAttribute<T>().MetaData.Primarykey[0];
                int result = Service.Delete(new Easy.Data.DataFilter().Where(primary, OperatorType.In, listIds));
                if (result > 0)
                {
                    return Json(new AjaxResult() { Status = AjaxStatus.Normal, Message = ids });
                }
                else
                {
                    return Json(new AjaxResult() { Status = AjaxStatus.Warn, Message = "未删除任何数据！" });
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult() { Status = AjaxStatus.Error, Message = ex.Message });
            }
        }
        [HttpPost]
        public virtual JsonResult GetList()
        {
            GridData data = new GridData(Request.Form, (tag) =>
            {
                if (tag is DropDownListHtmlTag &&
                    (tag as DropDownListHtmlTag).SourceType == SourceType.ViewData &&
                    ViewData.ContainsKey((tag as DropDownListHtmlTag).SourceKey))
                {
                    return ViewData[(tag as DropDownListHtmlTag).SourceKey] as Dictionary<string, string>;
                }
                return null;
            });
            var filter = data.GetDataFilter<T>();
            var pagin = data.GetPagination();
            return Json(data.GetJsonDataForGrid<T>(Service.Get(filter, pagin), pagin));
        }
    }
}
