using Easy.Constant;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Extend;

namespace Easy.Data
{

    /// <summary>
    /// 查询的条件
    /// </summary>
    public class Condition
    {
        readonly string _valueKey;
        private const string ConditionFormat = " {0} {1} @{2} ";
        private const string ConditionFormat2 = " [{0}] {1} @{2} ";
        public Condition()
        {
            this.ConditionType = ConditionType.And;
            this._valueKey = Guid.NewGuid().ToString("N");
        }

        public Condition(string property, OperatorType operatorType, object value)
        {
            this.Property = property;
            this.OperatorType = operatorType;
            this.Value = value;
            this.ConditionType = ConditionType.And;
            this._valueKey = Guid.NewGuid().ToString("N");
        }

        public Condition(string condition, ConditionType conditionTyep)
        {
            this.ConditionString = condition;
            this.ConditionType = conditionTyep;
        }

        public string ConditionString { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// 运算符
        /// </summary>
        public OperatorType OperatorType { get; set; }

        /// <summary>
        /// 条件类型
        /// </summary>
        public ConditionType ConditionType
        {
            get;
            set;
        }

        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            if (!string.IsNullOrEmpty(this.ConditionString))
            {
                if (this.ConditionString.Contains("{0}"))
                {
                    builder.AppendFormat(this.ConditionString, "@" + _valueKey);
                }
                else
                {
                    builder.Append(this.ConditionString);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(this.Property)) return string.Empty;
                string operatorStr = string.Empty;
                switch (this.OperatorType)
                {
                    case OperatorType.Equal:
                        {
                            operatorStr = "=";
                            break;
                        }
                    case OperatorType.GreaterThan:
                        {
                            operatorStr = ">";
                            break;
                        }
                    case OperatorType.GreaterThanOrEqualTo:
                        {
                            operatorStr = ">=";
                            break;
                        }
                    case OperatorType.LessThan:
                        {
                            operatorStr = "<";
                            break;
                        }
                    case OperatorType.LessThanOrEqualTo:
                        {
                            operatorStr = "<=";
                            break;
                        }
                    case OperatorType.NotEqual:
                        {
                            operatorStr = "<>";
                            break;
                        }
                    case OperatorType.StartWith:
                    case OperatorType.EndWith:
                    case OperatorType.Contains:
                        {
                            operatorStr = "like";
                            break;
                        }
                    case OperatorType.In:
                        {
                            var valuesBuilder = new StringBuilder();
                            if (this.Value is IEnumerable)
                            {
                                var valueEnum = this.Value as IEnumerable;
                                Type itemType = null;
                                foreach (var item in valueEnum)
                                {
                                    if (itemType == null)
                                        itemType = item.GetType();
                                    if (!itemType.IsClass)
                                    {
                                        valuesBuilder.AppendFormat("{0},", item);
                                    }
                                    else
                                    {
                                        valuesBuilder.AppendFormat("'{0}',", item.ToString().Replace("'", "''"));
                                    }
                                }
                            }
                            else
                            {
                                valuesBuilder.Append(this.Value);
                            }
                            if (this.Property.Contains("["))
                            {
                                builder.AppendFormat(" {0} in ({1}) ", this.Property, valuesBuilder.ToString().Trim(','));
                            }
                            else
                            {
                                builder.AppendFormat(" [{0}] in ({1}) ", this.Property, valuesBuilder.ToString().Trim(','));
                            }
                            break;
                        }
                    case OperatorType.NotIn:
                        {
                            var valuesBuilder = new StringBuilder();
                            if (this.Value is IEnumerable)
                            {
                                var valueEnum = this.Value as IEnumerable;
                                Type itemType = null;
                                foreach (var item in valueEnum)
                                {
                                    if (itemType == null)
                                        itemType = item.GetType();
                                    if (!itemType.IsClass)
                                    {
                                        valuesBuilder.AppendFormat("{0},", item);
                                    }
                                    else
                                    {
                                        valuesBuilder.AppendFormat("'{0}',", item.ToString().Replace("'", "''"));
                                    }
                                }
                            }
                            else
                            {
                                valuesBuilder.Append(this.Value);
                            }
                            if (this.Property.Contains("["))
                            {
                                builder.AppendFormat(" {0} not in ({1}) ", this.Property, valuesBuilder.ToString().Trim(','));
                            }
                            else
                            {
                                builder.AppendFormat(" [{0}] not in ({1}) ", this.Property, valuesBuilder.ToString().Trim(','));
                            }
                            break;
                        }
                    default:
                        {
                            operatorStr = "=";
                            break;
                        }
                }

                if (!operatorStr.IsNullOrEmpty())
                {
                    if (this.Property.Contains("["))
                    {
                        builder.AppendFormat(ConditionFormat, this.Property, operatorStr, _valueKey);
                    }
                    else
                    {
                        builder.AppendFormat(ConditionFormat2, this.Property, operatorStr, _valueKey);
                    }
                }

            }
            return builder.ToString();
        }
        public string ToString(bool withConditionType)
        {
            if (withConditionType)
            {
                switch (this.ConditionType)
                {
                    case ConditionType.And: return " AND " + ToString();
                    case ConditionType.Or: return " OR " + ToString();
                    default: return " AND " + ToString();
                }
            }
            else
            {
                return ToString();
            }
        }
        public KeyValuePair<string, object> GetKeyAndValue()
        {
            switch (this.OperatorType)
            {
                case OperatorType.StartWith: return new KeyValuePair<string, object>(_valueKey, Value + "%");
                case OperatorType.EndWith: return new KeyValuePair<string, object>(_valueKey, "%" + Value + "%");
                case OperatorType.Contains: return new KeyValuePair<string, object>(_valueKey, "%" + Value + "%");
                case OperatorType.In:
                case OperatorType.NotIn: return new KeyValuePair<string, object>();
                default: return new KeyValuePair<string, object>(_valueKey, Value);
            }

        }
    }

