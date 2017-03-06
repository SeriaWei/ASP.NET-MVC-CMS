/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Easy.Data;
using Easy.MetaData;

namespace Easy.Reflection
{
    public static class LinqExpression
    {
        public static string GetPropertyName(Expression expression)
        {
            if (expression.NodeType == ExpressionType.Convert ||
                expression.NodeType == ExpressionType.ConvertChecked)
            {
                var exp = (((UnaryExpression)expression).Operand as MemberExpression);
                if (exp != null)
                {
                    return exp.Member.Name;
                }
            }
            var memberExp = expression as MemberExpression;
            if (memberExp != null)
            {
                return memberExp.Member.Name;
            }
            return "";
        }
        public static void CopyTo(object from, object to, IEnumerable<ParameterExpression> paras, BinaryExpression expression)
        {
            var toType = to.GetType();
            Expression left;
            Expression right;
            if (expression.Left.NodeType == ExpressionType.Convert ||
               expression.Left.NodeType == ExpressionType.ConvertChecked)
            {
                left = ((UnaryExpression)expression.Left).Operand;
            }
            else
            {
                left = expression.Left;
            }
            if (expression.Right.NodeType == ExpressionType.Convert ||
                expression.Right.NodeType == ExpressionType.ConvertChecked)
            {
                right = ((UnaryExpression)expression.Right).Operand;
            }
            else
            {
                right = expression.Right;
            }
            if (expression.NodeType == ExpressionType.Equal)
            {
                Func<Expression, string> propertyHandle = exp =>
                {
                    if (exp is MemberExpression)
                    {
                        return (exp as MemberExpression).Member.Name;
                    }
                    return null;
                };
                Func<Expression, object> valueHandle = exp =>
                {
                    if (exp.NodeType == ExpressionType.Constant)
                    {
                        return ((ConstantExpression)exp).Value;
                    }
                    if (exp.NodeType == ExpressionType.Call || exp.NodeType == ExpressionType.MemberAccess)
                    {
                        var paraType = paras.FirstOrDefault(m => m.Type.IsAssignableFrom(GetExpressParamaterType(exp)));
                        if (paraType != null)
                        {
                            return Expression.Lambda(exp, paraType).Compile().DynamicInvoke(from);
                        }
                        else
                        {
                            return Expression.Lambda(exp).Compile().DynamicInvoke();
                        }
                    }
                    return null;
                };
                var leftIsTarget = !toType.IsAssignableFrom(GetExpressParamaterType(right));

                string propertyName = leftIsTarget ? propertyHandle(left) : propertyHandle(right);
                if (propertyName != null)
                {
                    object value = leftIsTarget ? valueHandle(right) : valueHandle(left);
                    if (value != null)
                    {
                        toType.GetProperty(propertyName).SetValue(to, value, null);
                    }
                }
            }
            if (expression.Left is BinaryExpression)
            {
                CopyTo(from, to, paras, (BinaryExpression)expression.Left);
            }
            if (expression.Right is BinaryExpression)
            {
                CopyTo(from, to, paras, (BinaryExpression)expression.Right);
            }
        }
        private static Type GetExpressParamaterType(Expression exp)
        {
            if (exp.NodeType == ExpressionType.Call)
            {
                var call = exp as MethodCallExpression;
                return GetExpressParamaterType(call.Object);
            }
            else if (exp.NodeType == ExpressionType.MemberAccess)
            {
                var member = exp as MemberExpression;
                if (member.Expression == null)
                {
                    return member.Type;
                }
                return member.Expression.Type;
            }
            return exp.Type;
        }
        public static DataFilter ConvertToDataFilter(IEnumerable<ParameterExpression> paras, BinaryExpression expression, object obj = null)
        {
            List<KeyValuePair<string, object>> objParams = new List<KeyValuePair<string, object>>();
            string condition = BinaryConvert(paras, expression, obj, ref objParams);
            return new DataFilter(condition, objParams);
        }
        private static string BinaryConvert(IEnumerable<ParameterExpression> paras, BinaryExpression expression, object obj, ref List<KeyValuePair<string, object>> objParams)
        {
            string operatorStr;
            switch (expression.NodeType)
            {
                case ExpressionType.AndAlso:
                    operatorStr = " AND ";
                    break;
                case ExpressionType.OrElse:
                    operatorStr = " OR ";
                    break;
                case ExpressionType.Equal:
                    operatorStr = " = ";
                    break;
                case ExpressionType.NotEqual:
                    operatorStr = " != ";
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    operatorStr = " >= ";
                    break;
                case ExpressionType.GreaterThan:
                    operatorStr = " > ";
                    break;
                case ExpressionType.LessThanOrEqual:
                    operatorStr = " <= ";
                    break;
                case ExpressionType.LessThan:
                    operatorStr = " < ";
                    break;
                default:
                    throw new ArgumentException("Invalid lambda expression");
            }
            Func<Expression, List<KeyValuePair<string, object>>, string> processExpression = (expre, parmas) =>
            {
                string str;
                Expression exp;
                if (expre.NodeType == ExpressionType.Convert || expre.NodeType == ExpressionType.ConvertChecked)
                {
                    exp = ((UnaryExpression)expre).Operand;
                }
                else
                {
                    exp = expre;
                }
                if (exp is BinaryExpression)
                {
                    str = BinaryConvert(paras, (BinaryExpression)exp, obj, ref parmas);
                }
                else if (exp.NodeType == ExpressionType.MemberAccess)
                {
                    var result = MemberConvert((MemberExpression)exp, obj);
                    str = result.Key;
                    if (result.Value != null)
                    {
                        str = "@" + str;
                        parmas.Add(result);
                    }
                }
                else if (exp.NodeType == ExpressionType.Constant)
                {
                    var result = ConstantConvert((ConstantExpression)exp);
                    str = result.Key;
                    if (result.Value != null)
                    {
                        str = "@" + str;
                        parmas.Add(result);
                    }
                }
                else if (exp.NodeType == ExpressionType.Call)
                {
                    var paraType = paras.FirstOrDefault(m => m.Type.IsAssignableFrom(GetExpressParamaterType(exp)));
                    object value;
                    if (paraType != null)
                    {
                        value = Expression.Lambda(exp, paraType).Compile().DynamicInvoke(obj);
                    }
                    else
                    {
                        value = Expression.Lambda(exp).Compile().DynamicInvoke();
                    }
                    KeyValuePair<string, object> keyValue = new KeyValuePair<string, object>(Guid.NewGuid().ToString("N"), value);
                    str = "@" + keyValue.Key;
                    parmas.Add(keyValue);
                }
                else
                {
                    throw new ArgumentException("Invalid lambda expression");
                }
                return str;
            };

            string leftText = processExpression(expression.Left, objParams);

            string rightText = processExpression(expression.Right, objParams);

            return string.Format("({0} {1} {2})", leftText, operatorStr, rightText);
        }
        private static KeyValuePair<string, object> MemberConvert(MemberExpression expression, object obj)
        {
            Func<MemberExpression, object> valueGetter = exp =>
            {
                var objectMember = Expression.Convert(exp, typeof(object));
                var getterLambda = Expression.Lambda<Func<object>>(objectMember);
                return getterLambda.Compile()();
            };
            switch (expression.Member.MemberType)
            {
                case MemberTypes.Field:
                    {
                        return new KeyValuePair<string, object>(Guid.NewGuid().ToString("N"), valueGetter(expression));
                    }
                case MemberTypes.Property:
                    {
                        if (expression.Expression.NodeType == ExpressionType.MemberAccess)
                        {
                            return new KeyValuePair<string, object>(Guid.NewGuid().ToString("N"), valueGetter(expression));
                        }
                        if (obj != null)
                        {
                            var objType = obj.GetType();
                            if (expression.Member.ReflectedType != null && expression.Member.ReflectedType.IsAssignableFrom(objType))
                            {
                                var property = objType.GetProperty(expression.Member.Name);
                                if (property != null)
                                {
                                    var value = property.GetValue(obj, null);
                                    if (value != null)
                                    {
                                        return new KeyValuePair<string, object>(Guid.NewGuid().ToString("N"), value);
                                    }
                                }
                            }

                        }
                        DataConfigureAttribute attribute = DataConfigureAttribute.GetAttribute(expression.Member.ReflectedType);
                        string column = attribute != null ? attribute.GetPropertyMapper(expression.Member.Name) : expression.Member.Name;
                        return new KeyValuePair<string, object>(column, null);
                    }
            }
            return new KeyValuePair<string, object>();
        }
        private static KeyValuePair<string, object> ConstantConvert(ConstantExpression expression)
        {
            return new KeyValuePair<string, object>(Guid.NewGuid().ToString("N"), expression.Value);
        }
    }
}
