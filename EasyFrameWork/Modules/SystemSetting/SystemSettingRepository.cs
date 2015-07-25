using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Modules.SystemSetting
{
    class SystemSettingRepository : RepositoryBase<SystemSettingBase>
    {
        public SystemSettingBase Get()
        {
            DataTable table = DB.CustomerSql("select Property,Val from SystemSetting").ToDataTable();
            SystemSettingBase setting = ServiceLocator.Current.GetInstance<SystemSettingBase>();
            Type setType = setting.GetType();
            foreach (DataRow item in table.Rows)
            {
                string property = item["Property"].ToString();
                string value = item["Val"].ToString();
                PropertyInfo propertyInfo = setType.GetProperty(property);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(setting, Easy.Reflection.ClassAction.ValueConvert(propertyInfo, value), null);
                }
            }
            return setting;
        }
        public void Update(SystemSettingBase setting)
        {
            Type setType = setting.GetType();
            PropertyInfo[] propertys = setType.GetProperties();
            DB.CustomerSql("Delete From SystemSetting").ExecuteNonQuery();
            foreach (PropertyInfo item in propertys)
            {
                object value = item.GetValue(setting, null);
                if (value == null) value = string.Empty;
                DB.CustomerSql("Insert into SystemSetting(Property,Val) values (@Property,@Val)")
                    .AddParameter("Property", item.Name)
                    .AddParameter("Val", value.ToString())
                    .ExecuteNonQuery();
            }
        }
    }
}
