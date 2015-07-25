using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Models
{
    public interface IEnterprise : IEntity
    {
        long ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        string SerialNumber { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        string Legal { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        int? IndustryID { get; set; }
        /// <summary>
        /// 行业名称
        /// </summary>
        string IndustryName { get; set; }
        /// <summary>
        /// 主营产品
        /// </summary>
        string MainProducts { get; set; }
        /// <summary>
        /// 性质
        /// </summary>
        int? InwardnessID { get; set; }
        /// <summary>
        /// 性质（国营，私营，合资，外企）
        /// </summary>
        string InwardnessName { get; set; }
        /// <summary>
        /// 规模
        /// </summary>
        int? Scale { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        string Telephone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        string Email { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        string Address { get; set; }
        /// <summary>
        /// 联系人ID
        /// </summary>
        int? ContactID { get; set; }
        /// <summary>
        /// 联系人名称
        /// </summary>
        string ContactName { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        int? CountryID { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        string CountryName { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        int? ProvinceID { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        string ProvinceName { get; set; }
        /// <summary>
        /// 主页
        /// </summary>
        string HomePage { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        string Remark { get; set; }
    }
}
