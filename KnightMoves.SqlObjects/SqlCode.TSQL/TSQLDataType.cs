using System;
using System.Data;

namespace KnightMoves.SqlObjects.SqlCode.TSQL
{
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
}