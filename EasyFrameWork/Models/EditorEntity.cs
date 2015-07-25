using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;

namespace Easy.Models
{
    public class EditorEntity : IEntity
    {

        public string Title { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 是否通过
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreateBy { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatebyName { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime? CreateDate { get; set; }
        /// <summary>
        /// 修改人ID
        /// </summary>
        public string LastUpdateBy { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string LastUpdateByName { get; set; }
        /// <summary>
        /// 最后更新日期
        /// </summary>
        public DateTime? LastUpdateDate { get; set; }

        public Constant.ActionType? ActionType { get; set; }
    }

}
