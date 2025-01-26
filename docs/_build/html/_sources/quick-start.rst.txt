`Documentation Home <https://docs.knightmovesolutions.com>`_

===========
Quick Start
===========

Install the NuGet package 

.. code-block:: powershell

   Install-Package KnightMoves.SqlObjects

Import the namespace

.. code-block:: csharp

   using KnightMoves.SqlObjects;


Use the Fluent API through the static ``TSQL`` class

.. code-block:: csharp

   var sql = TSQL

        .SELECT()
          .STAR()
        .FROM("Products")
        .Build()

   ;

   Console.WriteLine(sql);

Output:

.. code-block:: text

     SELECT
      *
     FROM [Products]

