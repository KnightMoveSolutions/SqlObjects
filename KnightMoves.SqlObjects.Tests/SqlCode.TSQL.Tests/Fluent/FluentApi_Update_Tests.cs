using System;
using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Update_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Update_Table_Where()
    {
        // ARRANGE
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);
        var guid = Guid.NewGuid();

        var sqlObj = 
            TSQL
            .UPDATE("dbo", "SomeTable").SET()
              .COLUMN("StringColumn").IsEqualTo("Foo")
              .COLUMN("IntColumn").IsEqualTo(99)
              .COLUMN("DateTimeColumn").IsEqualTo(today)
              .COLUMN("BoolColumn").IsEqualTo(true)
              .COLUMN("LongColumn").IsEqualTo(long.MaxValue)
              .COLUMN("GuidColumn").IsEqualTo(guid)
              .COLUMN("CharColumn").IsEqualTo('X')
              .COLUMN("DecimalColumn").IsEqualTo(decimal.MaxValue)
            .WHERE("Id").IsEqualTo(1);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  UPDATE [dbo].[SomeTable] SET{NL}" +
                       $"   [StringColumn] = 'Foo',{NL}" +
                       $"   [IntColumn] = 99,{NL}" +
                       $"   [DateTimeColumn] = '{todayStr}',{NL}" +
                       $"   [BoolColumn] = 1,{NL}" +
                       $"   [LongColumn] = {long.MaxValue},{NL}" +
                       $"   [GuidColumn] = '{guid}',{NL}" +
                       $"   [CharColumn] = 'X',{NL}" +
                       $"   [DecimalColumn] = {decimal.MaxValue}{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND [Id] = 1{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Update_Table_Where_Int()
    {
        // ARRANGE / ACTION
        var sqlObj = 
            TSQL
            .UPDATE("dbo", "SomeTable").SET()
              .COLUMN("StringColumn").IsEqualTo("Foo")
            .WHERE(1).IsEqualTo("SomeTable", "Id");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  UPDATE [dbo].[SomeTable] SET{NL}" +
                       $"   [StringColumn] = 'Foo'{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND 1 = [SomeTable].[Id]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Update_Table_Where_DateTime()
    {
        // ARRANGE
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);

        var sqlObj = 
            TSQL
            .UPDATE("dbo", "SomeTable").SET()
              .COLUMN("StringColumn").IsEqualTo("Foo")
            .WHERE(today).IsEqualTo("SomeTable", "SomeDateColumn");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  UPDATE [dbo].[SomeTable] SET{NL}" +
                       $"   [StringColumn] = 'Foo'{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND '{todayStr}' = [SomeTable].[SomeDateColumn]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Update_Table_Where_Bool()
    {
        // ARRANGE / ACTION
        var sqlObj =
            TSQL
            .UPDATE("dbo", "SomeTable").SET()
              .COLUMN("StringColumn").IsEqualTo("Foo")
            .WHERE(true).IsEqualTo("SomeTable", "SomeBoolColumn");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  UPDATE [dbo].[SomeTable] SET{NL}" +
                       $"   [StringColumn] = 'Foo'{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND 1 = [SomeTable].[SomeBoolColumn]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Update_Table_Where_Long()
    {
        // ARRANGE / ACTION
        var sqlObj =
            TSQL
            .UPDATE("dbo", "SomeTable").SET()
              .COLUMN("StringColumn").IsEqualTo("Foo")
            .WHERE(long.MaxValue).IsEqualTo("SomeTable", "SomeLongColumn");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  UPDATE [dbo].[SomeTable] SET{NL}" +
                       $"   [StringColumn] = 'Foo'{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND {long.MaxValue} = [SomeTable].[SomeLongColumn]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Update_Table_Where_Guid()
    {
        // ARRANGE
        var guid = Guid.NewGuid();

        var sqlObj = 
            TSQL
            .UPDATE("dbo", "SomeTable").SET()
              .COLUMN("StringColumn").IsEqualTo("Foo")
            .WHERE(guid).IsEqualTo("SomeTable", "SomeGuidColumn");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  UPDATE [dbo].[SomeTable] SET{NL}" +
                       $"   [StringColumn] = 'Foo'{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND '{guid}' = [SomeTable].[SomeGuidColumn]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Update_Table_Where_Char()
    {
        // ARRANGE / ACTION
        var sqlObj =
            TSQL
            .UPDATE("dbo", "SomeTable").SET()
              .COLUMN("StringColumn").IsEqualTo("Foo")
            .WHERE('X').IsEqualTo("SomeTable", "SomeCharColumn");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  UPDATE [dbo].[SomeTable] SET{NL}" +
                       $"   [StringColumn] = 'Foo'{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND 'X' = [SomeTable].[SomeCharColumn]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Update_Table_Where_Decimal()
    {
        // ARRANGE / ACTION
        var sqlObj =
            TSQL
            .UPDATE("dbo", "SomeTable").SET()
              .COLUMN("StringColumn").IsEqualTo("Foo")
            .WHERE(decimal.MaxValue).IsEqualTo("SomeTable", "SomeDecimalColumn");

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  UPDATE [dbo].[SomeTable] SET{NL}" +
                       $"   [StringColumn] = 'Foo'{NL}" +
                       $"  WHERE 1=1{NL}" +
                       $"   AND {decimal.MaxValue} = [SomeTable].[SomeDecimalColumn]{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
