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
using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlUpdate"/> and builds a SQL UPDATE ... SET statement
/// using all <see cref="ISqlBasicCondition"/> objects added to it for the update list
/// </summary>
public class TSQLUpdate : TSQLStatement, ISqlUpdate
{
    /// <summary>
    /// The Schema of the Table
    /// </summary>
    public string Schema { get; set; }

    /// <summary>
    /// The Table name
    /// </summary>
    public string Table { get; set; }

    public TSQLUpdate() { }

    public TSQLUpdate(string table) : this(string.Empty, table) { }

    public TSQLUpdate(string schema, string table)
    {
        Schema = schema;
        Table = table;
    }

    /// <summary>
    /// Builds a SQL UPDATE ... SET statement using any <see cref="ISqlBasicCondition"/> objects 
    /// added to it for the update expressions.
    /// <code>UPDATE [Schema].[Table] SET</code>
    /// <code>[Column1] = [Value1],</code>
    /// <code>[Column2] = [Value2],</code>
    /// <code>[ColumnN] = [ValueN]</code>
    /// <para>The WHERE clause is built using the WHERE() fluent methods or manually building a <see cref="TSQLWhereClause"/> object</para>
    /// </summary>
    public override string SQL()
    {
        var sql = new StringBuilder();

        sql.Append($"{IndentString}UPDATE ");

        if (!string.IsNullOrEmpty(Schema))
            sql.Append($"[{Schema.SanitizeDelimitedValue()}].");

        sql.Append($"[{Table.SanitizeDelimitedValue()}] SET{Environment.NewLine}");

        var updates = Children
                        .Where(c => 
                        {
                            var u = c as ISqlBasicCondition;

                            return c.IsComment ||
                            (
                                u != null && 
                                c.Children.FirstOrDefault() is ISqlColumn && 
                                u.Operator == SqlComparisonOperators.IsEqualTo
                            );
                        })
                        .ToList();

        foreach (var sqlObject in updates)
        {
            if (sqlObject.IsComment)
            {
                sql.Append($"{sqlObject}");
                continue;
            }

            var u = sqlObject as ISqlBasicCondition;

            var leftOperand = sqlObject.Children.FirstOrDefault(c => c is ISqlQueryExpression);
            var rightOperand = sqlObject.Children.LastOrDefault(c => c is ISqlQueryExpression);

            sql.Append($"{sqlObject.IndentString}{leftOperand.ToString().Trim()} = {rightOperand.ToString().Trim()},{Environment.NewLine}");
        }

        if (updates.LastOrDefault(x => x.IsBasicCondition) != null)
            sql.Remove(sql.Length - 3, 1);

        return sql.ToString();
    }
}
