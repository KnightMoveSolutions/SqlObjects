`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

======
HAVING
======

**Basic Syntax**

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .COLUMN("c", "CategoryName")
          .COUNT("p", "ProductID").AS("TotalProducts")
          .AVG("p", "UnitPrice").AS("AvgPrice")
        .FROM("dbo", "Products", "p")
        .INNERJOIN("dbo", "Categories", "c").ON("c", "CategoryID").IsEqualTo("p", "CategoryID")
        .GROUPBY()
        .HAVING()
          .AVG("p", "UnitPrice").IsGreaterThan(25.00m)
       .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   [c].[CategoryName],
   COUNT([p].[ProductID]) AS [TotalProducts],
   AVG([p].[UnitPrice]) AS [AvgPrice]
  FROM [dbo].[Products] p
  INNER JOIN [dbo].[Categories] c ON [c].[CategoryID] = [p].[CategoryID]
  GROUP BY
   [c].[CategoryName]
  HAVING 1=1
   AND AVG([p].[UnitPrice]) > 25.00

:doc:`HAVING 1=1 ?<where-1-equals-1>`

**Conditions**

Conditions in a ``HAVING`` clause are the same as those in a ``WHERE`` clause. You can find the syntax for those 
concerns by clicking on the links below. 

- :ref:`comparison-operators`
- :ref:`and-logical-operator`
- :ref:`or-logical-operator`
- :ref:`between-condition`
- :ref:`in-clause`
- :ref:`like-condition`
