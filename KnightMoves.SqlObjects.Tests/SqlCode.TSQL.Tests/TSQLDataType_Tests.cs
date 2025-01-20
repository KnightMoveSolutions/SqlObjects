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
