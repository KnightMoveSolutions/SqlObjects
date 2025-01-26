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

using KnightMoves.Hierarchical;
using Newtonsoft.Json;
using System;
using System.Data;


namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlParameter"/> 
/// and implements the <see cref="ISqlQueryExpression.DataType"/> property. 
/// Other than it taking on the functionality of the <see cref="ISqlParameter"/> 
/// abstract class, this class serves as a marker data type.
/// </summary>
public class TSQLParameter : TSQLStatement, ISqlParameter
{
    /// <summary>
    /// The alias of the SQL expression so it can be referenced easily in other SQL fragments.
    /// </summary>
    public string Alias { get; set; }
    
    /// <summary>
    /// The return data type of the calculation
    /// </summary>
    public SqlDataType DataType { get; set; }
    
    /// <summary>
    /// The name of the parameter
    /// </summary>
    public string ParameterName { get; set; }

    /// <summary>
    /// A description of the parameter
    /// </summary>
    public string ParameterDescription { get; set; }

    /// <summary>
    /// A <see cref="SqlDataType"/> object that represents the data type of the parameter
    /// </summary>
    public SqlDataType ParameterDataType { get; set; }

    /// <summary>
    /// True if the parameter's data type is non-specific...aka a "Variant", false if not
    /// </summary>
    public bool IsVariant { get; set; }

    /// <summary>
    /// The default value of the parameter
    /// </summary>
    public string DefaultValue { get; set; }

    /// <summary>
    /// The sort order that the parameters would appear in the function call. This is so the parameters 
    /// can be ordinally mapped to the actual SQL function in the database.
    /// </summary>
    public int Order { get; set; }

    private string _enumType;

    /// <summary>
    /// Specifies the name of an enum if this parameter's value must be one of the enum values. This 
    /// effectively causes validation upon setting the value of the <see cref="Value"/> property 
    /// ensuring that <see cref="Value"/> is one of the enum values.
    /// </summary>
    public string EnumType
    {
        get { return _enumType; }
        set
        {
            if (value != null)
            {
                Type enumType = Type.GetType(value);

                if (enumType == null || enumType.IsEnum == false)
                    throw new ArgumentException("You can only use the Type of an enum for the EnumType property of the SqlParameter class. '" + value + "' is not a valid enum type. Use 'typeof(enumType).AssemblyQualifiedName'.");
            }

            _enumType = value;
        }
    }

    private ISqlQueryExpression _value;

    /// <summary>
    /// The value of the parameter. If <see cref="EnumType"/> is specified, then the value must be 
    /// able to be parsed into one of that enum types values. If not, it will throw an <see cref="InvalidOperationException"/> 
    /// </summary>
    [JsonConverter(typeof(TreeNodeJsonConverter))]
    public ISqlQueryExpression Value
    {
        get { return _value; }

        set
        {
            if (value == null)
            {
                _value = value;
                return;
            }

            if (ParameterDataType == null)
                throw new InvalidOperationException("Cannot set the Parameter.Value because the ParameterDataType property must be set first.");

            if (IsVariant == false && value.DataType.DataType != SqlDbType.Variant.ToString() && value.DataType != ParameterDataType)
                throw new InvalidOperationException("Cannot set a Value with DataType '" + value.DataType.DataType + "' for a Parameter whose ParameterDataType is '" + ParameterDataType.DataType + "'. The data types must match.");

            // Enforce a valid enum value if an EnumType has been specified
            if (EnumType != null && EnumType != string.Empty)
            {
                // First we make sure the SqlQueryExpression object being set is a type of SqlLiteral object. If not, throw an exception.
                if ((value is ISqlLiteral) == false)
                    throw new InvalidOperationException("This parameter must be a value from the enumeration '" + EnumType + "' in the form of a SqlLiteral object with its Value property set to a valid option of the '" + EnumType + "' enumeration.");

                // Now that we know we're dealing with a SqlLiteral object, we make sure that 
                // its Value property is one of the valid enum options.  If not, throw an exception.
                Type enumType = Type.GetType(EnumType);

                try
                {
                    object o = Enum.Parse(enumType, ((ISqlLiteral)value).Value, true);
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("The value '" + ((ISqlLiteral)value).Value + "' is not a valid option from the '" + EnumType + "' enum.", e);
                }
            }

            _value = value;
        }
    }

    /// <summary>
    /// Returns the value of the parameter.
    /// </summary>
    public override string SQL()
    {
        return Value == null ? DefaultValue : Value.ToString();
    }
}
