using System.Linq;
using System.Text;

namespace KnightMoves.SqlObjects.SqlCode.TSQL;

/// <summary>
/// This class implements <see cref="ISqlScript"/> and serves as a container 
/// for other SQL objects particularly as the Root object in the Fluent API.
/// It can also serve as a container for arbitrary SQL code.
/// </summary>
public class TSQLScript : TSQLStatement, ISqlScript
{
    /// <summary>
    /// Outputs the resulting SQL of any SQL objects added to it
    /// </summary>
    public override string SQL()
    {
        var sb = new StringBuilder();

        Children.ToList().ForEach(child => sb.Append(child));

        return sb.ToString();
    }
}
