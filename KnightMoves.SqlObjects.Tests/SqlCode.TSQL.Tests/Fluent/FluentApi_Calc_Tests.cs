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
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Calc_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Calculate_Plus_From()
    {
        // ARRANGE 
        var sqlObj =
            TSQL
              .SELECT()
                .Calculate("t", "ColumnA").Plus("ColumnB").AS("Result1")
                .Calculate(2).Plus(2).AS("Result2")
                .Calculate(decimal.MinValue).Plus(decimal.MaxValue).AS("Result3")
                .Calculate(long.MinValue).Plus(long.MaxValue).AS("Result4")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   ([t].[ColumnA] + [ColumnB]) AS [Result1],{NL}" +
                       $"   (2 + 2) AS [Result2],{NL}" +
                       $"   ({decimal.MinValue} + {decimal.MaxValue}) AS [Result3],{NL}" +
                       $"   ({long.MinValue} + {long.MaxValue}) AS [Result4]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Calculate_Minus_From()
    {
        // ARRANGE 
        var sqlObj =
            TSQL
              .SELECT()
                .Calculate("t", "ColumnA").Minus("ColumnB").AS("Result1")
                .Calculate(2).Minus(2).AS("Result2")
                .Calculate(decimal.MinValue).Minus(decimal.MaxValue).AS("Result3")
                .Calculate(long.MinValue).Minus(long.MaxValue).AS("Result4")
              .FROM("dbo", "MyTable", "t");
        
        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   ([t].[ColumnA] - [ColumnB]) AS [Result1],{NL}" +
                       $"   (2 - 2) AS [Result2],{NL}" +
                       $"   ({decimal.MinValue} - {decimal.MaxValue}) AS [Result3],{NL}" +
                       $"   ({long.MinValue} - {long.MaxValue}) AS [Result4]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Calculate_Times_From()
    {
        // ARRANGE 
        var sqlObj =
            TSQL
              .SELECT()
                .Calculate("t", "ColumnA").Times("ColumnB").AS("Result1")
                .Calculate(2).Times(2).AS("Result2")
                .Calculate(decimal.MinValue).Times(decimal.MaxValue).AS("Result3")
                .Calculate(long.MinValue).Times(long.MaxValue).AS("Result4")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   ([t].[ColumnA] * [ColumnB]) AS [Result1],{NL}" +
                       $"   (2 * 2) AS [Result2],{NL}" +
                       $"   ({decimal.MinValue} * {decimal.MaxValue}) AS [Result3],{NL}" +
                       $"   ({long.MinValue} * {long.MaxValue}) AS [Result4]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Calculate_DividedBy_From()
    {
        // ARRANGE 
        var sqlObj =
            TSQL
              .SELECT()
                .Calculate("t", "ColumnA").DividedBy("ColumnB").AS("Result1")
                .Calculate(2).DividedBy(2).AS("Result2")
                .Calculate(decimal.MinValue).DividedBy(decimal.MaxValue).AS("Result3")
                .Calculate(long.MinValue).DividedBy(long.MaxValue).AS("Result4")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   ([t].[ColumnA] / [ColumnB]) AS [Result1],{NL}" +
                       $"   (2 / 2) AS [Result2],{NL}" +
                       $"   ({decimal.MinValue} / {decimal.MaxValue}) AS [Result3],{NL}" +
                       $"   ({long.MinValue} / {long.MaxValue}) AS [Result4]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Calculate_Modulo_From()
    {
        // ARRANGE 
        var sqlObj =
            TSQL
              .SELECT()
                .Calculate("t", "ColumnA").Modulo("ColumnB").AS("Result1")
                .Calculate(2).Modulo(2).AS("Result2")
                .Calculate(decimal.MinValue).Modulo(decimal.MaxValue).AS("Result3")
                .Calculate(long.MinValue).Modulo(long.MaxValue).AS("Result4")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   ([t].[ColumnA] % [ColumnB]) AS [Result1],{NL}" +
                       $"   (2 % 2) AS [Result2],{NL}" +
                       $"   ({decimal.MinValue} % {decimal.MaxValue}) AS [Result3],{NL}" +
                       $"   ({long.MinValue} % {long.MaxValue}) AS [Result4]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
