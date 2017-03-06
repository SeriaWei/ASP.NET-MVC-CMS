/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Easy.Extend;
using Easy.MetaData;

namespace Easy.Data.DataBase
{
    public class CustomerSqlHelper
    {
        private string _command;
        private readonly List<KeyValuePair<string, object>> _keyValue;
        private readonly DataBasic _dataBase;
        private readonly CommandType _commandType;
        public CustomerSqlHelper(string command, CommandType commandType, DataBasic db)
        {
            if (commandType == CommandType.StoredProcedure && !command.Contains('['))
            {
                this._command = string.Format("[{0}]", command);
            }
            else this._command = command;
            _keyValue = new List<KeyValuePair<string, object>>();
            this._commandType = commandType;
            this._dataBase = db;
        }
        private List<KeyValuePair<string, string>> GetMap<T>()
        {
            DataConfigureAttribute attribute = DataConfigureAttribute.GetAttribute<T>();
            var map = new List<KeyValuePair<string, string>>();
            if (attribute != null && attribute.MetaData != null && attribute.MetaData.PropertyDataConfig != null)
            {
                attribute.MetaData.PropertyDataConfig.Each(m => map.Add(new KeyValuePair<string, string>(m.Value.ColumnName, m.Key)));
            }
            else
            {
                var proertyInfoArray = typeof(T).GetProperties();
                proertyInfoArray.Where(m => m.CanWrite).Each(m => map.Add(new KeyValuePair<string, string>(m.Name, m.Name)));
            }
            return map;
        }
        public CustomerSqlHelper AddParameter(string name, object value)
        {
            _keyValue.Add(new KeyValuePair<string, object>(name, value));
            return this;
        }
        public int ExecuteNonQuery()
        {
            return _dataBase.ExecuteNonQuery(_command, _commandType, _keyValue);
        }
        public T To<T>()
        {
            if (_commandType == CommandType.StoredProcedure)
            {
                _command = "EXEC " + _command;
            }
            DataTable table = _dataBase.GetData(_command, _keyValue);
            var map = GetMap<T>();
            if (map.Count == 0)
            {
                return Reflection.ClassAction.GetModel<T>(table, 0);
            }
            else
            {
                return Reflection.ClassAction.GetModel<T>(table, 0, map);
            }
        }
        public List<T> ToList<T>()
        {
            if (_commandType == CommandType.StoredProcedure)
            {
                _command = "EXEC " + _command;
            }
            DataTable table = _dataBase.GetData(_command, _keyValue);
            var lists = new List<T>();
            var map = GetMap<T>();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (map.Any())
                {
                    lists.Add(Reflection.ClassAction.GetModel<T>(table, i, map));
                }
                else
                {
                    lists.Add(Reflection.ClassAction.GetModel<T>(table, i));
                }

            }
            return lists;
        }
        public DataTable ToDataTable()
        {
            if (_commandType == CommandType.StoredProcedure)
            {
                _command = "EXEC " + _command;
            }
            return _dataBase.GetData(_command, _keyValue);
        }

    }
}
