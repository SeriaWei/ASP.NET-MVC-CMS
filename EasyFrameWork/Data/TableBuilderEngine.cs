/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Data.DataBase;
using Easy.MetaData;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Data
{
    public class TableBuilderEngine
    {
        public TableBuilderEngine()
        {
            string dataBase = System.Configuration.ConfigurationManager.AppSettings[DataBasic.DataBaseAppSetingKey];
            DataBase =
                ServiceLocator.Current.GetAllInstances<DataBasic>()
                    .FirstOrDefault(m => m.DataBaseTypeNames().Any(n => n == dataBase)) ?? new Sql();
        }
        public DataBasic DataBase
        {
            get;
            private set;
        }
        public Type TargeType { get; set; }
        public void Buid<T>() where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            TargeType = typeof(T);
            System.Reflection.PropertyInfo[] propertys = TargeType.GetProperties();
            if (custAttribute != null)
            {
                if (!DataBase.IsExistTable(custAttribute.MetaData.Table))
                {
                    DataBase.CreateTable<T>();
                }
                else
                {
                    foreach (var item in propertys)
                    {
                        TypeCode code = Type.GetTypeCode(item.PropertyType.IsGenericType ? item.PropertyType.GetGenericArguments()[0] : item.PropertyType);

                        if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(item.Name))
                        {
                            PropertyDataInfo config = custAttribute.MetaData.PropertyDataConfig[item.Name];

                            if (!config.Ignore && !config.IsRelation)
                            {
                                string columnName = string.IsNullOrEmpty(config.ColumnName)
                                    ? config.PropertyName
                                    : config.ColumnName;
                                if (!DataBase.IsExistColumn(custAttribute.MetaData.Table, columnName))
                                {
                                    DataBase.AddColumn(custAttribute.MetaData.Table, columnName, Common.ConvertToDbType(code),
                                        config.StringLength);
                                }
                                else
                                {
                                    DataBase.AlterColumn(custAttribute.MetaData.Table, columnName,
                                        Common.ConvertToDbType(code),
                                        config.StringLength);
                                }
                            }
                        }
                        else
                        {
                            if (!DataBase.IsExistColumn(custAttribute.MetaData.Table, item.Name))
                            {
                                DataBase.AddColumn(custAttribute.MetaData.Table, item.Name, Common.ConvertToDbType(code));
                            }
                            else
                            {
                                DataBase.AlterColumn(custAttribute.MetaData.Table, item.Name, Common.ConvertToDbType(code));
                            }
                        }

                    }
                }
            }
            else
            {
                if (!DataBase.IsExistTable(TargeType.Name))
                {
                    DataBase.CreateTable<T>();
                }
                else
                {
                    foreach (var item in propertys)
                    {
                        TypeCode code = Type.GetTypeCode(item.PropertyType.IsGenericType ? item.PropertyType.GetGenericArguments()[0] : item.PropertyType);
                        if (!DataBase.IsExistColumn(TargeType.Name, item.Name))
                        {
                            DataBase.AddColumn(TargeType.Name, item.Name, Common.ConvertToDbType(code));
                        }
                        else
                        {
                            DataBase.AlterColumn(TargeType.Name, item.Name, Common.ConvertToDbType(code));
                        }
                    }
                }
            }
        }
    }
}
