using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TIQRI.ITS.Web.Helpers
{
    [Obsolete("There should not be direct SQL execution from the Web project is should be from the repository level")]
    public static class SqlHelper
    {
        [Obsolete("There should not be direct SQL execution from the Web project is should be from the repository level")]
        public static DataTable ExecuteStatement(string query)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["HRDBConnection"].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return dataTable;
        }

        [Obsolete("There should not be direct SQL execution from the Web project is should be from the repository level")]
        public static DataTable ExecuteStatement(string query, string connectionName)
        {
            var connectionStringName = string.IsNullOrEmpty(connectionName) ? "DefaultConnection" : connectionName;
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            DataTable dataTable = new DataTable();
            try
            {
                connection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
                sqlDataAdapter.Fill(dataTable);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return dataTable;
        }

        [Obsolete("There should not be direct SQL execution from the Web project is should be from the repository level")]
        public static int ExecuteUpdate(string query)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            SqlConnection connection = new SqlConnection(connectionString);
            int recordCount = 0;
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                recordCount = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return recordCount;
        }
    }
}