using Microsoft.SqlServer.Management.Smo;
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
using static System.Net.Mime.MediaTypeNames;

namespace TableDiagramExtension.Controllers
{
    internal class SQLController : ISQLController
    {
        ConvertController _convertController;
        ErrorController _errorController;

        public SQLController()
        {
            _convertController = new ConvertController();
            _errorController = new ErrorController();
        }

        public List<DatabaseMetaData> RetrieveDatabaseMetaData(string initialConnectionString, string activeDatabase)
        {
            try
            {
                List<DatabaseMetaData> metadata = new List<DatabaseMetaData>();
                string connectionString = initialConnectionString.Replace(SqlStatements.Multiple_Active_Result_Sets, SqlStatements.MultipleActiveResultSets).Replace(SqlStatements.Trust_Server_Certificate, SqlStatements.TrustServerCertificate);
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
                _errorController.DisplayErrorMessage(ex.Message);
                return Enumerable.Empty<DatabaseMetaData>().ToList();
            }
        }

        public List<string> RetrieveDatabases(string initialConnectionString)
        {
            try
            {
                string connectionString = initialConnectionString.Replace(SqlStatements.Multiple_Active_Result_Sets, SqlStatements.MultipleActiveResultSets).Replace(SqlStatements.Trust_Server_Certificate, SqlStatements.TrustServerCertificate);
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
                _errorController.DisplayErrorMessage(ex.Message);
                return Enumerable.Empty<string>().ToList();
            }
        }

        public List<string> RetrieveDependencyTables(string connectionString, string selectedDatabase, string selectedTable)
        {
            try
            {
                List<string> metadata = new List<string>();

                connectionString = connectionString.Replace(SqlStatements.Multiple_Active_Result_Sets, SqlStatements.MultipleActiveResultSets).Replace(SqlStatements.Trust_Server_Certificate, SqlStatements.TrustServerCertificate);
                string sql = String.Format(SqlStatements.SelectDependencyTableName, selectedDatabase, selectedTable);
                
                DataTable results = new DataTable();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, new SqlConnection(connectionString)))
                {
                    dataAdapter.Fill(results);
                }

                return results.AsEnumerable().Select(n => n.Field<string>(0)).ToList();
            }
            catch (Exception ex)
            {
                _errorController.DisplayErrorMessage(ex.Message);
                return Enumerable.Empty<string>().ToList();
            }
        }

        public string RetrieveDependencyTablesMetaData(string initialConnectionString, string activeDatabase, List<string> dependencyTables)
        {
            try
            {
                string delimTables = "'" + String.Join("','", dependencyTables) + "'";

                string connectionString = initialConnectionString.Replace(SqlStatements.Multiple_Active_Result_Sets, SqlStatements.MultipleActiveResultSets).Replace(SqlStatements.Trust_Server_Certificate, SqlStatements.TrustServerCertificate);
                string sql = String.Format(SqlStatements.SelectDependencyTableInfo, activeDatabase, delimTables);

                DataTable results = new DataTable();
                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, new SqlConnection(connectionString)))
                {
                    dataAdapter.Fill(results);
                }

                return results.Rows[0][0].ToString(); // return XML                
            }
            catch (Exception ex)
            {
                _errorController.DisplayErrorMessage(ex.Message);
                return string.Empty;
            }
        }
    }
}
