/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.IOC;
using System;

namespace Easy.Models
{
    public interface IImage : IEntity
    {
        string ImageUrl { get; set; }
        string ImageThumbUrl { get; set; }
    }
}
