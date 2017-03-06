/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.ViewPort.Grid;
using Easy.ViewPort.Validator;
using Easy.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.ViewPort.Descriptor
{
    public abstract class BaseDescriptor
    {

        public BaseDescriptor(Type modelType, string property)
        {
            Validator = new List<ValidatorBase>();
            Classes = new List<string>();
            Properties = new Dictionary<string, string>();
            Styles = new Dictionary<string, string>();
            GridSetting = new GridSetting();
            this.ModelType = modelType;
            this.Name = property;
            this.OrderIndex = 100;
            this.IsShowForEdit = true;
            this.IsShowForDisplay = true;
        }
        #region Private

        /// <summary>
        /// 数据类型
        /// </summary>
        public Type ModelType { get; private set; }
        #endregion

        #region 公共属性
        /// <summary>
        /// 标签类型
        /// </summary>
        public HTMLEnumerate.HTMLTagTypes TagType { get; set; }
        /// <summary>
        /// CSS样式Class集合
        /// </summary>
        public List<string> Classes
        {
            get;
            set;
        }
        /// <summary>
        /// 标签属性集合
        /// </summary>
        public Dictionary<string, string> Properties
        {
            get;
            set;
        }
        /// <summary>
        /// 样式Style
        /// </summary>
        public Dictionary<string, string> Styles
        {
            get;
            set;
        }
        /// <summary>
        /// 名称ID
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        public object DefaultValue { get; set; }

        /// <summary>
        /// 在列表显示时的配置
        /// </summary>
        public GridSetting GridSetting
        {
            get;
            set;
        }
        /// <summary>
        /// 排序索引
        /// </summary>
        public int OrderIndex { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type DataType { get; set; }
        /// <summary>
        /// 验证信息
        /// </summary>
        public List<ValidatorBase> Validator { get; set; }
        /// <summary>
        /// 只读
        /// </summary>
        public bool IsReadOnly { get; set; }
        /// <summary>
        /// 必填
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// 是否在只读视图中显示
        /// </summary>
        public bool IsShowForDisplay { get; set; }
        public bool IsShowForEdit { get; set; }
        public bool IsIgnore { get; set; }

        public bool IsHidden { get; set; }
        /// <summary>
        /// 显示模板
        /// </summary>
        public string TemplateName { get; set; }

        public string ValueFormat { get; set; }
        #endregion
    }
}
