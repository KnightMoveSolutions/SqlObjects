/*
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

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

public abstract partial class TSQLStatement : SqlStatement
{
    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided StartVal. If <paramref name="multipartIdentifier"/> 
    /// is provided then it will interpret <paramref name="value"/> as a column name. Otherwise, it will 
    /// treat <paramref name="value"/> as a string that will be surrounded in single quotes.
    /// </summary>
    public override SqlStatement BETWEEN(string multipartIdentifier, string value)
    {
        ISqlQueryExpression startVal;

        if (!string.IsNullOrEmpty(multipartIdentifier))
        {
            startVal = new TSQLColumn { MultiPartIdentifier = multipartIdentifier, ColumnName = value };
        }
        else
        {
            startVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.VarChar),
                Value = value
            };
        }

        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = startVal
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided <see cref="int"/> <paramref name="value"/> as the StartVal
    /// </summary>
    public override SqlStatement BETWEEN(int value)
    {
        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Int),
                Value = value.ToString()
            }
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided <see cref="DateTime"/> <paramref name="value"/> as the StartVal
    /// </summary>
    public override SqlStatement BETWEEN(DateTime value)
    {
        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.DateTime),
                Value = value.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)
            }
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided <see cref="long"/> <paramref name="value"/> as the StartVal
    /// </summary>
    public override SqlStatement BETWEEN(long value)
    {
        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.BigInt),
                Value = value.ToString()
            }
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided <see cref="char"/> <paramref name="value"/> as the StartVal
    /// </summary>
    public override SqlStatement BETWEEN(char value)
    {
        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Char),
                Value = value.ToString()
            }
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

    /// <summary>
    /// Starts a SQL BETWEEN clause with the provided <see cref="decimal"/> <paramref name="value"/> as the StartVal
    /// </summary>
    public override SqlStatement BETWEEN(decimal value)
    {
        var between = new TSQLBetweenCondition
        {
            LeftOperand = GetLeftOperand(),
            StartVal = new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Decimal),
                Value = value.ToString()
            }
        };

        var parentStatement = GetConditionParent();

        parentStatement.Children.Add(between);

        return between;
    }

}
