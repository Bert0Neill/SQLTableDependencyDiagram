using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TableDiagramExtension.Classes;
using TableDiagramExtension.Resources;

namespace TableDiagramExtension.Controllers
{
    internal class SQLController
    {
        public SQLController()
        {

        }

        public DataTable RetrieveDatabaseMetaData(SharedData sharedData)
        {
            try
            {
                string sql = SqlStatements.SelectDatabaseTables;
                DataTable results = null;
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, new SqlConnection(sharedData.SqlOlapConnectionInfoBase.ConnectionString)))
                {                    
                    dataAdapter.Fill(results);
                }

                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
