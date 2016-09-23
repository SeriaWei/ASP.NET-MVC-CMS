/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.IO;
using Easy.Data;
using Easy.Extend;
using Easy.RepositoryPattern;

namespace Easy.Web.CMS.Media
{
    public class MediaService : ServiceBase<MediaEntity>, IMediaService
    {
        public override void Add(MediaEntity item)
        {
            item.ID = Guid.NewGuid().ToString("N");
            if (item.ParentID.IsNullOrWhiteSpace())
            {
                item.ParentID = "#";
            }
            if (item.Url.IsNotNullAndWhiteSpace())
            {
                string extension = Path.GetExtension(item.Url);
                if (Common.IsImage(extension))
                {
                    item.MediaType = (int)MediaType.Image;
                }
                else if (FileExtensions.Video.Contains(extension))
                {
                    item.MediaType = (int) MediaType.Video;
                }
                else if (FileExtensions.Zip.Contains(extension))
                {
                    item.MediaType = (int) MediaType.Zip;
                }
                else if (FileExtensions.Pdf.Contains(extension))
                {
                    item.MediaType = (int) MediaType.Pdf;
                }
                else if (FileExtensions.Txt.Contains(extension))
                {
                    item.MediaType = (int) MediaType.Txt;
                }
                else if (FileExtensions.Doc.Contains(extension))
                {
                    item.MediaType = (int) MediaType.Doc;
                }
                else if (FileExtensions.Excel.Contains(extension))
                {
                    item.MediaType = (int) MediaType.Excel;
                }
                else if (FileExtensions.Pdf.Contains(extension))
                {
                    item.MediaType = (int) MediaType.Pdf;
                }
                else
                {
                    item.MediaType = (int) MediaType.Other;
                }
            }
            else
            {
                item.MediaType = (int)MediaType.Folder;
            }
            base.Add(item);
        }

        public override int Delete(DataFilter filter)
        {
            Get(filter).Each(m =>
            {
                Delete(new DataFilter().Where("ParentID", OperatorType.Equal, m.ID));
            });
            return base.Delete(filter);
        }

        public override int Delete(params object[] primaryKeys)
        {
            var media = Get(primaryKeys);
            if (media != null)
            {
                Delete(new DataFilter().Where("ParentID", OperatorType.Equal, media.ID));
            }
            return base.Delete(primaryKeys);
        }
    }
}