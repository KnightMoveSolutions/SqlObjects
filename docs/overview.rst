`Documentation Home <https://docs.knightmovesolutions.com>`_

.. image:: _images/overview-demo-animation.gif
   :width: 800

========
Overview
========

What is it?
-----------

KnightMoves.SqlObjects is a .NET library that wraps the SQL language for the purpose of embedding queries 
in .NET applications to be submitted to the database for execution. 

What problem does it solve?
---------------------------

This library provides an object-based solution that replaces embedded concatenated SQL queries strings. 
Using this library provides the following benefits.

- Fluent API with syntax that matches the SQL language keywords (e.g. ``.SELECT()``, ``.INSERT()``, etc.)
- Intellisense in the .NET IDE 
- Support for parameterized queries
- Debug easier with the ability to see the resulting SQL with parameters replaced by the provided values
- Resulting SQL is always formatted with indentation for easier testing / debugging
- Use in conjunction with ORMs like ``Dapper``
- Serialize the SQL object model to JSON and deserialize the JSON back into the SQL object model 
- Support for complex queries that include things like

  - LIKE
  - BETWEEN 
  - IN (...)
  - CASE statements
  - Calculations
  - Subqueries
  - JOINS 
  - UNION 

- Strongly-typed Fluent API methods that automatically quote and translate values (e.g. bool value false to 0)
- String input for values are protected against SQL injection in case values are provided before submitting to the ORM 
