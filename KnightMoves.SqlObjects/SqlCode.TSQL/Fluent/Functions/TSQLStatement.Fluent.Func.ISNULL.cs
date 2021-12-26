using System;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
    public abstract partial class TSQLStatement : SqlStatement
    {
        private string _checkExpressionParamName = nameof(TSQLFuncIsNull.TSQLFuncIsNullParams.checkExpression);
        private string _replacementValueParamName = nameof(TSQLFuncIsNull.TSQLFuncIsNullParams.replacementValue);

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a column for the <paramref name="checkExpression"/> 
        /// and a literal <see cref="string"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string multipartIdentifier, string column, string replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.VarChar, multipartIdentifier, column, replaceExpression);
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a column for the <paramref name="checkExpression"/> 
        /// and an <see cref="int"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string multipartIdentifier, string column, int replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Int, multipartIdentifier, column, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a column for the <paramref name="checkExpression"/> 
        /// and a <see cref="DateTime"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string multipartIdentifier, string column, DateTime replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.DateTime, multipartIdentifier, column, replaceExpression.ToString(SqlDataType.SQL_DATE_STRING_FORMAT));
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a column for the <paramref name="checkExpression"/> 
        /// and a <see cref="bool"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string multipartIdentifier, string column, bool replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Bit, multipartIdentifier, column, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a column for the <paramref name="checkExpression"/> 
        /// and a <see cref="long"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string multipartIdentifier, string column, long replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.BigInt, multipartIdentifier, column, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a column for the <paramref name="checkExpression"/> 
        /// and a <see cref="Guid"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string multipartIdentifier, string column, Guid replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.UniqueIdentifier, multipartIdentifier, column, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a column for the <paramref name="checkExpression"/> 
        /// and a <see cref="char"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string multipartIdentifier, string column, char replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Char, multipartIdentifier, column, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a column for the <paramref name="checkExpression"/> 
        /// and a <see cref="decimal"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string multipartIdentifier, string column, decimal replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Decimal, multipartIdentifier, column, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a literal <see cref="string"/> for the <paramref name="checkExpression"/> 
        /// and a literal <see cref="string"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string checkExpression, string replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.VarChar, string.Empty, checkExpression, replaceExpression);
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a literal <see cref="string"/> for the <paramref name="checkExpression"/> 
        /// and an <see cref="int"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string checkExpression, int replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Int, string.Empty, checkExpression, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a literal <see cref="string"/> for the <paramref name="checkExpression"/> 
        /// and a <see cref="DateTime"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string checkExpression, DateTime replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.DateTime, string.Empty, checkExpression, replaceExpression.ToString(SqlDataType.SQL_DATE_STRING_FORMAT));
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a literal <see cref="string"/> for the <paramref name="checkExpression"/> 
        /// and a <see cref="bool"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string checkExpression, bool replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Bit, string.Empty, checkExpression, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a literal <see cref="string"/> for the <paramref name="checkExpression"/> 
        /// and a <see cref="long"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string checkExpression, long replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.BigInt, string.Empty, checkExpression, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a literal <see cref="string"/> for the <paramref name="checkExpression"/> 
        /// and a <see cref="Guid"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string checkExpression, Guid replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.UniqueIdentifier, string.Empty, checkExpression, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a literal <see cref="string"/> for the <paramref name="checkExpression"/> 
        /// and a <see cref="char"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string checkExpression, char replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Char, string.Empty, checkExpression, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using a literal <see cref="string"/> for the <paramref name="checkExpression"/> 
        /// and a <see cref="decimal"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(string checkExpression, decimal replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Decimal, string.Empty, checkExpression, replaceExpression.ToString());
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using an <see cref="ISqlQueryExpression"/> object for the 
        /// <paramref name="checkExpression"/> and a literal <see cref="string"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(ISqlQueryExpression checkExpression, string replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.VarChar, string.Empty, string.Empty, replaceExpression, checkExpression);
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using an <see cref="ISqlQueryExpression"/> object for the 
        /// <paramref name="checkExpression"/> and an <see cref="int"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(ISqlQueryExpression checkExpression, int replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Int, string.Empty, string.Empty, replaceExpression.ToString(), checkExpression);
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using an <see cref="ISqlQueryExpression"/> object for the 
        /// <paramref name="checkExpression"/> and a <see cref="DateTime"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(ISqlQueryExpression checkExpression, DateTime replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.DateTime, string.Empty, string.Empty, replaceExpression.ToString(SqlDataType.SQL_DATE_STRING_FORMAT), checkExpression);
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using an <see cref="ISqlQueryExpression"/> object for the 
        /// <paramref name="checkExpression"/> and a <see cref="bool"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(ISqlQueryExpression checkExpression, bool replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Bit, string.Empty, string.Empty, replaceExpression.ToString(), checkExpression);
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using an <see cref="ISqlQueryExpression"/> object for the 
        /// <paramref name="checkExpression"/> and a <see cref="long"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(ISqlQueryExpression checkExpression, long replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.BigInt, string.Empty, string.Empty, replaceExpression.ToString(), checkExpression);
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using an <see cref="ISqlQueryExpression"/> object for the 
        /// <paramref name="checkExpression"/> and a <see cref="Guid"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(ISqlQueryExpression checkExpression, Guid replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.UniqueIdentifier, string.Empty, string.Empty, replaceExpression.ToString(), checkExpression);
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using an <see cref="ISqlQueryExpression"/> object for the 
        /// <paramref name="checkExpression"/> and a <see cref="char"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(ISqlQueryExpression checkExpression, char replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Char, string.Empty, string.Empty, replaceExpression.ToString(), checkExpression);
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using an <see cref="ISqlQueryExpression"/> object for the 
        /// <paramref name="checkExpression"/> and a <see cref="decimal"/> for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(ISqlQueryExpression checkExpression, decimal replaceExpression)
        {
            return MakeIsNullFunction(SqlDbType.Decimal, string.Empty, string.Empty, replaceExpression.ToString(), checkExpression);
        }

        /// <summary>
        /// Outputs ISNULL(checkExpression,replacementValue) using an <see cref="ISqlQueryExpression"/> object for the 
        /// <paramref name="checkExpression"/> and an <see cref="ISqlQueryExpression"/> object for the <paramref name="replaceExpression"/>
        /// </summary>
        public override SqlStatement ISNULL(ISqlQueryExpression checkExpression, ISqlQueryExpression replaceExpression)
        {
            var func = new TSQLFuncIsNull(Enum.Parse<SqlDbType>(checkExpression.DataType.DataType.ToString()));

            func.SetParameterValue(_checkExpressionParamName, checkExpression);
            func.SetParameterValue(_replacementValueParamName, replaceExpression);

            var parent = GetExpressionParent();

            parent.Children.Add(func);

            return func;
        }

        private SqlStatement MakeIsNullFunction(SqlDbType dataType, string multipartIdentifier, string checkExpression, string replaceExpression, ISqlQueryExpression queryExpression = null)
        {
            ISqlQueryExpression checkExpressionParameter;

            var func = new TSQLFuncIsNull(dataType);

            if (queryExpression != null)
                checkExpressionParameter = queryExpression;
            else if (checkExpression.IsParameter() || checkExpression.IsDelimited())
                checkExpressionParameter = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = checkExpression };
            else
                checkExpressionParameter = new TSQLColumn { DataType = new TSQLDataType(dataType), MultiPartIdentifier = multipartIdentifier, ColumnName = checkExpression };

            func.SetParameterValue(_checkExpressionParamName, checkExpressionParameter);
            func.SetParameterValue(_replacementValueParamName, new TSQLLiteral { DataType = new TSQLDataType(dataType), Value = replaceExpression });

            var parent = GetExpressionParent();

            parent.Children.Add(func);

            return func;
        }
    }
}
