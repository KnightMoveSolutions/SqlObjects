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

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// Implements the <see cref="SqlDataType"/> abstract class. 
/// </summary>
public class TSQLDataType : SqlDataType
{
    private SqlDbType _dataType;

    /// <summary>
    /// This constructor uses the <see cref="SqlDbType"/> enum as the basis for data types. 
    /// The <see cref="DataType"/> property must be one of those enum values.
    /// </summary>
    public TSQLDataType(SqlDbType dataType)
    {
        _dataType = dataType;
    }

    /// <summary>
    /// The data type, which is a stringified version of one of the <see cref="SqlDbType"/> 
    /// enum values.
    /// </summary>
    public override string DataType
    {
        get { return _dataType.ToString(); }

        set
        {
            if (!Enum.TryParse<SqlDbType>(value, out _dataType))
                throw new Exception("The data type must be a valid string representation of a SqlDbType enumeration value.");
        }
    }
}