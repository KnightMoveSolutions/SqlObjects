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

namespace KnightMoves.SqlObjects.SqlCode;

public static class StringExtensions
{
    /// <summary>
    /// Identifies strings in parameter name format using the @ symbol prefix (e.g. @someParam)
    /// </summary>
    public static bool IsParameter(this string val) => val != null && val.StartsWith('@');

    /// <summary>
    /// Returns true if the value is in SQL delimited format using square brackets (e.g. [SomeValue] )
    /// </summary>
    public static bool IsDelimited(this string val) =>
        // Ensure val is not null or empty 
        !string.IsNullOrEmpty(val) &&

        // Ensure standard format --> [something]
        val.StartsWith('[') && val.EndsWith(']') &&

        // Ensure no terminating end bracket is unescaped --> Okay: [something]]anotherthing] Bad: [something]anotherthing]
        val[1..^1].Replace("]]", string.Empty).Contains("]") == false
    ;

    /// <summary>
    /// Ensures that all terminating brackets are escaped
    /// </summary>
    public static string SanitizeDelimitedValue(this string val)
    {
        if (val == null) 
            return null;

        if (val.StartsWith('[') && val.EndsWith(']')) 
            return $"[{val[1..^1].Replace("]]", "]").Replace("]", "]]")}]";

        return val.Replace("]]", "]").Replace("]", "]]");
    }
}
