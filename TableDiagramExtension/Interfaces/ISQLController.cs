using System.Data;
using TableDiagramExtension.Classes;

namespace TableDiagramExtension.Interfaces
{
    internal interface ISQLController
    {
        DataTable RetrieveDatabaseMetaData(SharedData sharedData);
    }
}