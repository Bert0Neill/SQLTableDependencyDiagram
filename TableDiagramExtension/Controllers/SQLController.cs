using Microsoft.SqlServer.Management.Smo.RegSvrEnum;
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
using TableDiagramExtension.Models;
using TableDiagramExtension.Resources;

namespace TableDiagramExtension.Controllers
{
    internal class SQLController : ISQLController
    {
        ConvertController _convertController;

        public SQLController()
        {
            _convertController = new ConvertController();
        }

        public List<DatabaseMetaData> RetrieveDatabaseMetaData(string initialConnectionString, string activeDatabase)
        {
            try
            {
                List<DatabaseMetaData> metadata = new List<DatabaseMetaData>();
                string connectionString = initialConnectionString.Replace("Multiple Active Result Sets", "MultipleActiveResultSets").Replace("Trust Server Certificate", "TrustServerCertificate");
                string sql = String.Format(SqlStatements.SelectDatabaseTables, activeDatabase);
                DataTable results = new DataTable() ;
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, new SqlConnection(connectionString)))
                {
                    dataAdapter.Fill(results);
                }

                metadata = _convertController.ConvertDataTable<DatabaseMetaData>(results);

                return metadata;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<string> RetrieveDatabases(string initialConnectionString)
        {
            try
            {
                string connectionString = initialConnectionString.Replace("Multiple Active Result Sets", "MultipleActiveResultSets").Replace("Trust Server Certificate", "TrustServerCertificate");
                string sql = SqlStatements.RetrieveDatabases;
                DataTable results = new DataTable();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, new SqlConnection(connectionString)))
                {
                    dataAdapter.Fill(results);
                }

                return results.AsEnumerable().Select(x => x[0].ToString()).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
