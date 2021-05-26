using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace SQLProject
{
    class SqlHelper
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;        
        private SqlConnection _sqlConnection;

        public SqlHelper (string dbName)
        {
            _sqlConnectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = "localhost",
                InitialCatalog = dbName,
                IntegratedSecurity = true
            };
        }

        public void OpenConnection()
        {
            _sqlConnection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            _sqlConnection.Open();
        }

        public void CloseConnection() => _sqlConnection.Close();
              
        public void ExecuteNonQuery(string request)
        {
            var command = new SqlCommand(request, _sqlConnection);
            command.ExecuteNonQuery();
        }

        public void Insert(string table, Dictionary<string, string> parameters)
        {
            var columns = string.Empty;
            var values = string.Empty;

            foreach (var (key, value) in parameters)
            {
                columns += $"{key},";
                values += $"{value},";
            }

            var command = new SqlCommand($"insert into {table} ({columns.TrimEnd(',')}) values ({values.TrimEnd(',')})",
               _sqlConnection);
            command.ExecuteNonQuery();
        }

        public void Delete (string table, Dictionary<string, string> parameters)
        {
            var whereConditions = string.Empty;

            foreach (var (key, value) in parameters)
                whereConditions += $" and {key}={value}";           

            var command = new SqlCommand($"delete from {table} where {whereConditions.Substring(5)}",
               _sqlConnection);            
            command.ExecuteNonQuery();   
        }

        public void Update (string table, Dictionary<string, string> parameters, Dictionary<string, string> condition)
        {
            var whereConditions = string.Empty;

            foreach (var (key, value) in condition)
                whereConditions += $" and {key}={value}"; 

             var whereParameters = string.Empty;

            foreach (var (column, value) in parameters)
                whereParameters += $", {column}={value}"; 
            

            var command = new SqlCommand($"update {table} set {whereParameters[2..]} where {whereConditions.Substring(5)}",
               _sqlConnection);            
            command.ExecuteNonQuery();   
        }

        public bool IsRowExistedInTable(string table, Dictionary<string, string> parameters)
        {
            var whereParameters = string.Empty;

            foreach (var (key, value) in parameters)
                whereParameters += $" and {key}={value}";

            var sqlDataAdapter = new SqlDataAdapter(
                $"select * from {table} where {whereParameters.Substring(5)}",
                _sqlConnection);
            var dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            return dataTable.Rows.Count > 0;
        }
    }
}
