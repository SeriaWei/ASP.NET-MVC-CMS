/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Easy.CMS.Common.ViewModels;
using Easy.Data;
using Easy.Extend;
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.CMS;
using Easy.Web.CMS.Media;
using Easy.Web.Controller;
using Easy.Web.Extend;
using System.Linq;

namespace Easy.CMS.Common.Controllers
{
    [DefaultAuthorize]
    public class MediaController : BasicController<MediaEntity, string, IMediaService>
    {
        public MediaController(IMediaService service)
            : base(service)
        {
        }
        [NonAction]
        public override ActionResult Index()
        {
            return base.Index();
        }
        [AdminTheme]
        public ActionResult Index(string ParentId, int? pageIndex)
        {
            ParentId = ParentId ?? "#";
            Pagination pagin = new Pagination { PageIndex = pageIndex ?? 0 };
            var medias = Service.Get(new DataFilter().Where("ParentID", OperatorType.Equal, ParentId).OrderBy("CreateDate", OrderType.Descending), pagin);
            var viewModel = new MediaViewModel
            {
                ParentID = ParentId,
                Medias = medias,
                Pagin = pagin
            };
            if (ParentId != "#")
            {
                viewModel.Parent = Service.Get(ParentId);
                viewModel.Parents = new List<MediaEntity>();
                LoadParents(viewModel.Parent, viewModel.Parents);
            }
            return View("Index", viewModel);
        }

        private void LoadParents(MediaEntity parent, List<MediaEntity> parents)
        {
            if (parent != null)
            {
                parents.Insert(0, parent);
                if (parent.ParentID != "#")
                {
                    var p = Service.Get(parent.ParentID);
                    if (p != null)
                    {
                        LoadParents(p, parents);
                    }
                }
            }
        }

        [PopUp]
        public ActionResult Select(string ParentId, int? pageIndex)
        {
            return Index(ParentId, pageIndex);
        }
        [HttpPost]
        public JsonResult Save(string id, string title, string parentId)
        {
            MediaEntity entity = null;
            if (id.IsNotNullAndWhiteSpace() && title.IsNotNullAndWhiteSpace())
            {
                DataFilter filter = new DataFilter(new List<string> { "Title" });
                Service.Update(new MediaEntity { Title = title }, filter.Where("ID", OperatorType.Equal, id));
                entity = Service.Get(id);
            }
            else if (title.IsNotNullAndWhiteSpace())
            {
                entity = new MediaEntity { Title = title, MediaType = (int)MediaType.Folder, ParentID = parentId };
                Service.Add(entity);
            }
            return Json(entity);
        }
        [HttpPost]
        public JsonResult Upload(string parentId, string folder)
        {
            if (Request.Files.Count > 0)
            {
                if (folder.IsNotNullAndWhiteSpace())
                {
                    var parent = Service.Get(new DataFilter().Where("Title", OperatorType.Equal, folder).Where("MediaType", OperatorType.Equal, (int)MediaType.Folder))
                        .FirstOrDefault();

                    if (parent == null)
                    {
                        parent = new MediaEntity
                        {
                            Title = folder,
                            MediaType = (int)MediaType.Folder,
                            ParentID = "#"
                        };
                        Service.Add(parent);
                    }
                    parentId = parent.ID;
                }
                parentId = parentId ?? "#";
                string fileName = Request.Files[0].FileName;
                var entity = new MediaEntity
                {
                    ParentID = parentId,
                    Title = fileName
                };
                string extension = Path.GetExtension(fileName).ToLower();
                if (Web.Common.IsImage(extension))
                {
                    entity.Url = Request.SaveImage();
                }
                else
                {
                    entity.Url = Request.SaveFile();
                }
                if (entity.Url.IsNotNullAndWhiteSpace())
                {
                    Service.Add(entity);
                    entity.Url = Url.Content(entity.Url);
                }
                return Json(entity);
            }
            return Json(false);
        }
        public override JsonResult Delete(string ids)
        {
            DeleteMedia(ids);
            return base.Delete(ids);
        }

        private void DeleteMedia(string mediaId)
        {
            var media = Service.Get(mediaId);
            if (media != null && media.MediaType != (int)MediaType.Folder)
            {
                if (media.Url.StartsWith("http://") || media.Url.StartsWith("https://"))
                {
                    media.Url = "~" + new Uri(media.Url).AbsolutePath;
                }
                Request.DeleteFile(media.Url);
            }
            else
            {
                Service.Get(new DataFilter().Where("ParentID", OperatorType.Equal, mediaId))
                    .Each(m => DeleteMedia(m.ID));
            }
            Service.Delete(mediaId);
        }
    }
}
