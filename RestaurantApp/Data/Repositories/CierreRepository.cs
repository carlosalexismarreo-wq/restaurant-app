using System;
using System.Data;
using System.Data.SqlClient;
using RestaurantApp.Models;
using System.Collections.Generic;

namespace RestaurantApp.Data.Repositories
{
    /// <summary>
    /// Repositorio para gestionar operaciones con cierres de caja
    /// </summary>
    public class CierreRepository
    {
        private const string TABLE_NAME = "CierresCaja";

        public bool CrearCierre(CierreCaja cierre)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"INSERT INTO {TABLE_NAME}
                                    (CierreID, NumeroCierre, FechaCierre, UsuarioID, SaldoInicial,
                                     TotalVentas, TotalEgresos, SaldoFinal, Diferencia, Observaciones)
                                    VALUES (@CierreID, @NumeroCierre, @FechaCierre, @UsuarioID, @SaldoInicial,
                                            @TotalVentas, @TotalEgresos, @SaldoFinal, @Diferencia, @Observaciones)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CierreID", cierre.CierreID);
                        cmd.Parameters.AddWithValue("@NumeroCierre", cierre.NumeroCierre);
                        cmd.Parameters.AddWithValue("@FechaCierre", cierre.FechaCierre);
                        cmd.Parameters.AddWithValue("@UsuarioID", cierre.UsuarioID);
                        cmd.Parameters.AddWithValue("@SaldoInicial", cierre.SaldoInicial);
                        cmd.Parameters.AddWithValue("@TotalVentas", cierre.TotalVentas);
                        cmd.Parameters.AddWithValue("@TotalEgresos", cierre.TotalEgresos);
                        cmd.Parameters.AddWithValue("@SaldoFinal", cierre.SaldoFinal);
                        cmd.Parameters.AddWithValue("@Diferencia", cierre.Diferencia);
                        cmd.Parameters.AddWithValue("@Observaciones", cierre.Observaciones ?? (object)DBNull.Value);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear cierre de caja: " + ex.Message);
            }
        }

        public CierreCaja ObtenerCierrePorID(string cierreID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE CierreID = @CierreID";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CierreID", cierreID);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapearCierre(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cierre: " + ex.Message);
            }
        }

        public List<CierreCaja> ObtenerCierresPorFecha(DateTime fecha)
        {
            List<CierreCaja> cierres = new List<CierreCaja>();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE CAST(FechaCierre AS DATE) = @Fecha ORDER BY FechaCierre DESC";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cierres.Add(MapearCierre(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener cierres: " + ex.Message);
            }
            return cierres;
        }

        public int ObtenerProximoNumeroCierre()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT ISNULL(MAX(NumeroCierre), 0) + 1 FROM {TABLE_NAME}";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        return (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener próximo número de cierre: " + ex.Message);
            }
        }

        private CierreCaja MapearCierre(SqlDataReader reader)
        {
            return new CierreCaja
            {
                CierreID = reader["CierreID"].ToString(),
                NumeroCierre = (int)reader["NumeroCierre"],
                FechaCierre = (DateTime)reader["FechaCierre"],
                UsuarioID = (int)reader["UsuarioID"],
                SaldoInicial = (decimal)reader["SaldoInicial"],
                TotalVentas = (decimal)reader["TotalVentas"],
                TotalEgresos = (decimal)reader["TotalEgresos"],
                SaldoFinal = (decimal)reader["SaldoFinal"],
                Diferencia = (decimal)reader["Diferencia"],
                Observaciones = reader["Observaciones"] != DBNull.Value ? reader["Observaciones"].ToString() : null
            };
        }
    }
}
