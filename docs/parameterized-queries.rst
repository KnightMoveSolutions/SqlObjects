`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

=====================
Parameterized Queries
=====================

The SqlObjects library supports of the crafting of parameterized queries by specifying variables in the 
``@paramVar`` format using the ``@`` prefix. When setting values to a string in the ``@paramVar`` format 
then it will be printed in the output without any quotes as shown below, after which the resulting query
can be passed to a Dapper method or any other ORM that supports parameterized queries. 

.. code-block:: csharp 

    var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("Products")
        .WHERE("ProductId").IsEqualTo("@ProductId")
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  SELECT
   *
  FROM [Products]
  WHERE 1=1
   AND [ProductId] = @productId
   
:doc:`where-1-equals-1` 

Then somewhere using Dapper ORM for example:

.. code-block:: csharp 

   var paramObj = new { ProductId = 123 };

   var product = connection.Query<Product>(sql, paramObj);

You can specify ``@paramVar`` variables anywhere in your query where it makes sense and it will render the variable accordingly.

