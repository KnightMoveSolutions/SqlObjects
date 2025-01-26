﻿/*
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

using System;

namespace KnightMoves.SqlObjects.SqlCode;

public abstract partial class SqlStatement : IFluentSqlInsert
{
    public abstract SqlStatement INSERT();
    public abstract SqlStatement INTO(string table);
    public abstract SqlStatement INTO(string schema, string table);
    public abstract SqlStatement VALUES();
    public abstract SqlStatement VALUE(string value);
    public abstract SqlStatement VALUE(int value);
    public abstract SqlStatement VALUE(DateTime value);
    public abstract SqlStatement VALUE(bool value);
    public abstract SqlStatement VALUE(long value);
    public abstract SqlStatement VALUE(Guid value);
    public abstract SqlStatement VALUE(char value);
    public abstract SqlStatement VALUE(decimal value);

}
