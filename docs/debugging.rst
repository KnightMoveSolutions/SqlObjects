`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

=========
Debugging
=========

One of the goals of this library is a pleasant debugging experience. This is accomplished in two ways.

1. Indented "pretty print" SQL strings

   As shown throughout this documentation and if you've run the library yourself, it prints the SQL 
   statements with indentation for an easy debugging experience when you need to look at the rendered SQL.

2. Parameter value rendering 

   If you follow best practices you are undoubtedly utilizing :doc:`parameterized-queries`. As such, when 
   you render the SQL, what you will find is that the variables themselves are printed, which is necessary 
   and by design. However, if you need to troubleshoot the SQL statement with the actual values then it is
   extremely cumbersome to do manually. This is resolved with a ``.Build(object parameters)`` overload 
   documented below.

**Render SQL with Parameter Values**

Consider the following query that specifies a number of parameter variables.

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("Employees")
        .WHERE("ReportsTo").IsEqualTo("@SupervisorId")
          .AND().COLUMN("BirthDate").BETWEEN("@StartDate").AND("@EndDate")
          .AND().COLUMN("Region").IsEqualTo("@Region")
          .AND().COLUMN("PostalCode").IN("@PostalCodes")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [Employees]
  WHERE 1=1
   AND [ReportsTo] = @SupervisorId
   AND [BirthDate] BETWEEN @StartDate AND @EndDate
   AND [Region] = @Region
   AND [PostalCode] IN @PostalCodes
   
:doc:`where-1-equals-1`

First notice that the ``IN`` statement renders with a Dapper compatible parameter specification by 
omitting the parentheses in the syntax of an ``IN`` statement.

Now, if you wanted to debug that statement with the values in the parameter variables at runtime, 
you could do the following. 

.. code-block:: csharp 

    var paramObj = new
    {
        SupervisorId = 2,
        StartDate = new DateTime(1930,1,1),
        EndDate = new DateTime(1970,12,31),
        Region = "WA",
        PostalCodes = new List<string> { "98122", "98105" },
        SeattleOnly = true
    };

    var sql = TSQL

        .SELECT()
            .STAR()
        .FROM("Employees")
        .WHERE("ReportsTo").IsEqualTo("@SupervisorId")
          .AND().COLUMN("BirthDate").BETWEEN("@StartDate").AND("@EndDate")
          .AND().COLUMN("Region").IsEqualTo("@Region")
          .AND().COLUMN("PostalCode").IN("@PostalCodes")
          .AND()
            .CASE("[City]")
              .WHEN("Seattle").THEN(true)
              .ELSE(false)
          .END().IsEqualTo("@SeattleOnly")
        .Build(paramObj)

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [Employees]
  WHERE 1=1
   AND [ReportsTo] = 2
   AND [BirthDate] BETWEEN '1930-01-01 00:00:00' AND '1970-12-31 00:00:00'
   AND [Region] = 'WA'
   AND [PostalCode] IN ('98122','98105')
   AND CASE [City]
     WHEN 'Seattle' THEN 1
     ELSE 0
    END = 1

:doc:`where-1-equals-1`

As you can see, the values are properly interpolated into the SQL statement. Data types that 
should be quoted are quoted, data types that should not be quoted are not, and the collection 
of values fed to the ``IN`` statement are properly formated with parentheses as well. 

This accommodates runtime inspection of a query with values rendered by calling the ``.BUILD(object parameters)``
overload. 

.. warning::
   Keep in mind that you should use this feature for debugging purposes only since submitting SQL 
   statements to the database with values directly interpolated into the SQL string defeats the 
   purpose of parameterized queries. 

