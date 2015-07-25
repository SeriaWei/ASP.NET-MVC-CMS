using Easy.HTML.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Easy.HTML
{

    public class TagsHelper
    {
        string Key;
        Dictionary<string, HtmlTagBase> Attributes;
        PropertyInfo targetType;
        Type modelType;
        Type dataType;
        public TagsHelper(string Key, ref Dictionary<string, HtmlTagBase> Attributes, Type modelType, PropertyInfo targetType)
        {
            this.Key = Key;
            this.Attributes = Attributes;
            this.targetType = targetType;
            this.modelType = modelType;
            if (targetType.PropertyType.Name == "Nullable`1")
            {
                dataType = targetType.PropertyType.GetGenericArguments()[0];
            }
            else
            {
                dataType = targetType.PropertyType;
            }
        }
        /// <summary>
        /// 基本输入框
        /// </summary>
        /// <returns></returns>
        public TextBoxHtmlTag AsTextBox()
        {
            TextBoxHtmlTag tag = new TextBoxHtmlTag(modelType, Key);
            if (Attributes.ContainsKey(this.Key))
            {
                Attributes.Remove(this.Key);
            }
            tag.DataType = dataType;
            Attributes.Add(this.Key, tag);
            return tag;
        }
        /// <summary>
        /// 基本输入框
        /// </summary>
        /// <returns></returns>
        public MutiLineTextBoxHtmlTag AsMutiLineTextBox()
        {
            MutiLineTextBoxHtmlTag tag = new MutiLineTextBoxHtmlTag(modelType, Key);
            if (Attributes.ContainsKey(this.Key))
            {
                Attributes.Remove(this.Key);
            }
            tag.DataType = dataType;
            Attributes.Add(this.Key, tag);
            return tag;
        }
        /// <summary>
        /// 下拉框
        /// </summary>
        /// <returns></returns>
        public DropDownListHtmlTag AsDropDownList()
        {
            DropDownListHtmlTag tag = new DropDownListHtmlTag(modelType, Key);
            if (Attributes.ContainsKey(this.Key))
            {
                Attributes.Remove(this.Key);
            }
            tag.DataType = dataType;
            Attributes.Add(this.Key, tag);
            return tag;
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        public FileHtmlTag AsFileUp()
        {
            FileHtmlTag tag = new FileHtmlTag(modelType, Key);
            if (Attributes.ContainsKey(this.Key))
            {
                Attributes.Remove(this.Key);
            }
            tag.DataType = dataType;
            Attributes.Add(this.Key, tag);
            return tag;
        }
        /// <summary>
        /// 多选项
        /// </summary>
        /// <returns></returns>
        public MutiSelectHtmlTag AsMutiSelect()
        {
            MutiSelectHtmlTag tag = new MutiSelectHtmlTag(modelType, Key);
            if (Attributes.ContainsKey(this.Key))
            {
                Attributes.Remove(this.Key);
            }
            tag.DataType = dataType;
            Attributes.Add(this.Key, tag);
            return tag;
        }
        /// <summary>
        /// 密码输入框
        /// </summary>
        /// <returns></returns>
        public PassWordHtmlTag AsPassWord()
        {
            PassWordHtmlTag tag = new PassWordHtmlTag(modelType, Key);
            if (Attributes.ContainsKey(this.Key))
            {
                Attributes.Remove(this.Key);
            }
            tag.DataType = dataType;
            Attributes.Add(this.Key, tag);
            return tag;
        }
        /// <summary>
        /// 隐藏框
        /// </summary>
        /// <returns></returns>
        public HiddenHtmlTag AsHidden()
        {
            HiddenHtmlTag tag = new HiddenHtmlTag(modelType, Key);
            if (Attributes.ContainsKey(this.Key))
            {
                Attributes.Remove(this.Key);
            }
            tag.DataType = dataType;
            Attributes.Add(this.Key, tag);
            return tag;
        }
        /// <summary>
        /// 勾选框
        /// </summary>
        /// <returns></returns>
        public CheckBoxHtmlTag AsCheckBox()
        {
            CheckBoxHtmlTag tag = new CheckBoxHtmlTag(modelType, Key);
            if (Attributes.ContainsKey(this.Key))
            {
                Attributes.Remove(this.Key);
            }
            tag.DataType = dataType;
            Attributes.Add(this.Key, tag);
            return tag;
        }
        public CollectionAreaTag AsCollectionArea()
        {
            CollectionAreaTag tag = new CollectionAreaTag(modelType, Key);
            if (Attributes.ContainsKey(this.Key))
            {
                Attributes.Remove(this.Key);
            }
            tag.DataType = dataType;
            Attributes.Add(this.Key, tag);
            return tag;
        }
    }
}
