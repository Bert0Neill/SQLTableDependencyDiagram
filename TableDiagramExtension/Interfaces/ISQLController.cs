using System.Collections.Generic;
using System.Data;
using TableDiagramExtension.Classes;
using TableDiagramExtension.Models;

namespace TableDiagramExtension.Interfaces
{
    internal interface ISQLController
    {
        List<DatabaseMetaData> RetrieveDatabaseMetaData(string initialConnectionString, string activeDatabase);
        List<string> RetrieveDatabases(string initialConnectionString);
    }
}