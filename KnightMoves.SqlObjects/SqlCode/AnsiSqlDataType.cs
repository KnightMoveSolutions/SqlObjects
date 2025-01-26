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
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode;

/// <summary>
/// This class implements the <see cref="KnightMoves.SqlObjects.SqlCode.SqlDataType"/> abstract class 
/// using the <see cref="System.Data.DbType"/> enum as the list of SQL Data Types to be used for the 
/// <see cref="DataType"/> abstract property.
/// </summary>
public class AnsiSqlDataType : SqlDataType
{
    private DbType _dataType;

    /// <summary>
    /// Sets the <see cref="System.Data.DbType"/> enum value ToString() as the <see cref="KnightMoves.SqlObjects.SqlCode.SqlDataType.DataType"/> property.
    /// </summary>
    /// <param name="dataType">The <see cref="System.Data.DbType"/> enum value that will be converted to a string to be returned as the <see cref="KnightMoves.SqlObjects.SqlCode.SqlDataType.DataType"/> property</param>
    public AnsiSqlDataType(DbType dataType)
    {
        _dataType = dataType;
    }

    /// <summary>
    /// Returns the <see cref="System.Data.DbType"/> enum value as a string.
    /// </summary>
    public override string DataType
    {
        get { return _dataType.ToString(); }

        set
        {
            if (!Enum.TryParse(value, out _dataType))
                throw new Exception("The data type must be a valid string representation of a DbType enumeration value.");
        }
    }
}
