namespace KnightMoves.SqlObjects.SqlCode;

/// <summary>
/// <para>
/// This abstract class represents a SQL Data Type. It implements the operator overloads so that objects of 
/// this type can be used with C# code like "==", "!=" or "Equals(...)"
/// </para>
/// <para>
/// Classes that implement this abstract class must provide an implementation for the <see cref="DataType"/> property.
/// </para>
/// </summary>
public class SqlDataType
{
    public static string SQL_DATE_STRING_FORMAT = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    /// Classes that implement this abstract class must provide an implementation of this abstract property. 
    /// It should return the name of a SQL data type in the form of a string. 
    /// </summary>
    public virtual string DataType { get; set; }

    /// <summary>
    /// Overloads the C# "==" operator so that two SqlDataType objects can be easily tested for equality.
    /// </summary>
    /// <param name="a">The first SqlDataType object to be compared for equality against the second SqlDataType object</param>
    /// <param name="b">The second SqlDataType object to be compared for equality against the first SqlDataType object</param>
    /// <returns>True if <paramref name="a"/> is equal to <paramref name="b"/>, false if not</returns>
    public static bool operator ==(SqlDataType a, SqlDataType b)
    {
        // If both are null, or both are same instance, return true.
        if (ReferenceEquals(a, b))
            return true;

        // If one is null, but not both, return false.
        if ((a is null) || (b is null))
            return false;

        // Return true if the fields match:
        return a.DataType == b.DataType;
    }

    /// <summary>
    /// Overloads the C# "!=" operator so that two SqlDataType objects can be easily tested for inequality. 
    /// </summary>
    /// <param name="a">The first SqlDataType object to be compared for inequality against the second SqlDataType object</param>
    /// <param name="b">The second SqlDataType object to be compared for inequality against the first SqlDataType object</param>
    /// <returns>True if <paramref name="a"/> is not equal to <paramref name="b"/>, false if not</returns>
    public static bool operator !=(SqlDataType a, SqlDataType b)
    {
        return !(a == b);
    }

    /// <summary>
    /// Tests the <paramref name="obj"/> parameter for equality to this instance. If <paramref name="obj"/> 
    /// is not a SqlDataType object, it returns false. If this instance is equal to the <paramref name="obj"/> 
    /// instance it returns true.
    /// </summary>
    /// <param name="obj">A SqlDataType object to be compared for equality with this instance</param>
    /// <returns>True if <paramref name="obj"/> is equal to this instance, false if not</returns>
    public override bool Equals(System.Object obj)
    {
        // If parameter cannot be cast return false:
        if (!(obj is SqlDataType p))
            return false;

        // Return true if the fields match:
        return DataType == p.DataType;
    }

    /// <summary>
    /// Tests the <paramref name="p"/> parameter for equality to the this instance. If this instance is 
    /// equal to the <paramref name="p"/> parameter it returns true, otherwise it returns false.
    /// </summary>
    /// <param name="p">A SqlDataType object to be compared for equality with this instance</param>
    /// <returns>True if <paramref name="p"/> is equal to this instance, false if not</returns>
    public bool Equals(SqlDataType p)
    {
        // Return true if the fields match:
        return DataType == p.DataType;
    }

    /// <summary>
    /// Overrides <see cref="System.Object.GetHashCode"/> with custom implementation.
    /// </summary>
    /// <returns>The hash code as an integer</returns>
    public override int GetHashCode()
    {
        return base.GetHashCode() ^ DataType.GetHashCode();
    }

}
