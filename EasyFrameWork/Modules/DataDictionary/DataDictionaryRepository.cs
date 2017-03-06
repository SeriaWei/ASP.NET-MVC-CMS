/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Modules.DataDictionary
{
    class DataDictionaryRepository : RepositoryBase<DataDictionaryEntity>
    {
        public List<string> GetDictionaryType()
        {
            return DataBase.CustomerSql("select DicName from DataDictionary group by DicName order by DicName").ToList<string>();
        }
    }
}
