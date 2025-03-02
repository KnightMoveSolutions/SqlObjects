﻿/*
    THE LICENSED WORK IS PROVIDED UNDER THE TERMS OF THE DEVELOPER TOOL LIMITED 
    LICENSE (“LICENSE”) AS FIRST COMPLETED BY THE ORIGINAL AUTHOR. ANY USE, PUBLIC 
    DISPLAY, PUBLIC PERFORMANCE, REPRODUCTION OR DISTRIBUTION OF, OR PREPARATION OF 
    DERIVATIVE WORKS BASED ON THE LICENSED WORK CONSTITUTES RECIPIENT’S ACCEPTANCE 
    OF THIS LICENSE AND ITS TERMS, WHETHER OR NOT SUCH RECIPIENT READS THE TERMS OF THE 
    LICENSE. “LICENSED WORK” AND “RECIPIENT” ARE DEFINED IN THE LICENSE. A COPY OF THE 
    LICENSE IS LOCATED IN THE TEXT FILE ENTITLED “LICENSE.TXT” ACCOMPANYING THE CONTENTS 
    OF THIS FILE. IF A COPY OF THE LICENSE DOES NOT ACCOMPANY THIS FILE, A COPY OF THE 
    LICENSE MAY ALSO BE OBTAINED AT THE FOLLOWING WEB SITE:  
 
    https://docs.knightmovesolutions.com/sql-objects/license.html
*/

using System;
using System.Data;
using System.Linq;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

// Common method implementations for Fluent API
public abstract partial class TSQLStatement : SqlStatement
{
    // Find the nearest ancestor that takes a condition if it's not "this"
    public override SqlStatement GetConditionParent()
    {
        var parent = base.GetConditionParent();

        if (parent != null)
            return parent;

        if (this is ITSQLCaseExpression)
            return this;

        ProcessAncestors(s =>
        {
            if (s.IsWhereClause || s.IsInnerJoin || ( s.IsBetween && (s as ISqlBetweenCondition).EndVal == null ) || s.IsConditionGroup || s.IsHaving || s is ITSQLCaseExpression)
                parent = s;
        },
        this,
        s => parent != null);

        return parent;
    }

    private SqlStatement MakeBasicCondition(string leftMultipartIdentifier, string leftValue, SqlDbType? leftDbType, SqlComparisonOperators op, string rightMultiParIdentifier, string rightValue, SqlDbType? rightDbType)
    {
        if (string.IsNullOrEmpty(rightMultiParIdentifier) && this is ITSQLCase && (this as ITSQLCase).DataType != new TSQLDataType(rightDbType.Value))
            throw new InvalidOperationException($"Cannot compare value of {rightDbType} data type to a CASE expression with {((this as ITSQLCase).DataType.DataType)} data type");

        ISqlQueryExpression leftOperand = LeftOperand;

        if (leftOperand == null && (IsWhereClause || IsHaving) && Children.Any())
        {
            var condition = Children.LastOrDefault(c => c is ISqlQueryExpression);
            leftOperand = condition == null ? null : condition as ISqlQueryExpression;
            Children.Remove(condition);
        }

        if (this is ITSQLCase || IsColumn || IsCalculation || IsFunction || IsSubQuery)
        {
            // We remove the Parent reference because it is now wrapped in a condition not as a child
            Parent.Children.Remove(this);

            if (!IsSubQuery)
                Parent = null;

            leftOperand = this as ISqlQueryExpression;
        }

        if (leftOperand == null)
            leftOperand = new TSQLColumn { MultiPartIdentifier = leftMultipartIdentifier, ColumnName = leftValue };

        ISqlQueryExpression rightOperand;

        if (rightDbType.HasValue)
        {
            rightOperand = new TSQLLiteral { Value = rightValue, DataType = new TSQLDataType(rightDbType.Value) };
        }
        else
        {
            rightOperand = new TSQLColumn { MultiPartIdentifier = rightMultiParIdentifier, ColumnName = rightValue };
        }

        if (!IsCalculation)
            ClearTempValues();

        var basicCondition = new TSQLBasicCondition { Operator = op } as SqlStatement;

        basicCondition.Children.Add(leftOperand as SqlStatement);
        basicCondition.Children.Add(rightOperand as SqlStatement);

        return basicCondition;
    }

    public ISqlQueryExpression GetLeftOperand()
    {
        if (LeftOperand != null)
            return LeftOperand;

        if (!string.IsNullOrEmpty(LeftMultiPartIdentifier))
            return new TSQLColumn { MultiPartIdentifier = LeftMultiPartIdentifier, ColumnName = LeftOperandValue };

        if (!string.IsNullOrEmpty(LeftOperandValue) && LeftOperandValue.IsDelimited())
        {
            return new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Variant),
                Value = LeftOperandValue,
                ComparisonOperator = this.ComparisonOperator
            };
        }

        if (IsQueryExpression)
        {
            Parent.Children.Remove(this);
            return this as ISqlQueryExpression;
        }

        if ((IsWhereClause || IsHaving ) && Children.Any() && Children.Last().IsQueryExpression)
        {
            var leftOperand = Children.Last();
            Children.Remove(leftOperand);
            return leftOperand as ISqlQueryExpression;
        }

        return new TSQLLiteral
        {
            DataType = new TSQLDataType(SqlDbType.VarChar),
            Value = LeftOperandValue
        };
    }
}
