/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Data
{
    /// <summary>
    /// 运算符
    /// </summary>
    public enum OperatorType
    {
        Equal = 1,
        GreaterThan = 2,
        GreaterThanOrEqualTo = 3,
        LessThan = 4,
        LessThanOrEqualTo = 5,
        NotEqual = 6,
        Contains = 7,
        StartWith = 8,
        EndWith = 9,
        In = 10,
        NotIn = 11
    }

    /// <summary>
    /// 条件类型
    /// </summary>
    public enum ConditionType
    {
        And = 1,
        Or = 2
    }
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum OrderType
    {
        Ascending = 1,
        Descending = 2
    }
    /// <summary>
    /// 连接类型
    /// </summary>
    public enum RelationType
    {
        InnerJoin = 1,
        LeftJoin = 2,
        RightJoin = 3,
        LeftOuterJoin = 4,
        RightOuterJoin = 5,
        FullJoin = 6,
        FullOuterJoin = 7
    }
}
