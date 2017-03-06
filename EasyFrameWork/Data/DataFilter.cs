/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Constant;
using Easy.Extend;
using Easy.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Easy.Data
{

    public class DataFilter
    {
        void Init()
        {
            Conditions = new List<Condition>();
            ConditionGroups = new List<ConditionGroup>();
            Orders = new OrderCollection();
        }
        public DataFilter()
        {
            Init();
        }
        public DataFilter(List<string> updateProperties)
        {
            Init();
            this.UpdateProperties = updateProperties;
        }

        public DataFilter(string condition, List<KeyValuePair<string, object>> objParams)
        {
            Init();
            _condition = condition;
            _objParams = objParams;
        }

        private readonly string _condition;
        private readonly List<KeyValuePair<string, object>> _objParams;
        public List<string> UpdateProperties { get; set; }

        public List<ConditionGroup> ConditionGroups
        {
            get;
            set;
        }
        public List<Condition> Conditions
        {
            get;
            set;
        }
        public OrderCollection Orders
        {
            get;
            set;
        }

        #region 条件
        public DataFilter Where(Condition condition)
        {
            Conditions.Add(condition);
            return this;
        }
        public DataFilter Where(string property, OperatorType operatorType, object value)
        {
            return Where(new Condition(property, operatorType, value));
        }

        public DataFilter Where(string property, OperatorType operatorType, object value, ConditionType conditionType)
        {
            return Where(new Condition(property, operatorType, value, conditionType));
        }

        public DataFilter Where(ConditionGroup conditionGroup)
        {
            this.ConditionGroups.Add(conditionGroup);
            return this;
        }
        #endregion

        #region 排序
        public DataFilter OrderBy(Order item)
        {
            Orders.Append(item);
            return this;
        }

        public DataFilter OrderBy(string property, OrderType order)
        {
            Orders.Append(new Order(property, order));
            return this;
        }
        #endregion

        public override string ToString()
        {
            var builder = new StringBuilder();
            for (int i = 0; i < ConditionGroups.Count; i++)
            {
                if (i > 0)
                {
                    builder.Append(ConditionGroups[i].ToString(true));
                }
                else
                {
                    builder.Append(ConditionGroups[i]);
                }
            }
            for (int i = 0; i < Conditions.Count; i++)
            {
                if (i > 0 || builder.Length > 0)
                {
                    builder.Append(Conditions[i].ToString(true));
                }
                else
                {
                    builder.Append(Conditions[i]);
                }
            }
            if (_condition.IsNotNullAndWhiteSpace())
            {
                if (builder.Length > 0)
                {
                    builder.Append(" AND ");
                }

                builder.Append(_condition);
            }
            return builder.ToString();
        }
        public string GetOrderString()
        {
            var builder = new StringBuilder();
            foreach (var item in Orders)
            {
                if (builder.Length == 0)
                {
                    builder.Append(" ORDER BY ");
                    builder.Append(item);
                }
                else
                {
                    builder.AppendFormat(",{0} ", item);
                }
            }
            return builder.ToString();
        }
        public string GetContraryOrderString()
        {
            var builder = new StringBuilder();
            foreach (var item in Orders)
            {
                if (builder.Length == 0)
                {
                    builder.Append(" ORDER BY ");
                    builder.Append(item.ToString(true));
                }
                else
                {
                    builder.AppendFormat(",{0} ", item.ToString(true));
                }
            }
            return builder.ToString();
        }
        public List<KeyValuePair<string, object>> GetParameterValues()
        {
            var values = new List<KeyValuePair<string, object>>();
            ConditionGroups.ForEach(m => values.AddRange(m.GetKeyAndValue()));
            Conditions.Where(m => m.Value != null).Each(m =>
            {
                var keyvalue = m.GetKeyAndValue();
                keyvalue.Each(kv =>
                {
                    if (kv.Key.IsNotNullAndWhiteSpace())
                    {
                        values.Add(kv);
                    }
                });
            });
            if (_objParams != null)
            {
                _objParams.Each(m => values.Add(m));
            }
            return values;
        }
    }
}
