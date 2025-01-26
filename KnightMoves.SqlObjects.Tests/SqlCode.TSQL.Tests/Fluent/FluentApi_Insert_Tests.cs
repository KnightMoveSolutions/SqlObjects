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
using KnightMoves.SqlObjects.SqlCode;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql.Fluent;

public class FluentApi_Insert_Tests
{
    private readonly string NL = Environment.NewLine;

    [Fact]
    public void Insert_Into_Values()
    {
        // ARRANGE
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);
        var guid = Guid.NewGuid();

        var sqlObj = TSQL
            .INSERT().INTO("SomeTable")
              .COLUMN("StringColumn")
              .COLUMN("IntColumn")
              .COLUMN("DateTimeColumn")
              .COLUMN("BoolColumn")
              .COLUMN("LongColumn")
              .COLUMN("GuidColumn")
              .COLUMN("CharColumn")
              .COLUMN("DecimalColumn")
            .VALUES()
              .VALUE("Foo")
              .VALUE(99)
              .VALUE(today)
              .VALUE(true)
              .VALUE(long.MaxValue)
              .VALUE(guid)
              .VALUE('X')
              .VALUE(decimal.MaxValue);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  INSERT INTO{NL}" +
                       $"  ({NL}" +
                       $"   [StringColumn],{NL}" +
                       $"   [IntColumn],{NL}" +
                       $"   [DateTimeColumn],{NL}" +
                       $"   [BoolColumn],{NL}" +
                       $"   [LongColumn],{NL}" +
                       $"   [GuidColumn],{NL}" +
                       $"   [CharColumn],{NL}" +
                       $"   [DecimalColumn]{NL}" +
                       $"  ) VALUES ({NL}" +
                       $"   'Foo',{NL}" +
                       $"   99,{NL}" +
                       $"   '{todayStr}',{NL}" +
                       $"   1,{NL}" +
                       $"   {long.MaxValue},{NL}" +
                       $"   '{guid}',{NL}" +
                       $"   'X',{NL}" +
                       $"   {decimal.MaxValue}{NL}" +
                       $"  ){NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }

    [Fact]
    public void Insert_Into_Select()
    {
        // ARRANGE
        var today = DateTime.Today;
        var todayStr = today.ToString(SqlDataType.SQL_DATE_STRING_FORMAT);
        var guid = Guid.NewGuid();

        var sqlObj = TSQL
            .INSERT().INTO("SomeTable")
              .COLUMN("StringColumn")
              .COLUMN("IntColumn")
              .COLUMN("DateTimeColumn")
              .COLUMN("BoolColumn")
              .COLUMN("LongColumn")
              .COLUMN("GuidColumn")
              .COLUMN("CharColumn")
              .COLUMN("DecimalColumn")
            .SELECT()
              .VALUE("Foo")
              .VALUE(99)
              .VALUE(today)
              .VALUE(true)
              .VALUE(long.MaxValue)
              .VALUE(guid)
              .VALUE('X')
              .VALUE(decimal.MaxValue);

        // ACTION
        var sql = sqlObj.Build();

        var expected = $"  INSERT INTO{NL}" +
                       $"  ({NL}" +
                       $"   [StringColumn],{NL}" +
                       $"   [IntColumn],{NL}" +
                       $"   [DateTimeColumn],{NL}" +
                       $"   [BoolColumn],{NL}" +
                       $"   [LongColumn],{NL}" +
                       $"   [GuidColumn],{NL}" +
                       $"   [CharColumn],{NL}" +
                       $"   [DecimalColumn]{NL}" +
                       $"  ){NL}" +
                       $"  SELECT{NL}" +
                       $"   'Foo',{NL}" +
                       $"   99,{NL}" +
                       $"   '{todayStr}',{NL}" +
                       $"   1,{NL}" +
                       $"   {long.MaxValue},{NL}" +
                       $"   '{guid}',{NL}" +
                       $"   'X',{NL}" +
                       $"   {decimal.MaxValue}{NL}";

        // ASSERT
        Assert.Equal(expected, sql);
        TestHelper.Assert.SerializationWorks(expected, sqlObj);
    }
}
