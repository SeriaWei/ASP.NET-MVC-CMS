/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using Easy.IOC;
using Easy.RepositoryPattern;

namespace Easy.Modules.DataDictionary
{
    public interface IDataDictionaryService : IService<DataDictionaryEntity>, IFreeDependency
    {
        /// <summary>
        /// 根据类别获取数据字典
        /// </summary>
        /// <param name="dicType"></param>
        /// <returns></returns>
        IEnumerable<DataDictionaryEntity> GetDictionaryByType(string dicType);
        /// <summary>
        /// 获取数据字典所有类别
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetDictionaryType();
        IEnumerable<DataDictionaryEntity> GetChildren(string dicType, long parentId);
    }
}
