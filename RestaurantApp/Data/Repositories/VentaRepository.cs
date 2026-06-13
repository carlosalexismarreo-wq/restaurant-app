using System;
using System.Data;
using System.Data.SqlClient;
using RestaurantApp.Models;
using System.Collections.Generic;

namespace RestaurantApp.Data.Repositories
{
    /// <summary>
    /// Repositorio para gestionar operaciones con ventas
    /// </summary>
    public class VentaRepository
    {
        private const string TABLE_NAME = "Ventas";
        private const string TABLE_DETALLE = "DetalleVentas";

        public bool CrearVenta(Venta venta)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"INSERT INTO {TABLE_NAME}
                                    (VentaID, NumeroVenta, FechVenta, UsuarioID, TipoVenta, MonedaID,
                                     MontoTotal, Descuento, MontoNeto, FormaPago, Estado)
                                    VALUES (@VentaID, @NumeroVenta, @FechVenta, @UsuarioID, @TipoVenta, @MonedaID,
                                            @MontoTotal, @Descuento, @MontoNeto, @FormaPago, @Estado)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VentaID", venta.VentaID);
                        cmd.Parameters.AddWithValue("@NumeroVenta", venta.NumeroVenta);
                        cmd.Parameters.AddWithValue("@FechVenta", venta.FechVenta);
                        cmd.Parameters.AddWithValue("@UsuarioID", venta.UsuarioID);
                        cmd.Parameters.AddWithValue("@TipoVenta", venta.TipoVenta);
                        cmd.Parameters.AddWithValue("@MonedaID", venta.MonedaID);
                        cmd.Parameters.AddWithValue("@MontoTotal", venta.MontoTotal);
                        cmd.Parameters.AddWithValue("@Descuento", venta.Descuento);
                        cmd.Parameters.AddWithValue("@MontoNeto", venta.MontoNeto);
                        cmd.Parameters.AddWithValue("@FormaPago", venta.FormaPago);
                        cmd.Parameters.AddWithValue("@Estado", venta.Estado);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear venta: " + ex.Message);
            }
        }

        public bool AgregarDetalleVenta(DetalleVenta detalle)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"INSERT INTO {TABLE_DETALLE}
                                    (VentaID, FichaTecnicaID, Cantidad, PrecioUnitario, PrecioTotal)
                                    VALUES (@VentaID, @FichaTecnicaID, @Cantidad, @PrecioUnitario, @PrecioTotal)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VentaID", detalle.VentaID);
                        cmd.Parameters.AddWithValue("@FichaTecnicaID", detalle.FichaTecnicaID);
                        cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                        cmd.Parameters.AddWithValue("@PrecioTotal", detalle.PrecioTotal);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar detalle de venta: " + ex.Message);
            }
        }

        public Venta ObtenerVentaPorID(string ventaID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE VentaID = @VentaID";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@VentaID", ventaID);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapearVenta(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener venta: " + ex.Message);
            }
        }

        public List<Venta> ObtenerVentasPorFecha(DateTime fecha)
        {
            List<Venta> ventas = new List<Venta>();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE CAST(FechVenta AS DATE) = @Fecha ORDER BY FechVenta DESC";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Fecha", fecha.Date);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ventas.Add(MapearVenta(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ventas: " + ex.Message);
            }
            return ventas;
        }

        public int ObtenerProximoNumeroVenta()
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT ISNULL(MAX(NumeroVenta), 0) + 1 FROM {TABLE_NAME}";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        return (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener próximo número de venta: " + ex.Message);
            }
        }

        private Venta MapearVenta(SqlDataReader reader)
        {
            return new Venta
            {
                VentaID = reader["VentaID"].ToString(),
                NumeroVenta = (int)reader["NumeroVenta"],
                FechVenta = (DateTime)reader["FechVenta"],
                UsuarioID = (int)reader["UsuarioID"],
                TipoVenta = reader["TipoVenta"].ToString(),
                MonedaID = (int)reader["MonedaID"],
                MontoTotal = (decimal)reader["MontoTotal"],
                Descuento = (decimal)reader["Descuento"],
                MontoNeto = (decimal)reader["MontoNeto"],
                FormaPago = reader["FormaPago"].ToString(),
                Estado = reader["Estado"].ToString()
            };
        }
    }
}
