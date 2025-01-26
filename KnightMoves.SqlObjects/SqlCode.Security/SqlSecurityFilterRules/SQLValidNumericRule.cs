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
using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// Checks values for valid numeric value
/// </summary>
public class SQLValidNumericRule : ISQLSecurityFilterRule
{
    string _errorMsg;

    /// <summary>
    /// If <typeparamref name="T"/> is 
    /// <see cref="byte"/>, <see cref="short"/>, <see cref="int"/>, <see cref="long"/>, <see cref="decimal"/>, <see cref="float"/>, or <see cref="double"/>
    /// it will check <paramref name="val"/> for a valid value of the specified type and return 0 if it is invalid. 
    /// If <typeparamref name="T"/> is not one of those numeric types then it will ignore
    /// the value and return it as-is.
    /// </summary>
    public string SanitizeInput<T>(string val)
    {
        return SanitizeInput(typeof(T), val);
    }

    /// <summary>
    /// If <typeparamref name="T"/> is 
    /// <see cref="byte"/>, <see cref="short"/>, <see cref="int"/>, <see cref="long"/>, <see cref="decimal"/>, <see cref="float"/>, or <see cref="double"/>
    /// it will check <paramref name="val"/> for a valid value of the specified type and return 0 if it is invalid. 
    /// If <typeparamref name="T"/> is not one of those numeric types then it will ignore
    /// the value and return it as-is.
    /// </summary>
    public string SanitizeInput(Type T, string val)
    {
        if (IsNumericType(T) && !ParseValue(T, val))
            val = "0";

        return val;
    }

    /// <summary>
    /// If <typeparamref name="T"/> is
    /// <see cref="byte"/>, <see cref="short"/>, <see cref="int"/>, <see cref="long"/>, <see cref="decimal"/>, <see cref="float"/>, or <see cref="double"/>
    /// it will return true if <paramref name="val"/> is a valid numeric value of the spcified type. If not it returns false with a collection 
    /// of error strings.
    /// </summary>
    public (bool, IEnumerable<string>) CheckInput<T>(string val)
    {
        return CheckInput(typeof(T), val);
    }

    /// <summary>
    /// If <typeparamref name="T"/> is
    /// <see cref="byte"/>, <see cref="short"/>, <see cref="int"/>, <see cref="long"/>, <see cref="decimal"/>, <see cref="float"/>, or <see cref="double"/>
    /// it will return true if <paramref name="val"/> is a valid numeric value of the spcified type. If not it returns false with a collection 
    /// of error strings.
    /// </summary>
    public (bool, IEnumerable<string>) CheckInput(Type T, string val)
    {
        bool okay = !IsNumericType(T) || ParseValue(T, val);

        return (okay, okay ? new List<string>() : new List<string> { _errorMsg } );
    }

    private bool IsNumericType(Type T) =>
        T == typeof(byte) ||
        T == typeof(short) ||
        T == typeof(int) ||
        T == typeof(long) ||
        T == typeof(decimal) ||
        T == typeof(float) ||
        T == typeof(double)
    ;

    private bool ParseValue(Type t, string val)
    {
        bool okay = true;

        if (t == typeof(byte) && byte.TryParse(val, out byte byteVar) == false)
        {
            _errorMsg = "Value not a valid System.Byte (byte). Value was: " + val;
            return false;
        }

        if (t == typeof(short) && short.TryParse(val, out short shortVar) == false)
        {
            _errorMsg = "Value not a valid System.Int16 (short). Value was: " + val;
            return false;
        }

        if (t == typeof(int) && int.TryParse(val, out int intVar) == false)
        {
            _errorMsg = "Value not a valid System.Int32 (int). Value was: " + val;
            return false;
        }

        if (t == typeof(long) && long.TryParse(val, out long int64Var) == false)
        {
            _errorMsg = "Value not a valid System.Int64 (long). Value was: " + val;
            return false;
        }

        if (t == typeof(decimal) && decimal.TryParse(val, out decimal decVar) == false)
        {
            _errorMsg = "Value not a valid System.Decimal (decimal). Value was: " + val;
            return false;
        }

        if (t == typeof(float) && float.TryParse(val, out float singleVar) == false)
        {
            _errorMsg = "Value not a valid System.Single (float). Value was: " + val;
            return false;
        }

        if (t == typeof(double) && double.TryParse(val, out double dblVar) == false)
        {
            _errorMsg = "Value not a valid System.Double (double). Value was: " + val;
            return false;
        }

        return okay;
    }
}
