namespace KnightMoves.SqlObjects.SqlCode
{
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
}
