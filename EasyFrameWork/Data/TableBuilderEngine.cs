using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Data.DataBase;
using Easy.MetaData;

namespace Easy.Data
{
    public class TableBuilderEngine
    {
        public TableBuilderEngine()
        {
            string dataBase = System.Configuration.ConfigurationManager.AppSettings[DataBasic.DataBaseAppSetingKey];
            var con = System.Configuration.ConfigurationManager.ConnectionStrings[DataBasic.ConnectionKey];
            string connString = string.Empty;
            if (con != null)
            {
                connString = con.ConnectionString;
            }
            else
            {
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString;
            }
            if (dataBase == DataBasic.Ace)
            {
                DataBase = new Access(connString);
                (DataBase as Access).DbType = Access.DdTypes.Ace;
            }
            else if (dataBase == DataBasic.Jet)
            {
                DataBase = new Access(connString);
                (DataBase as Access).DbType = Access.DdTypes.JET;
            }
            else if (dataBase == DataBasic.SQL)
            {
                DataBase = new SQL(connString);
            }
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
                        TypeCode code;
                        if (item.PropertyType.Name == "Nullable`1")
                        {
                            code = Type.GetTypeCode(item.PropertyType.GetGenericArguments()[0]);
                        }
                        else
                        {
                            code = Type.GetTypeCode(item.PropertyType);
                        }

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
                        TypeCode code;
                        if (item.PropertyType.Name == "Nullable`1")
                        {
                            code = Type.GetTypeCode(item.PropertyType.GetGenericArguments()[0]);
                        }
                        else
                        {
                            code = Type.GetTypeCode(item.PropertyType);
                        }
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
