/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Data
{
    public class Relation
    {
        /// <summary>
        /// 关联表
        /// </summary>
        public string RelationTable { get; set; }
        /// <summary>
        /// 关联关系
        /// </summary>
        public RelationType RelationType { get; set; }
        /// <summary>
        /// 表别名
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 关联条件
        /// </summary>
        public string Conditions { get; set; }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(" ");
            switch (RelationType)
            {
                case RelationType.InnerJoin:
                    builder.Append("INNER JOIN");
                    break;
                case RelationType.LeftJoin:
                    builder.Append("LEFT JOIN");
                    break;
                case RelationType.RightJoin:
                    builder.Append("RIGHT JOIN");
                    break;
                case RelationType.LeftOuterJoin:
                    builder.Append("LEFT OUTER JOIN");
                    break;
                case RelationType.RightOuterJoin:
                    builder.Append("RIGHT OUTER JOIN");
                    break;
                case RelationType.FullJoin:
                    builder.Append("FULL JOIN");
                    break;
                case RelationType.FullOuterJoin:
                    builder.Append("FULL OUTER JOIN");
                    break;
                default:
                    builder.Append("LEFT JOIN");
                    break;
            }
            builder.Append(" ");
            builder.Append(RelationTable);
            builder.Append(" ");
            builder.Append(Alias);
            builder.Append(" ON ");
            builder.Append(Conditions);
            builder.Append(" ");
            return builder.ToString();
        }
    }

    public class RelationHelper
    {
       private readonly List<Relation> _relations;
        public RelationHelper(List<Relation> relation)
        {
            this._relations = relation;
        }
        public RelationHelper InnerJoin(string table, string alias, string condition)
        {
            _relations.Add(new Relation
            {
                RelationTable = table,
                RelationType = RelationType.InnerJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }

        public RelationHelper LeftJoin(string table, string alias, string condition)
        {
            _relations.Add(new Relation
            {
                RelationTable = table,
                RelationType = RelationType.LeftJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
        public RelationHelper RightJoin(string table, string alias, string condition)
        {
            _relations.Add(new Relation
            {
                RelationTable = table,
                RelationType = RelationType.RightJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
        public RelationHelper LeftOuterJoin(string table, string alias, string condition)
        {
            _relations.Add(new Relation
            {
                RelationTable = table,
                RelationType = RelationType.LeftOuterJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
        public RelationHelper RightOuterJoin(string table, string alias, string condition)
        {
            _relations.Add(new Relation
            {
                RelationTable = table,
                RelationType = RelationType.RightOuterJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
        public RelationHelper FullJoin(string table, string alias, string condition)
        {
            _relations.Add(new Relation
            {
                RelationTable = table,
                RelationType = RelationType.FullJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
        public RelationHelper FullOuterJoin(string table, string alias, string condition)
        {
            _relations.Add(new Relation
            {
                RelationTable = table,
                RelationType = RelationType.FullOuterJoin,
                Conditions = condition,
                Alias = alias
            });
            return this;
        }
    }
}
