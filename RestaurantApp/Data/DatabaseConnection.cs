using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace RestaurantApp.Data
{
    /// <summary>
    /// Maneja la conexión a la base de datos SQL Server
    /// </summary>
    public class DatabaseConnection
    {
        private static string _connectionString;

        public static void Initialize()
        {
            try
            {
                _connectionString = ConfigurationManager.ConnectionStrings["RestaurantDB"]?.ConnectionString;
                if (string.IsNullOrEmpty(_connectionString))
                {
                    throw new Exception("Cadena de conexión 'RestaurantDB' no encontrada en App.config");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar la conexión a la base de datos: " + ex.Message);
            }
        }

        public static SqlConnection GetConnection()
        {
            if (string.IsNullOrEmpty(_connectionString))
            {
                Initialize();
            }
            return new SqlConnection(_connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static SqlDataAdapter GetDataAdapter(string query)
        {
            return new SqlDataAdapter(query, GetConnection());
        }
    }
}
