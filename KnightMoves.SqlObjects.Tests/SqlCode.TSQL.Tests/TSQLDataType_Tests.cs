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

using KnightMoves.SqlObjects.SqlCode.TSQL;
using Xunit;

namespace KnightMoves.SqlObjects.Tests.TSql;

public class TSQLDataType_Tests
{
    [Fact]
    public void DataType_IsEqualTo_SqlDbType()
    {
        TSQLDataType dt = new TSQLDataType(System.Data.SqlDbType.Int);

        Assert.Equal("Int", dt.DataType);
    }

    [Fact]
    public void TSQLDataType_OperatorOverload_Works()
    {
        TSQLDataType dt1 = new TSQLDataType(System.Data.SqlDbType.Int);
        TSQLDataType dt2 = new TSQLDataType(System.Data.SqlDbType.Int);
        TSQLDataType dt3 = dt1;
        TSQLDataType dt4 = new TSQLDataType(System.Data.SqlDbType.Text);

        Assert.True(dt1 == dt2, "dt1 was not == to dt2");
        Assert.False(dt1 != dt2, "dt1 was not != to dt2");
        Assert.False(ReferenceEquals(dt1, dt2), "ReferenceEquals(dt1,dt2) returned true but should be false");
        Assert.True(dt1.Equals(dt2), "dt1.Equals(dt2) returned false but should be true");

        Assert.True(dt1 == dt3, "dt1 was not == to dt3");
        Assert.False(dt1 != dt3, "dt1 was not != to dt3");
        Assert.True(ReferenceEquals(dt1, dt3), "ReferenceEquals(dt1,dt3) returned false but should be true");
        Assert.True(dt1.Equals(dt2), "dt1.Equals(dt3) returned false but should be true");

        Assert.False(dt1 == dt4, "dt1 == dt4 returned true but should be false.");
        Assert.True(dt1 != dt4, "dt1 != dt4 returned false but should be true.");
    }
}
