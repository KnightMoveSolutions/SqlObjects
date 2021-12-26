﻿using System;
using System.Collections.Generic;

namespace KnightMoves.SqlObjects.SqlCode.Security
{
    /// <summary>
    /// Checks strings for SQL Injection
    /// </summary>
    public class SQLValidStringRule : ISQLSecurityFilterRule
    {
        ISQLInjectionSignatureChecker _sqlInjectionChecker;

        public SQLValidStringRule(ISQLInjectionSignatureChecker sqlInjectionChecker)
        {
            _sqlInjectionChecker = sqlInjectionChecker;
        }

        /// <summary>
        /// If <typeparamref name="T"/> is <see cref="string"/> it will ensure that all single quotes are escaped
        /// and return the result. If <typeparamref name="T"/> is not <see cref="string"/> then it will ignore
        /// the value and return it as-is.
        /// </summary>
        public string SanitizeInput<T>(string val)
        {
            return SanitizeInput(typeof(T), val);
        }

        /// <summary>
        /// If <typeparamref name="T"/> is <see cref="string"/> it will ensure that all single quotes are escaped
        /// and return the result. If <typeparamref name="T"/> is not <see cref="string"/> then it will ignore
        /// the value and return it as-is.
        /// </summary>
        public string SanitizeInput(Type T, string val)
        {
            if (T != typeof(string))
                return val;

            // Here we double-up on any single quotes in the string. This will take care of 
            // the O'Tooles and the O'Ghouls of the population.  This also guarantees that 
            // there is no unterminated single-quote, which should block SQL injection.
            return val.Replace("''", "'").Replace("'", "''");
        }

        public (bool, IEnumerable<string>) CheckInput<T>(string val)
        {
            return CheckInput(typeof(T), val);
        }

        /// <summary>
        /// If <typeparamref name="T"/> is <see cref="string"/> it will return false if <paramref name="val"/> is 
        /// found to have any SQL Injection signatures in it with the signature disclosed in the returned string collection. 
        /// This is done by using an instance of <see cref="ISQLInjectionSignatureChecker"/>. The default implementation is 
        /// <see cref="SQLInjectionSignatureChecker"/>, which cycles through the collection of <see cref="ISQLInjectionSignatureRule"/> 
        /// implementations. If no SQL injection signature are detected it will return true with an empty collection of error strings. 
        /// If <typeparamref name="T"/> is not <see cref="string"/> then it returns (false, null).
        /// </summary>
        public (bool, IEnumerable<string>) CheckInput(Type T, string val)
        {
            if (T != typeof(string))
                return (false, null);

            (bool injectionDetected, IEnumerable<string> sigsDetected) = _sqlInjectionChecker.ContainsSqlInjection(val);

            return (!injectionDetected, sigsDetected);
        }
    }
}