    public class ConditionGroup
    {
        public ConditionGroup()
        {
            this.Conditions = new List<Condition>();
        }

        public List<Condition> Conditions
        {
            get;
            set;
        }
        /// <summary>
        /// 条件类型
        /// </summary>
        public ConditionType ConditionType
        {
            get;
            set;
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            int i = 0;
            if (Conditions.Count > 0)
            {
                builder.Append("(");
                foreach (var item in Conditions)
                {
                    if (i == 0) builder.Append(item);
                    else builder.Append(item.ToString(true));
                    i++;
                }
                builder.Append(")");
            }
            return builder.ToString();
        }
        public string ToString(bool withConditionType)
        {
            if (withConditionType)
            {
                switch (this.ConditionType)
                {
                    case ConditionType.And: return " AND " + ToString();
                    case ConditionType.Or: return " OR " + ToString();
                    default: return " AND " + ToString();
                }
            }
            return ToString();

        }
        public List<KeyValuePair<string, object>> GetKeyAndValue()
        {
            var list = new List<KeyValuePair<string, object>>();
            Conditions.Each(item =>
            {
                if (item.Value != null)
                {
                    list.Add(item.GetKeyAndValue());
                }
            });
            return list;
        }
        public void Add(Condition condition)
        {
            this.Conditions.Add(condition);
        }
    }

    public class Order
    {
        public Order()
        {

        }
        public Order(string property, OrderType order)
        {
            this.Property = property;
            this.OrderType = order;
        }
        public string Property { get; set; }
        public OrderType OrderType { get; set; }
        public override string ToString()
        {
            if (!Property.Contains("["))
            {
                Property = string.Format("[{0}]", Property);
            }
            switch (OrderType)
            {
                case OrderType.Ascending: return string.Format(" {0} Asc", Property);
                case OrderType.Descending: return string.Format(" {0} Desc", Property);
                default: return string.Format(" {0} Asc", Property);
            }
        }
        public string ToString(bool contrary)
        {
            if (contrary)
            {
                if (!Property.Contains("["))
                {
                    Property = string.Format("[{0}]", Property);
                }
                switch (OrderType)
                {
                    case OrderType.Descending: return string.Format(" {0} Asc", Property);
                    case OrderType.Ascending: return string.Format(" {0} Desc", Property);
                    default: return string.Format(" {0} Asc", Property);
                }
            }
            return this.ToString();

        }
    }
}
