using System;
using System.Data;
using KnightMoves.SqlObjects.SqlCode;
using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Function_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Select_Abs_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .ABS(decimal.MinValue).AS("Result1")
                .ABS("ColumnA").AS("Result2")
                .ABS("t", "ColumnB").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   ABS({decimal.MinValue}) AS [Result1],{NL}" +
                       $"   ABS(ColumnA) AS [Result2],{NL}" +
                       $"   ABS([t].[ColumnB]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Avg_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .AVG(decimal.MinValue).AS("Result1")
                .AVG("ColumnA").AS("Result2")
                .AVG("t", "ColumnB").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   AVG({decimal.MinValue}) AS [Result1],{NL}" +
                       $"   AVG(ColumnA) AS [Result2],{NL}" +
                       $"   AVG([t].[ColumnB]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Ceiling_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .CEILING(decimal.MinValue).AS("Result1")
                .CEILING("ColumnA").AS("Result2")
                .CEILING("t", "ColumnB").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   CEILING({decimal.MinValue}) AS [Result1],{NL}" +
                       $"   CEILING(ColumnA) AS [Result2],{NL}" +
                       $"   CEILING([t].[ColumnB]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Count_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .COUNT("*").AS("Result1")
                .COUNT("ColumnA").AS("Result2")
                .COUNT("t", "ColumnB").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   COUNT(*) AS [Result1],{NL}" +
                       $"   COUNT(ColumnA) AS [Result2],{NL}" +
                       $"   COUNT([t].[ColumnB]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_DateAdd_From()
    {
        // ARRANGE
        var testDate = DateTime.Today;
        var testDateStr = testDate.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

        var sqlObj =
            TSQL
              .SELECT()
                .DATEADD(DateParts.Day, 1, testDate).AS("Result1")
                .DATEADD(DateParts.Day, 1, "@dateVar").AS("Result2")
                .DATEADD(DateParts.Day, 1, "t", "ColumnB").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   DATEADD(Day, 1, '{testDateStr}') AS [Result1],{NL}" +
                       $"   DATEADD(Day, 1, @dateVar) AS [Result2],{NL}" +
                       $"   DATEADD(Day, 1, [t].[ColumnB]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_DateDiff_From()
    {
        // ARRANGE
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);
        var weekAgo = today.AddDays(-7);
        var weekAgoStr = weekAgo.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

        var sqlObj =
            TSQL
              .SELECT()
                .DATEDIFF(DateParts.Day, weekAgo, today).AS("Result1")
                .DATEDIFF(DateParts.Day, "@startDateVar", today).AS("Result2")
                .DATEDIFF(DateParts.Day, weekAgo, "@endDateVar").AS("Result3")
                .DATEDIFF(DateParts.Day, "@startDateVar", "@endDateVar").AS("Result4")
                .DATEDIFF(DateParts.Day, "t", "ColumnA", today).AS("Result5")
                .DATEDIFF(DateParts.Day, "t", "ColumnA", "@endDateVar").AS("Result6")
                .DATEDIFF(DateParts.Day, weekAgo, "t", "ColumnB").AS("Result7")
                .DATEDIFF(DateParts.Day, "t", "ColumnA", "t", "ColumnB").AS("Result8")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   DATEDIFF(Day, '{weekAgoStr}', '{todayStr}') AS [Result1],{NL}" +
                       $"   DATEDIFF(Day, @startDateVar, '{todayStr}') AS [Result2],{NL}" +
                       $"   DATEDIFF(Day, '{weekAgoStr}', @endDateVar) AS [Result3],{NL}" +
                       $"   DATEDIFF(Day, @startDateVar, @endDateVar) AS [Result4],{NL}" +
                       $"   DATEDIFF(Day, [t].[ColumnA], '{todayStr}') AS [Result5],{NL}" +
                       $"   DATEDIFF(Day, [t].[ColumnA], @endDateVar) AS [Result6],{NL}" +
                       $"   DATEDIFF(Day, '{weekAgoStr}', [t].[ColumnB]) AS [Result7],{NL}" +
                       $"   DATEDIFF(Day, [t].[ColumnA], [t].[ColumnB]) AS [Result8]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_DateName_From()
    {
        // ARRANGE
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

        var sqlObj =
            TSQL
              .SELECT()
                .DATENAME(DateParts.Day, today).AS("Result1")
                .DATENAME(DateParts.Month, "@dateVar").AS("Result2")
                .DATENAME(DateParts.Month, "t", "ColumnA").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   DATENAME(Day, '{todayStr}') AS [Result1],{NL}" +
                       $"   DATENAME(Month, @dateVar) AS [Result2],{NL}" +
                       $"   DATENAME(Month, [t].[ColumnA]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_DatePart_From()
    {
        // ARRANGE
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

        var sqlObj =
            TSQL
              .SELECT()
                .DATEPART(DateParts.Day, today).AS("Result1")
                .DATEPART(DateParts.Month, "@dateVar").AS("Result2")
                .DATEPART(DateParts.Month, "t", "ColumnA").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   DATEPART(Day, '{todayStr}') AS [Result1],{NL}" +
                       $"   DATEPART(Month, @dateVar) AS [Result2],{NL}" +
                       $"   DATEPART(Month, [t].[ColumnA]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_GetDate_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .GETDATE().AS("Result")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   GETDATE() AS [Result]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Day_From()
    {
        // ARRANGE
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

        var sqlObj =
            TSQL
              .SELECT()
                .DAY(today).AS("Result1")
                .DAY("@dateVar").AS("Result2")
                .DAY("t", "ColumnA").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   DAY('{todayStr}') AS [Result1],{NL}" +
                       $"   DAY(@dateVar) AS [Result2],{NL}" +
                       $"   DAY([t].[ColumnA]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Floor_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .FLOOR(decimal.MaxValue).AS("Result1")
                .FLOOR("@someVar").AS("Result2")
                .FLOOR("t", "ColumnA").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   FLOOR({decimal.MaxValue}) AS [Result1],{NL}" +
                       $"   FLOOR(@someVar) AS [Result2],{NL}" +
                       $"   FLOOR([t].[ColumnA]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Max_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .MAX("@someVar").AS("Result2")
                .MAX("t", "ColumnA").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   MAX(@someVar) AS [Result2],{NL}" +
                       $"   MAX([t].[ColumnA]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Min_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .MIN("@someVar").AS("Result2")
                .MIN("t", "ColumnA").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   MIN(@someVar) AS [Result2],{NL}" +
                       $"   MIN([t].[ColumnA]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Month_From()
    {
        // ARRANGE
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

        var sqlObj =
            TSQL
              .SELECT()
                .MONTH(today).AS("Result1")
                .MONTH("@dateVar").AS("Result2")
                .MONTH("t", "ColumnA").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   MONTH('{todayStr}') AS [Result1],{NL}" +
                       $"   MONTH(@dateVar) AS [Result2],{NL}" +
                       $"   MONTH([t].[ColumnA]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Sum_From()
    {
        // ARRANGE
        var sqlObj =
            TSQL
              .SELECT()
                .SUM("@someVar").AS("Result2")
                .SUM("t", "ColumnA").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   SUM(@someVar) AS [Result2],{NL}" +
                       $"   SUM([t].[ColumnA]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_Year_From()
    {
        // ARRANGE
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

        var sqlObj =
            TSQL
              .SELECT()
                .YEAR(today).AS("Result1")
                .YEAR("@dateVar").AS("Result2")
                .YEAR("t", "ColumnA").AS("Result3")
              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   YEAR('{todayStr}') AS [Result1],{NL}" +
                       $"   YEAR(@dateVar) AS [Result2],{NL}" +
                       $"   YEAR([t].[ColumnA]) AS [Result3]{NL}" +
                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Select_IsNull_As_From()
    {
        // ARRANGE
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);
        var queryExpression = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "QryExp" };

        var checkExpression1 = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "CheckExp" };
        var checkExpression2 = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Int), ColumnName = "CheckExp" };
        var checkExpression3 = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.DateTime), ColumnName = "CheckExp" };

        var replaceExpression1 = new TSQLColumn { DataType = new TSQLDataType(SqlDbType.Variant), ColumnName = "ReplaceExp" };
        var replaceExpression2 = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.Int), Value = "99" };
        var replaceExpression3 = new TSQLLiteral { DataType = new TSQLDataType(SqlDbType.DateTime), Value = todayStr };

        var sqlObj =
            TSQL
              .SELECT()

                .ISNULL("t", "Column1", "default").AS("Result1")
                .ISNULL("t", "Column2", 99).AS("Result2")
                .ISNULL("t", "Column3", today).AS("Result3")
                .ISNULL("t", "Column4", true).AS("Result4")
                .ISNULL("t", "Column5", long.MaxValue).AS("Result5")
                .ISNULL("t", "Column6", Guid.Empty).AS("Result6")
                .ISNULL("t", "Column7", 'X').AS("Result7")
                .ISNULL("t", "Column8", decimal.MaxValue).AS("Result8")

                .ISNULL("Column9", "default").AS("Result9")
                .ISNULL("@someParam", "default").AS("Result10")

                .ISNULL("[Column11]", "default").AS("Result11")
                .ISNULL("Column12", 99).AS("Result12")
                .ISNULL("Column13", today).AS("Result13")
                .ISNULL("Column14", true).AS("Result14")
                .ISNULL("Column15", long.MaxValue).AS("Result15")
                .ISNULL("Column16", Guid.Empty).AS("Result16")
                .ISNULL("Column17", 'X').AS("Result17")
                .ISNULL("Column18", decimal.MaxValue).AS("Result18")

                .ISNULL(queryExpression, "default").AS("Result19")
                .ISNULL(queryExpression, 99).AS("Result20")
                .ISNULL(queryExpression, today).AS("Result21")
                .ISNULL(queryExpression, true).AS("Result22")
                .ISNULL(queryExpression, long.MaxValue).AS("Result23")
                .ISNULL(queryExpression, Guid.Empty).AS("Result24")
                .ISNULL(queryExpression, 'X').AS("Result25")
                .ISNULL(queryExpression, decimal.MaxValue).AS("Result26")

                .ISNULL(checkExpression1, replaceExpression1).AS("Result27")
                .ISNULL(checkExpression2, replaceExpression2).AS("Result28")
                .ISNULL(checkExpression3, replaceExpression3).AS("Result29")

              .FROM("dbo", "MyTable", "t");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  SELECT{NL}" +
                       $"   ISNULL([t].[Column1], 'default') AS [Result1],{NL}" +
                       $"   ISNULL([t].[Column2], 99) AS [Result2],{NL}" +
                       $"   ISNULL([t].[Column3], '{todayStr}') AS [Result3],{NL}" +
                       $"   ISNULL([t].[Column4], 1) AS [Result4],{NL}" +
                       $"   ISNULL([t].[Column5], {long.MaxValue}) AS [Result5],{NL}" +
                       $"   ISNULL([t].[Column6], '{Guid.Empty}') AS [Result6],{NL}" +
                       $"   ISNULL([t].[Column7], 'X') AS [Result7],{NL}" +
                       $"   ISNULL([t].[Column8], {decimal.MaxValue}) AS [Result8],{NL}" +

                       $"   ISNULL([Column9], 'default') AS [Result9],{NL}" +
                       $"   ISNULL(@someParam, 'default') AS [Result10],{NL}" +

                       $"   ISNULL([Column11], 'default') AS [Result11],{NL}" +
                       $"   ISNULL([Column12], 99) AS [Result12],{NL}" +
                       $"   ISNULL([Column13], '{todayStr}') AS [Result13],{NL}" +
                       $"   ISNULL([Column14], 1) AS [Result14],{NL}" +
                       $"   ISNULL([Column15], {long.MaxValue}) AS [Result15],{NL}" +
                       $"   ISNULL([Column16], '{Guid.Empty}') AS [Result16],{NL}" +
                       $"   ISNULL([Column17], 'X') AS [Result17],{NL}" +
                       $"   ISNULL([Column18], {decimal.MaxValue}) AS [Result18],{NL}" +

                       $"   ISNULL([QryExp], 'default') AS [Result19],{NL}" +
                       $"   ISNULL([QryExp], 99) AS [Result20],{NL}" +
                       $"   ISNULL([QryExp], '{todayStr}') AS [Result21],{NL}" +
                       $"   ISNULL([QryExp], 1) AS [Result22],{NL}" +
                       $"   ISNULL([QryExp], {long.MaxValue}) AS [Result23],{NL}" +
                       $"   ISNULL([QryExp], '{Guid.Empty}') AS [Result24],{NL}" +
                       $"   ISNULL([QryExp], 'X') AS [Result25],{NL}" +
                       $"   ISNULL([QryExp], {decimal.MaxValue}) AS [Result26],{NL}" +

                       $"   ISNULL([CheckExp], [ReplaceExp]) AS [Result27],{NL}" +
                       $"   ISNULL([CheckExp], 99) AS [Result28],{NL}" +
                       $"   ISNULL([CheckExp], '{todayStr}') AS [Result29]{NL}" +

                       $"  FROM [dbo].[MyTable] t{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
