=======
Support
=======

Not every combination of SQL statements that you put together will work, so before concluding that something is 
wrong please examine the following. 

- SQL syntax validation is not supported by this library. If you are able to build any SQL output it is not 
  guaranteed to be valid SQL. Make sure the SQL you are trying to build is valid against the target database first.

- While most of what can be considered basic, widely-supported SQL is implemented, this library targets Microsoft 
  SQL Server with T-SQL syntax. The majority of what you can build with this library will work with MySQL, Oracle, 
  and possibly other SQL databases as well but it is not guaranteed and no claims are made here that things will 
  work against anything other than SQL Server.

If you are able to successfully execute your target SQL statement in SQL Server but you are not able to build the 
equivalent SQL statement with this library, then please submit the following information as an issue in the GitHub 
repository.

- **Required:** Provide the working target SQL output that you are able to run against your database 
- **Required:** Provide an example of the KnightMoves.SqlObjects code that reproduces the error
- *Optional (recommended):* Make your SQL statement generic. Try not to disclose internal column names, values, etc.
- *Optional:* Provide CREATE scripts that setup your tables, relationships, keys, etc. that can be used to test your SQL.
- *Optional:* If possible, provide SQL and C# code that works against the Microsoft Northwind Database.

Once you provide the information above, it will be evaluated in the following way. 

- If it is something that is supposed to be supported by this library as documented but does not (i.e. not working as advertised)
  then all effort will be made to ensure the bug is fixed as soon as possible.

- If it is something that is not supported by this library and no claims have been made that it is supported, then it 
  will be treated as an enhancement and will take lower priority than bugs. However, the general attitude is to try 
  really, really hard to give you what you want. It just cannot be promised. 

With that said, your patience and understanding is extremely appreciated with high hopes that this library will be 
refined, enhanced, and improved with your feedback over time. 

Enjoy!

