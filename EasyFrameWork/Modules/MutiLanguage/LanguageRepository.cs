/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Data;

namespace Easy.Modules.MutiLanguage
{
    class LanguageRepository : RepositoryBase<LanguageEntity>
    {
        public IEnumerable<LanguageEntity> GetAllTypes()
        {
            return DataBase.CustomerSql("SELECT [Module] FROM [Language] group by [Module]").ToList<LanguageEntity>();
        }
        public Dictionary<string, string> InitLan(Dictionary<string, string> source)
        {
            if (source == null || source.Count == 0) return source;
            StringBuilder condition = new StringBuilder();
            foreach (var item in source)
            {
                condition.AppendFormat("'{0}',", item.Value);
            }
            System.Data.DataTable talbe = DataBase.CustomerSql(string.Format("SELECT [LanKey],[LanValue] FROM [Language] where [LanKey] in ({0})", condition.ToString().Trim(','))).ToDataTable();
            Dictionary<string, string> result = new Dictionary<string, string>();
            Dictionary<string, string> newLan = new Dictionary<string, string>();
            foreach (var item in source)
            {
                System.Data.DataRow[] ros = talbe.Select(string.Format("LanKey='{0}'", item.Value));
                if (ros.Length > 0)
                {
                    result.Add(item.Key, ros[0]["LanValue"].ToString());
                }
                else
                {
                    newLan.Add(item.Key, item.Value);
                    var lan = Get(new DataFilter().Where("LanKey", OperatorType.EndWith, "@" + item.Key)).FirstOrDefault();
                    if (lan != null)
                    {
                        result.Add(item.Key, lan.LanValue);
                    }
                }
            }
            foreach (var item in newLan)
            {
                var sql = DataBase.CustomerSql("INSERT INTO [Language] ([LanKey],[LanID],[LanValue],[Module],[LanType]) VALUES (@LanKey,@LanID,@LanValue,@Module,@LanType)")
                     .AddParameter("LanKey", item.Value)
                     .AddParameter("LanID", Localization.GetCurrentLanID())
                     .AddParameter("LanValue", result.ContainsKey(item.Key) ? result[item.Key] : item.Key);
                if (item.Value.Contains("@"))
                {
                    sql.AddParameter("Module", item.Value.Split('@')[0])
                    .AddParameter("LanType", "EntityProperty").ExecuteNonQuery();
                }
                else if (item.Value.Contains("|"))
                {
                    sql.AddParameter("Module", item.Value.Split('|')[0])
                    .AddParameter("LanType", "Enum").ExecuteNonQuery();
                }
            }

            return result;
        }
    }
}
