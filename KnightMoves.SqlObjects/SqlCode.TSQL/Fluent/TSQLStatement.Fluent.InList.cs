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
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="string"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params string[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (string value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.VarChar),
                Value = value
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

    /// <summary>
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="int"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params int[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (int value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Int),
                Value = value.ToString()
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

    /// <summary>
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="DateTime"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params DateTime[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (DateTime value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.DateTime),
                Value = value.ToString(SqlDataType.SQL_DATE_STRING_FORMAT)
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

    /// <summary>
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="long"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params long[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (long value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.BigInt),
                Value = value.ToString()
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

    /// <summary>
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="char"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params char[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (char value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Char),
                Value = value.ToString()
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

    /// <summary>
    /// Finishes a condition with a SQL IN (...) clause using the specified <see cref="decimal"/> <paramref name="values"/> as the list
    /// </summary>
    public override SqlStatement IN(params decimal[] values)
    {
        var parent = GetConditionParent();

        var inList = new TSQLInListCondition();

        inList.Children.Add(GetLeftOperand() as SqlStatement);

        foreach (decimal value in values)
        {
            inList.InList.Add(new TSQLLiteral
            {
                DataType = new TSQLDataType(SqlDbType.Decimal),
                Value = value.ToString()
            });
        }

        parent.Children.Add(inList);

        return parent;
    }

}
