using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
using Easy.Reflection;
using System.Web;
using Easy.HTML.Tags;
using Microsoft.Practices.ServiceLocation;

namespace Easy.HTML
{
    public class ViewModelDecode<T>
    {
        private readonly DataConfigureAttribute _attribute;
        private readonly T _entity;
        private readonly bool _withValue = false;

        public ViewModelDecode()
        {
            var entityType = typeof (T);
            _attribute = DataConfigureAttribute.GetAttribute<T>();
        }

        public ViewModelDecode(T Entity)
        {
            Type entityType;
            if (Entity != null)
            {
                _entity = Entity;
                _withValue = true;
                entityType = _entity.GetType();
                _attribute = DataConfigureAttribute.GetAttribute(entityType);
            }
            else
            {
                _entity = ServiceLocator.Current.GetInstance<T>();
                entityType = typeof (T);
                _attribute = DataConfigureAttribute.GetAttribute<T>();
            }
            if (_attribute == null)
            {
                throw new Exception(entityType.FullName + "未使用特性,请在其上使用[EasyFrameWork.Attribute.DataConfigureAttribute]特性！");
            }
        }
        public IDictionary<string, object> ExtendPropertyValue { get; set; }
        /// <summary>
        /// 获取所有可显示属性标签
        /// </summary>
        /// <param name="widthLabel">是否显示Label</param>
        /// <returns></returns>
        public List<string> GetViewModelPropertyHtmlTag(bool widthLabel)
        {
            List<string> lists = new List<string>();
            foreach (var item in _attribute.GetHtmlTags(false))
            {
                if (_withValue)
                {
                    if (item is DropDownListHtmlTag)
                    {
                        DropDownListHtmlTag tag = item as DropDownListHtmlTag;
                        if (tag.SourceType == Constant.SourceType.ViewData &&
                           ExtendPropertyValue.ContainsKey(tag.SourceKey))
                        {
                            if (ExtendPropertyValue[tag.SourceKey] is Dictionary<string, string>)
                            {
                                tag.DataSource(ExtendPropertyValue[tag.SourceKey] as Dictionary<string, string>);
                            }
                        }
                    }
                    object Val = ClassAction.GetObjPropertyValue(_entity, item.Name);
                    item.SetValue(Val);
                }
                lists.Add(item.ToString(widthLabel));
            }
            return lists;
        }
        public List<HtmlTagBase> GetViewModelPropertyHtmlTag()
        {
            List<HtmlTagBase> results = new List<HtmlTagBase>();
            foreach (var item in _attribute.GetHtmlTags(false))
            {
                object val = ClassAction.GetObjPropertyValue(_entity, item.Name);
                item.SetValue(val);
                results.Add(item);
            }
            return results;
        }
        /// <summary>
        /// 获取所有隐藏控件
        /// </summary>
        /// <returns></returns>
        public List<string> GetViewModelHiddenTargets()
        {
            List<string> lists = new List<string>();
            foreach (var item in _attribute.GetHtmlHiddenTags())
            {
                if (this._withValue)
                {
                    object Val = ClassAction.GetObjPropertyValue(this._entity, item.Name);
                    item.SetValue(Val);
                }
                lists.Add(item.ToString(false));
            }
            return lists;
        }
        /// <summary>
        /// 获取对应属性的标签
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetViewModelPropertyHtmlTag(string property)
        {
            object Val = ClassAction.GetObjPropertyValue(this._entity, property);
            var html = _attribute.GetHtmlTag(property);
            html.SetValue(Val);
            return html.ToString();
        }
        /// <summary>
        /// 获取对应属性的显示名称
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetPropertyDisplayName(string property)
        {
            return _attribute.GetDisplayName(property);
        }
        
    }
}
