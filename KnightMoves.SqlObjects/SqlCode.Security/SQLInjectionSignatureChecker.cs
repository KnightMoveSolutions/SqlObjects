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

using System.Collections.Generic;
using System.Linq;

namespace KnightMoves.SqlObjects.SqlCode.Security;

/// <summary>
/// <para>
/// This is the default implementation of the <see cref="KnightMoves.SqlObjects.SqlCode.Security.ISQLInjectionSignatureChecker"/> 
/// interface. This class is the entry point of a list of <see cref="KnightMoves.SqlObjects.SqlCode.Security.ISQLInjectionSignatureRule"/> 
/// objects implemented in the form of a "Chain of Responsibility" design pattern. 
/// </para>
/// </summary>
public class SQLInjectionSignatureChecker : ISQLInjectionSignatureChecker
{
    protected IList<ISQLInjectionSignatureRule> _sqlInjectionSignatureRules;

    /// <summary>
    /// A collection of messages describing which of the <see cref="KnightMoves.SqlObjects.SqlCode.Security.ISQLInjectionSignatureRule"/> 
    /// rules detected a SQL injection signature in the value <paramref name="val"/>.
    /// </summary>
    public IList<string> SignaturesDetected { get; private set; } = new List<string>();

    /// <summary>
    /// The constructor requires a collection of <see cref="KnightMoves.SqlObjects.SqlCode.Security.ISQLInjectionSignatureRule"/>  
    /// objects that will perform the SQL Injection checks
    /// </summary>
    /// <param name="sqlInjectionSignatureRules"></param>
    public SQLInjectionSignatureChecker(IList<ISQLInjectionSignatureRule> sqlInjectionSignatureRules)
    {
        _sqlInjectionSignatureRules = sqlInjectionSignatureRules;
    }

    /// <summary>
    /// If <paramref name="val"/> is found to contain a SQL injection signature, then it will return true and 
    /// a collection of strings describing the SQL injection signature(s) found. Otherwise, it will return 
    /// false and the collection of strings will be empty.
    /// </summary>
    public (bool, IEnumerable<string>) ContainsSqlInjection(string val)
    {
        var found = false;
        var signaturesFound = new List<string>();
        char[] valArray = val.ToCharArray();
        bool stringTerminated = true;
        int quotePosition = 0;

        string suspect;

        // Here we loop through each character until we find ourselves in an 
        // unterminated area of the string.  
        for (int i = 0; i < valArray.Length; i++)
        {
            if (valArray[i].ToString() == "'")
            {
                // Flip the flag.
                stringTerminated = !stringTerminated;

                if (stringTerminated == false)
                {
                    // Mark the starting position of the 
                    // vulnerable section of the string
                    quotePosition = i;
                }
                else
                {
                    if (i > 0 && i - quotePosition > 0)
                    {
                        // Okay, here we've come across a section of the string 
                        // that is vulnerable, so we grab it.
                        suspect = val.Substring(quotePosition + 1, i - quotePosition - 1);

                        _sqlInjectionSignatureRules
                            .ToList()
                            .ForEach(rule =>
                            {
                                if (rule.ContainsSqlInjection(suspect))
                                {
                                    found |= true;
                                    signaturesFound.Add(rule.ErrorMessage);
                                }
                            });
                    }
                }
            }
            else
            {
                if (i + 1 == valArray.Length && stringTerminated == false)
                {
                    // We've reached the end of the string and it has not been terminated.
                    // So, the section of the string from the last quote to the end
                    // is vulnerable so we grab it. 
                    suspect = val.Substring(quotePosition + 1, i - quotePosition);

                    _sqlInjectionSignatureRules
                        .ToList()
                        .ForEach(rule =>
                        {
                            if (rule.ContainsSqlInjection(suspect))
                            {
                                found |= true;
                                signaturesFound.Add(rule.ErrorMessage);
                            }
                        });
                }
            }
        }

        return (found, signaturesFound);
    }
}
