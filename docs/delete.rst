`Documentation Home <https://docs.knightmovesolutions.com>`_

.. include:: northwind-db-tip.rst

======
DELETE
======

**Basic Delete**

.. code-block:: csharp

    var sql = TSQL

        .DELETE().FROM("Categories")
        .WHERE("CategoryID").IsEqualTo(9)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  DELETE FROM [Categories]
  WHERE 1=1
   AND [CategoryID] = 9


**Basic Delete with Table Schema**

.. code-block:: csharp

    var sql = TSQL

        .DELETE().FROM("dbo", "Categories")
        .WHERE("CategoryID").IsEqualTo(9)
        .Build()

    ;

    Console.WriteLine(sql);

Output:

.. code-block:: sql

  DELETE FROM [dbo].[Categories]
  WHERE 1=1
   AND [CategoryID] = 9

