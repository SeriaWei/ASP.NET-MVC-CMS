using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Data;
using Easy.Web.CMS.Media;

namespace Easy.CMS.Common.ViewModels
{
    public class MediaViewModel
    {
        public MediaEntity Parent { get; set; }
        public IEnumerable<MediaEntity> Medias { get; set; }
        public Pagination Pagin { get; set; }
    }
}