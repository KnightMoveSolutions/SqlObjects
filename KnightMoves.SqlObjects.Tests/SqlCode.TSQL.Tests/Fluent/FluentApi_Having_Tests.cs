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
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Having_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_From_GroupBy_Having_Default()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .COUNT("*").AS("Count")
                .COLUMN("ColumnA")
              .FROM("Customers")
              .GROUPBY()
                .COLUMN("ColumnA")
              .HAVING();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   COUNT(*) AS [Count],{NL}" +
                       $"   [ColumnA]{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  GROUP BY{NL}" +
                       $"   [ColumnA]{NL}" +
                       $"  HAVING 1=1{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_From_GroupBy_Having_Literal_Throws_Exception()
    {
        // ARRANGE / ACTION
        var threwException = false;

        try
        {
            var sql =
                TSQL
                  .SELECT()
                    .COUNT("*").AS("Count")
                    .COLUMN("ColumnA")
                  .FROM("Customers")
                  .GROUPBY()
                    .COLUMN("ColumnA")
                  .HAVING(new TSQLLiteral { Value = "@someVar" }).IsEqualTo(99)
                  .Build();
        }
        catch (ArgumentException)
        {
            threwException = true;
        }

        // ASSERT
        Assert.True(threwException);
    }

    [Fact]
    public void Select_From_GroupBy_Having_Literal()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .COUNT("*").AS("Count")
                .COLUMN("ColumnA")
              .FROM("Customers")
              .GROUPBY()
                .COLUMN("ColumnA")
              .HAVING(new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Variant), Value = "@someVar" }).IsEqualTo(99);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   COUNT(*) AS [Count],{NL}" +
                       $"   [ColumnA]{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  GROUP BY{NL}" +
                       $"   [ColumnA]{NL}" +
                       $"  HAVING 1=1{NL}" + 
                       $"   AND @someVar = 99{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_From_GroupBy_Having_Basic()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .COUNT("*").AS("Count")
                .COLUMN("ColumnA")
              .FROM("Customers")
              .GROUPBY()
                .COLUMN("ColumnA")
              .HAVING("c", "Id").IsEqualTo(99);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   COUNT(*) AS [Count],{NL}" +
                       $"   [ColumnA]{NL}" +
                       $"  FROM [Customers]{NL}" +
                       $"  GROUP BY{NL}" +
                       $"   [ColumnA]{NL}" +
                       $"  HAVING 1=1{NL}" +
                       $"   AND [c].[Id] = 99{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_From_GroupBy_Having_MultipleConditions()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .COUNT("*").AS("Count")
                .COLUMN("Id")
                .COLUMN("LastName")
                .COLUMN("FirstName")
                .COLUMN("Age")
                .COLUMN("Gender")
                .COLUMN("SomeColumn")
              .FROM("dbo", "Customers", "c")
              .GROUPBY()
                .COLUMN("Id")
                .COLUMN("LastName")
                .COLUMN("FirstName")
                .COLUMN("Age")
                .COLUMN("Gender")
                .COLUMN("SomeColumn")
              .HAVING("c", "Id").IsEqualTo(99).AND()
                .COLUMN("LastName").IsEqualTo("Jones").AND()
                .COLUMN("FirstName").LIKE("M%").AND()
                .COLUMN("Age").IsGreaterThanOrEqualTo(21).AND()
                .Calculate(2).Times(2).IsEqualTo(4).AND()
                .Literal("21").IsLessThan("c", "Age").AND()
                .Literal("@someVar").LIKE("%pattern%").AND()
                .StartConditionScope()
                    .COLUMN("Gender").IsEqualTo("M").OR()
                    .COLUMN("Gender").IsEqualTo("")
                .EndConditionScope().AND()
                .COLUMN("SomeColumn").IN(1, 2, 3).AND()
                .DATEPART(DateParts.Month, "c", "DOB").IsEqualTo(8).AND()
                .SubQueryStart()
                    .SELECT()
                      .COUNT("*")
                    .FROM("dbo", "Categories")
                .SubQueryEnd().IsGreaterThan(5).AND()
                .CASE(SqlDbType.VarChar)
                    .WHEN("c", "Gender").IsEqualTo("M").THEN("Male")
                    .WHEN("c", "Gender").IsEqualTo("F").THEN("Female")
                    .WHEN("c", "Gender").IsEqualTo("").THEN("Unspecified")
                    .ELSE("Other")
                .END().IsEqualTo("Male");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   COUNT(*) AS [Count],{NL}" +
                       $"   [Id],{NL}" +
                       $"   [LastName],{NL}" +
                       $"   [FirstName],{NL}" +
                       $"   [Age],{NL}" +
                       $"   [Gender],{NL}" +
                       $"   [SomeColumn]{NL}" +
                       $"  FROM [dbo].[Customers] c{NL}" +
                       $"  GROUP BY{NL}" +
                       $"   [Id],{NL}" +
                       $"   [LastName],{NL}" +
                       $"   [FirstName],{NL}" +
                       $"   [Age],{NL}" +
                       $"   [Gender],{NL}" +
                       $"   [SomeColumn]{NL}" +
                       $"  HAVING 1=1{NL}" +
                       $"   AND [c].[Id] = 99{NL}" +
                       $"   AND [LastName] = 'Jones'{NL}" +
                       $"   AND [FirstName] LIKE 'M%'{NL}" +
                       $"   AND [Age] >= 21{NL}" +
                       $"   AND (2 * 2) = 4{NL}" +
                       $"   AND 21 < [c].[Age]{NL}" +
                       $"   AND @someVar LIKE '%pattern%'{NL}" +
                       $"   AND{NL}" +
                       $"   ({NL}" +
                       $"    [Gender] = 'M' OR{NL}" +
                       $"    [Gender] = ''{NL}" +
                       $"   ){NL}" +
                       $"   AND [SomeColumn] IN (1,2,3){NL}" +
                       $"   AND DATEPART(Month, [c].[DOB]) = 8{NL}" +
                       $"   AND ({NL}" +
                       $"     SELECT{NL}" +
                       $"      COUNT(*){NL}" +
                       $"     FROM [dbo].[Categories]{NL}" +
                       $"    ) > 5{NL}" +
                       $"   AND CASE{NL}" +
                       $"     WHEN [c].[Gender] = 'M' THEN 'Male'{NL}" +
                       $"     WHEN [c].[Gender] = 'F' THEN 'Female'{NL}" +
                       $"     WHEN [c].[Gender] = '' THEN 'Unspecified'{NL}" +
                       $"     ELSE 'Other'{NL}" +
                       $"    END = 'Male'{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_From_GroupBy_Having_RightOp_Is_QueryExpression()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .COUNT("*").AS("Count")
                .COLUMN("Id")
                .COLUMN("GenderId")
                .COLUMN("LastName")
                .COLUMN("FirstName")
                .COLUMN("Age")
                .COLUMN("Gender")
                .COLUMN("SomeColumn")
              .FROM("dbo", "Customers", "c")
              .GROUPBY()
                .COLUMN("Id")
                .COLUMN("GenderId")
                .COLUMN("LastName")
                .COLUMN("FirstName")
                .COLUMN("Age")
                .COLUMN("Gender")
                .COLUMN("SomeColumn")
              .HAVING("c", "GenderId").IsEqualTo()
                .CASE(SqlDbType.Int)
                    .WHEN("[Gender]").IsEqualTo("M").THEN(1)
                    .ELSE(0)
                .END().AND()
                .COLUMN("Age").IsLessThan()
                    .SubQueryStart()
                        .SELECT().COUNT("*").FROM("SomeTable")
                    .SubQueryEnd()
                .AND("[Age]").IsLessThan()
                    .SubQueryStart()
                        .SELECT().COUNT("*").FROM("SomeTable")
                    .SubQueryEnd()
                .AND("[SomeColumn]").IsGreaterThan()
                    .CASE(SqlDbType.Int)
                        .WHEN("[ColumnA]").IsEqualTo("A").THEN(1)
                        .ELSE(0)
                    .END();

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   COUNT(*) AS [Count],{NL}" +
                       $"   [Id],{NL}" +
                       $"   [GenderId],{NL}" +
                       $"   [LastName],{NL}" +
                       $"   [FirstName],{NL}" +
                       $"   [Age],{NL}" +
                       $"   [Gender],{NL}" +
                       $"   [SomeColumn]{NL}" +
                       $"  FROM [dbo].[Customers] c{NL}" +
                       $"  GROUP BY{NL}" +
                       $"   [Id],{NL}" +
                       $"   [GenderId],{NL}" +
                       $"   [LastName],{NL}" +
                       $"   [FirstName],{NL}" +
                       $"   [Age],{NL}" +
                       $"   [Gender],{NL}" +
                       $"   [SomeColumn]{NL}" +
                       $"  HAVING 1=1{NL}" +
                       $"   AND [c].[GenderId] = CASE{NL}" +
                       $"     WHEN [Gender] = 'M' THEN 1{NL}" +
                       $"     ELSE 0{NL}" +
                       $"    END{NL}" +
                       $"   AND [Age] < ({NL}" +
                       $"     SELECT{NL}" +
                       $"      COUNT(*){NL}" +
                       $"     FROM [SomeTable]{NL}" +
                       $"    ){NL}" +
                       $"   AND [Age] < ({NL}" +
                       $"     SELECT{NL}" +
                       $"      COUNT(*){NL}" +
                       $"     FROM [SomeTable]{NL}" +
                       $"    ){NL}" +
                       $"   AND [SomeColumn] > CASE{NL}" +
                       $"     WHEN [ColumnA] = 'A' THEN 1{NL}" +
                       $"     ELSE 0{NL}" +
                       $"    END{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
