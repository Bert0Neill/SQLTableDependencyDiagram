using Newtonsoft.Json.Linq;
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
using TableDiagramExtension.Interfaces;
using TableDiagramExtension.Resources;

namespace TableDiagramExtension.Controllers
{
    internal class SQLController : ISQLController
    {
        public SQLController()
        {
            
        }

        public DataTable RetrieveDatabaseMetaData(SharedData sharedData)
        {
            try
            {
                string connectionString = sharedData.SqlOlapConnectionInfoBase.ConnectionString.Replace("Multiple Active Result Sets", "MultipleActiveResultSets").Replace("Trust Server Certificate", "TrustServerCertificate");
                string sql = SqlStatements.SelectDatabaseTables;
                DataTable results = new DataTable() ;
                //using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, new SqlConnection(sharedData.SqlOlapConnectionInfoBase.ConnectionString)))
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, new SqlConnection(connectionString)))
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
