using System;
using System.Linq;
using KnightMoves.Hierarchical;
using KnightMoves.SqlObjects.SqlCode.Security;
using Newtonsoft.Json;

namespace KnightMoves.SqlObjects.SqlCode
{
    /// <summary>
    /// <para>
    /// This abstract class is the core base class of all SQL statement objects. It inherits from <see cref="KnightMoves.Hierarchical.TreeNode&lt;T&gt;"/> 
    /// so SqlStatement objects can be nested in a hierarchical structure.
    /// </para>
    /// <para>
    /// Classes that implement this abstract class must provide an implementation for the <see cref="SQL"/> method. 
    /// </para>
    /// </summary>
    public abstract partial class SqlStatement : TreeNode<string, SqlStatement>, ISqlStatement, ISqlCondition
    {
        /// <summary>
        /// A unique identifier for the SQL fragment. Useful for managing SQL creation through a user interface.
        /// </summary>
        public override string Id { get; set; }

        /// <summary>
        /// A name for the SQL fragment. Useful for managing SQL creation through a user interface.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A description for the SQL fragment. Useful for managing SQL creation through a user interface. 
        /// </summary>
        public string Description { get; set; }

        public bool IsDelete { get => this is ISqlDelete; }
        public bool IsFrom { get => this is ISqlFrom; }
        public bool IsGroupBy { get => this is ISqlGroupBy; }
        public bool IsInsert { get => this is ISqlInsert; }
        public bool IsJoin { get => this is ISqlJoin; }
        public bool IsRightJoin { get => this is ISqlRightJoin; }
        public bool IsLeftJoin { get => this is ISqlLeftJoin; }
        public bool IsInnerJoin { get => this is ISqlInnerJoin; }
        public bool IsScript { get => this is ISqlScript; }
        public bool IsSelect { get => this is ISqlSelect; }
        public bool IsUnion { get => this is ISqlUnion; }
        public bool IsUpdate { get => this is ISqlUpdate; }
        public bool IsWhereClause { get => this is ISqlWhereClause; }
        public bool IsBasicCondition { get => this is ISqlBasicCondition; }
        public bool IsBetween { get => this is ISqlBetweenCondition; }
        public bool IsCondition { get => this is ISqlCondition; }
        public bool IsConditionGroup { get => this is ISqlConditionGroup; }
        public bool IsInList { get => this is ISqlInListCondition; }
        public bool IsLike { get => this is ISqlLikeCondition; }
        public bool IsFunction { get => this is ISqlFunction; }
        public bool IsFunctionAbs { get => this is ISqlFunctionAbs; }
        public bool IsFunctionAvg { get => this is ISqlFunctionAvg; }
        public bool IsFunctionCeiling { get => this is ISqlFunctionCeiling; }
        public bool IsFunctionCount { get => this is ISqlFunctionCount; }
        public bool IsFunctionDateAdd { get => this is ISqlFunctionDateAdd; }
        public bool IsFunctionDateDiff { get => this is ISqlFunctionDateDiff; }
        public bool IsFunctionDateName { get => this is ISqlFunctionDateName; }
        public bool IsFunctionDatePart { get => this is ISqlFunctionDatePart; }
        public bool IsFunctionDay { get => this is ISqlFunctionDay; }
        public bool IsFunctionFloor { get => this is ISqlFunctionFloor; }
        public bool IsFunctionGetDate { get => this is ISqlFunctionGetDate; }
        public bool IsFunctionMax { get => this is ISqlFunctionMax; }
        public bool IsFunctionMin { get => this is ISqlFunctionMin; }
        public bool IsFunctionMonth { get => this is ISqlFunctionMonth; }
        public bool IsFunctionSum { get => this is ISqlFunctionSum; }
        public bool IsFunctionYear { get => this is ISqlFunctionYear; }
        public bool IsFunctionParameter { get => this is ISqlParameter; }
        public bool IsCalculation { get => this is ISqlCalculation; }
        public bool IsColumn { get => this is ISqlColumn; }
        public bool IsLiteral { get => this is ISqlLiteral; }
        public bool IsQueryExpression { get => this is ISqlQueryExpression; }
        public bool IsSubQuery { get => this is ISqlSubQuery; }
        public bool IsOrderBy { get => this is ISqlOrderBy; }
        public bool IsOrderByExpression { get => this is ISqlOrderByExpression; }
        public bool IsHaving { get => this is ISqlHaving; }
        public bool IsComment { get => this is ISqlComment; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SqlStatement()
        {
            Id = Guid.NewGuid().ToString().ToUpper();
        }

        /// <summary>
        /// Implementations of this method should return the valid fragment of SQL code that the concrete class represents.
        /// </summary>
        /// <returns>string - Fragment of valid of SQL Code</returns>
        public abstract string SQL();

        /// <summary>
        /// This override of ToString() simply returns the value from a call to SQL().
        /// </summary>
        /// <returns>The SQL statement. Returns the result of a call to <see cref="SQL"/></returns>
        public override string ToString() { return SQL(); }

        /// <summary>
        /// Returns the ToString() result of the <see cref="KnightMoves.SqlObjects.SqlCode.SqlLogicalOperators"/> enum value, 
        /// which is going to be either "AND" or "OR".
        /// </summary>
        /// <param name="op">A <see cref="KnightMoves.SqlObjects.SqlCode.SqlLogicalOperators"/> enum value representing the logical operator to be returned.</param>
        /// <returns>A ToString() result of the <see cref="KnightMoves.SqlObjects.SqlCode.SqlLogicalOperators"/> enum passed as the <paramref name="op"/> parameter.</returns>
        public string GetOperatorString(SqlLogicalOperators op) { return op.ToString(); }

        /// <summary>
        /// Converts the result of the <see cref="KnightMoves.SqlObjects.SqlCode.SqlComparisonOperators"/> enum value to it's SQL string equivalent.
        /// </summary>
        /// <param name="op">A <see cref="KnightMoves.SqlObjects.SqlCode.SqlComparisonOperators"/> enum value representing the comparison operator to be returned.</param>
        /// <returns>The <see cref="KnightMoves.SqlObjects.SqlCode.SqlComparisonOperators"/> enum passed as the <paramref name="op"/> parameter converted into it's SQL code equivalent</returns>
        public string GetOperatorString(SqlComparisonOperators op)
        {
            string opVal = "";

            switch (op)
            {
                case SqlComparisonOperators.IsEqualTo:
                    opVal = "=";
                    break;
                case SqlComparisonOperators.IsGreaterThan:
                    opVal = ">";
                    break;
                case SqlComparisonOperators.IsGreaterThanOrEqualTo:
                    opVal = ">=";
                    break;
                case SqlComparisonOperators.IsLessThan:
                    opVal = "<";
                    break;
                case SqlComparisonOperators.IsLessThanOrEqualTo:
                    opVal = "<=";
                    break;
                case SqlComparisonOperators.IsNotEqualTo:
                    opVal = "<>";
                    break;
                case SqlComparisonOperators.IS:
                    opVal = "IS";
                    break;
                case SqlComparisonOperators.ISNOT:
                    opVal = "IS NOT";
                    break;
            }

            return opVal;
        }

        /// <summary>
        /// Converts the result of the <see cref="KnightMoves.SqlObjects.SqlCode.SqlArithmeticOperators"/> enum value to 
        /// it's SQL string equivalent.
        /// </summary>
        /// <param name="op">A <see cref="KnightMoves.SqlObjects.SqlCode.SqlArithmeticOperators"/> enum value representing the arithmetic operator to be returned.</param>
        /// <returns>The <see cref="KnightMoves.SqlObjects.SqlCode.SqlArithmeticOperators"/> enum passed as the <paramref name="op"/> parameter converted into it's SQL code equivalent</returns>
        public string GetOperatorString(SqlArithmeticOperators op)
        {
            string opVal = "";

            switch (op)
            {
                case SqlArithmeticOperators.Divide:
                    opVal = "/";
                    break;
                case SqlArithmeticOperators.Minus:
                    opVal = "-";
                    break;
                case SqlArithmeticOperators.Modulo:
                    opVal = "%";
                    break;
                case SqlArithmeticOperators.Multiply:
                    opVal = "*";
                    break;
                case SqlArithmeticOperators.Plus:
                    opVal = "+";
                    break;
            }

            return opVal;
        }

        /// <summary>
        /// Builds the final SQL statement from all of the objects specified 
        /// through the fluent API
        /// </summary>
        public string Build()
        {
            var script = (Root ?? this) as SqlStatement;
            return script.SQL();
        }

        /// <summary>
        /// Builds the final SQL statement from all of the objects specified 
        /// through the fluent API using the <paramref name="parameters"/> object 
        /// to replace parameter specs in the form of @paramName found anywhere
        /// in the resulting SQL. Values that are supposed to be quoted are 
        /// quoted, values that are not supposed to be quoted are not quoted. 
        /// Care has been taken to block SQL Injection (test! your code!). This 
        /// can be used for debugging/simulating SQL token replacement by ORMs 
        /// such as Dapper.
        /// </summary>
        public string Build(object parameters)
        {
            var securityFilter = SQLSecurityFactory.Create();

            var sql = Build();

            var pType = parameters.GetType();

            if (!pType.IsClass)
                throw new ArgumentException($"The {nameof(parameters)} object cannot be an array, collection, enum or anything other than a single class.");

            foreach (var propInfo in pType.GetProperties())
            {
                var paramName = $"@{propInfo.Name}";

                var paramValue = propInfo.GetValue(parameters).ToString();

                var isQuoted = false;

                isQuoted |= propInfo.PropertyType == typeof(string);
                isQuoted |= propInfo.PropertyType == typeof(char);
                isQuoted |= propInfo.PropertyType == typeof(DateTime);
                isQuoted |= propInfo.PropertyType == typeof(Guid);
                isQuoted |= propInfo.PropertyType.IsEnum;

                if (propInfo.PropertyType == typeof(string))
                    paramValue = securityFilter.SanitizeInput<string>(paramValue);

                if (propInfo.PropertyType == typeof(DateTime))
                    paramValue = DateTime.Parse(paramValue).ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

                if (propInfo.PropertyType == typeof(bool))
                    paramValue = paramValue.ToLower() == "true" ? "1" : "0";

                paramValue = isQuoted ? $"'{paramValue}'" : paramValue;

                sql = sql.Replace(paramName, paramValue);
            }

            return sql;
        }

        public string ToJson()
        {
            var script = Root ?? this;

            script.MarkAsSerializable();

            var json = JsonConvert.SerializeObject(script);

            script.UnMarkAsSerializable();

            return json;
        }

        public static SqlStatement FromJson(string json) => JsonConvert.DeserializeObject<SqlStatement>(json);

        /*
         *************************************************************
         * ISqlCondition Implementation
         *************************************************************
        */

        public SqlLogicalOperators LogicalOperator { get; set; }

        [JsonConverter(typeof(TreeNodeJsonConverter))]
        public ISqlQueryExpression LeftOperand { get; set; }

        /*
         *************************************************************
         * MEMBERS TO SUPPORT FLUENT API IMPLEMENTATIONS
         *************************************************************
        */

        public string LeftOperandValue { get; set; }

        public string LeftMultiPartIdentifier { get; set; }

        public SqlComparisonOperators? ComparisonOperator { get; set; }

        public SqlArithmeticOperators? ArithmeticOperator { get; set; }

        public void ClearTempValues()
        {
            LeftOperand = null;
            LeftOperandValue = null;
            LeftMultiPartIdentifier = null;
            ComparisonOperator = null;
            ArithmeticOperator = null;
        }

        public virtual SqlStatement GetExpressionParent()
        {
            SqlStatement parent = null;

            ProcessAncestors(a =>
            {
                var s = a as SqlStatement;

                if (s.IsSelect || s.IsWhereClause || s.IsConditionGroup || s.IsInsert || s.IsGroupBy || s.IsOrderBy || s.IsHaving)
                    parent = s;

                return true;
            },
            this,
            a => parent != null);

            return parent;
        }

        public virtual SqlStatement GetParentScope()
        {
            SqlStatement parentScope = null;

            ProcessAncestors(a =>
            {
                var s = a as SqlStatement;

                if (s.IsScript || s.IsSubQuery || s.IsConditionGroup || s.IsUpdate || s.IsDelete)
                    parentScope = s;

                return true;
            },
            this,
            a => parentScope != null);

            return parentScope;
        }

        public virtual SqlStatement GetConditionParent()
        {
            if (IsWhereClause || IsJoin || ( IsBetween && (this as ISqlBetweenCondition).EndVal == null ) || IsConditionGroup || IsUpdate || IsHaving)
                return this;

            return null;
        }

        public SqlStatement WithId(string id)
        {
            var lastNode = GetLastAddedNode();

            lastNode.Id = id;

            return this;
        }

        public SqlStatement WithName(string name)
        {
            var lastNode = GetLastAddedNode();

            lastNode.Name = name;

            return this;
        }

        private SqlStatement GetLastAddedNode()
        {
            var lastAddedNode = Root as SqlStatement;

            if (!Root.Children.Any())
                return lastAddedNode;

            Root.ProcessChildren(c =>
            {
                lastAddedNode = c as SqlStatement;
                return true;
            });

            return lastAddedNode;
        }
    }
}
