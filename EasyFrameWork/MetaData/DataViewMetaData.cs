/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Easy.ViewPort;
using Easy.ViewPort.Descriptor;
using Easy.Data;
using Easy.Constant;
using Easy.Models;
using System.ComponentModel;
using System.Reflection;
using Easy;
using Easy.Extend;
using Microsoft.Practices.ServiceLocation;

namespace Easy.MetaData
{
    /// <summary>
    /// 数据元数据特性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DataViewMetaData<T> : IDataViewMetaData where T : class
    {
        public DataViewMetaData()
        {
            Init();
        }
        public void Init()
        {
            ViewPortDescriptors = new Dictionary<string, BaseDescriptor>();
            PropertyDataConfig = new Dictionary<string, PropertyDataInfo>();
            DataRelations=new List<Relation>();
            Alias = "T0";
            TargetType = typeof(T);
            foreach (var item in TargetType.GetProperties())
            {
                TypeCode code = Type.GetTypeCode(item.PropertyType.IsGenericType ? item.PropertyType.GetGenericArguments()[0] : item.PropertyType);
                switch (code)
                {
                    case TypeCode.Boolean:
                        ViewConfig(item.Name).AsCheckBox().SetColumnWidth(75);
                        break;
                    case TypeCode.Char:
                        ViewConfig(item.Name).AsTextBox().MaxLength(1).RegularExpression(RegularExpression.Letters);
                        break;
                    case TypeCode.DateTime:
                        ViewConfig(item.Name).AsTextBox().FormatAsDate().SetColumnWidth(140);
                        break;
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                        ViewConfig(item.Name).AsTextBox().RegularExpression(RegularExpression.PositiveIntegersAndZero).SetColumnWidth(75);
                        break;
                    case TypeCode.SByte:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                        ViewConfig(item.Name).AsTextBox().RegularExpression(RegularExpression.Integer).SetColumnWidth(75);
                        break;
                    case TypeCode.Object:
                        ViewConfig(item.Name).AsHidden().Ignore();
                        break;
                    case TypeCode.Single:
                    case TypeCode.Double:
                    case TypeCode.Decimal:
                        ViewConfig(item.Name).AsTextBox().RegularExpression(RegularExpression.Float).SetColumnWidth(75);
                        break;
                    case TypeCode.String:
                        ViewConfig(item.Name).AsTextBox().MaxLength(200).SetColumnWidth(200);
                        break;
                    case TypeCode.DBNull:
                    case TypeCode.Byte:
                    case TypeCode.Empty:
                    default: ViewConfig(item.Name).AsTextBox();
                        break;
                }
                if (code == TypeCode.Object)
                {
                    DataConfig(item.Name).Ignore();
                }
                else
                {
                    DataConfig(item.Name);
                }
            }
            if (typeof(EditorEntity).IsAssignableFrom(TargetType))
            {
                ViewConfig("CreateBy").AsHidden();
                ViewConfig("CreatebyName").AsTextBox().Hide().SetColumnWidth(80);
                ViewConfig("CreateDate").AsTextBox().Hide().FormatAsDateTime().SetColumnWidth(140);

                ViewConfig("LastUpdateBy").AsHidden();
                ViewConfig("LastUpdateByName").AsTextBox().Hide().SetColumnWidth(80);
                ViewConfig("LastUpdateDate").AsTextBox().Hide().FormatAsDateTime().SetColumnWidth(140);
                ViewConfig("ActionType").AsHidden().AddClass("ActionType");
                ViewConfig("Title").AsTextBox().Order(1).SetColumnWidth(200);
                ViewConfig("Description").AsTextArea().SetColumnWidth(250).Order(101);
                ViewConfig("Status").AsDropDownList().DataSource(DicKeys.RecordStatus, SourceType.Dictionary).SetColumnWidth(70);

                DataConfig("CreateBy").Update(false);
                DataConfig("CreatebyName").Update(false);
                DataConfig("CreateDate").Update(false);
                DataConfig("ActionType").Ignore();
            }
            if (typeof(IImage).IsAssignableFrom(TargetType))
            {
                ViewConfig("ImageUrl").AsTextBox().HideInGrid();
                ViewConfig("ImageThumbUrl").AsTextBox().HideInGrid();
            }
            if (IsIgnoreBase())
            {
                IgnoreBase();
            }
            DataConfigure();
            ViewConfigure();
        }

        public Dictionary<string, BaseDescriptor> ViewPortDescriptors
        {
            get;
            set;
        }

        public Type TargetType
        {
            get;
            private set;
        }

        public Dictionary<string, PropertyDataInfo> PropertyDataConfig
        {
            get;
            set;
        }
        IUser _user;
        public IUser User
        {
            get
            {
                var app = ServiceLocator.Current.GetInstance<IApplicationContext>();
                if (app != null)
                {
                    _user = app.CurrentUser;
                }
                return _user;
            }
        }

        public List<Relation> DataRelations { get; set; }
        public List<PrimaryKey> Primarykey
        {
            get
            {
                return PropertyDataConfig.Where(item => item.Value.IsPrimaryKey).ToList(m => new PrimaryKey
                    {
                        ColumnName = m.Value.ColumnName,
                        PropertyName = m.Value.PropertyName
                    });
            }
        }

        public Dictionary<string, PropertyInfo> Properties
        {
            get
            {
                var properties = new Dictionary<string, PropertyInfo>();
                TargetType.GetProperties().Each(m => properties.Add(m.Name, m));
                return properties;
            }
        }

        public int PrimarykeyCount
        {
            get;
            set;
        }
        /// <summary>
        /// 数据表
        /// </summary>
        public string Table
        {
            get;
            private set;
        }
        /// <summary>
        /// 别名
        /// </summary>
        public string Alias
        {
            get;
            private set;
        }
        /// <summary>
        /// 数据配置 方法[DataConfig][DataTable][DataPrimaryKey]
        /// </summary>
        protected abstract void DataConfigure();
        /// <summary>
        /// 视图配置 方法[ViewConfig]
        /// </summary>
        protected abstract void ViewConfigure();
        public virtual DataFilter DataAccess(DataFilter filter)
        {
            return filter;
        }
        /// <summary>
        /// 视图配置，界面显示
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected TagsHelper ViewConfig(Expression<Func<T, object>> expression)
        {
            string key = Reflection.LinqExpression.GetPropertyName(expression.Body);
            return ViewConfig(key);
        }
        /// <summary>
        /// 视图配置，界面显示
        /// </summary>
        /// <param name="properyt">实体字段名称</param>
        /// <returns></returns>
        protected TagsHelper ViewConfig(string properyt)
        {
            return new TagsHelper(properyt, ViewPortDescriptors, TargetType, TargetType.GetProperty(properyt));
        }
        /// <summary>
        /// 主键
        /// </summary>
        /// <summary>
        ///  数据配置，与数据库对应
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        protected PropertyDataInfoHelper<T> DataConfig(Expression<Func<T, object>> expression)
        {
            string key = Reflection.LinqExpression.GetPropertyName(expression.Body);
            return DataConfig(key);
        }
        /// <summary>
        /// 数据配置，与数据库对应
        /// </summary>
        /// <param name="property">实体字段名称</param>
        /// <returns></returns>
        protected PropertyDataInfoHelper<T> DataConfig(string property)
        {
            PropertyDataInfo data;
            if (PropertyDataConfig.ContainsKey(property))
                data = PropertyDataConfig[property];
            else
            {
                data = new PropertyDataInfo(property);
                data.TableAlias = "T0";
                data.ColumnName = property;
                PropertyDataConfig.Add(property, data);
            }
            data.Ignore = false;
            return new PropertyDataInfoHelper<T>(data, this);
        }
        /// <summary>
        /// 数据表名称
        /// </summary>
        /// <param name="table">表名称</param>
        protected RelationHelper DataTable(string table)
        {
            Table = table;
            return new RelationHelper(DataRelations);
        }
        /// <summary>
        /// 将属于基类的字段全部设为DataConfig(item.Name).Ignore();
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsIgnoreBase()
        {
            return false;
        }
        private void IgnoreBase()
        {
            PropertyDescriptorCollection attrs = TypeDescriptor.GetProperties(this.TargetType.BaseType);
            foreach (PropertyDescriptor item in attrs)
            {
                DataConfig(item.Name).Ignore();
                ViewConfig(item.Name).AsHidden();
            }
        }
    }

}
