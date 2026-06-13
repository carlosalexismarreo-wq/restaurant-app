using System;
using System.Data;
using System.Data.SqlClient;
using RestaurantApp.Models;
using System.Collections.Generic;

namespace RestaurantApp.Data.Repositories
{
    /// <summary>
    /// Repositorio para gestionar operaciones con inventarios
    /// </summary>
    public class InventarioRepository
    {
        private const string TABLE_NAME = "Inventarios";
        private const string TABLE_MOVIMIENTOS = "MovimientosInventario";

        public bool CrearInventario(Inventario inventario)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"INSERT INTO {TABLE_NAME} 
                                    (ProductoID, Modulo, CantidadActual, UnidadMedidaID, MonedaID)
                                    VALUES (@ProductoID, @Modulo, @CantidadActual, @UnidadMedidaID, @MonedaID)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", inventario.ProductoID);
                        cmd.Parameters.AddWithValue("@Modulo", inventario.Modulo);
                        cmd.Parameters.AddWithValue("@CantidadActual", inventario.CantidadActual);
                        cmd.Parameters.AddWithValue("@UnidadMedidaID", inventario.UnidadMedidaID);
                        cmd.Parameters.AddWithValue("@MonedaID", inventario.MonedaID);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear inventario: " + ex.Message);
            }
        }

        public Inventario ObtenerInventarioPorProductoYModulo(int productoID, string modulo)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE ProductoID = @ProductoID AND Modulo = @Modulo";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", productoID);
                        cmd.Parameters.AddWithValue("@Modulo", modulo);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapearInventario(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener inventario: " + ex.Message);
            }
        }

        public List<Inventario> ObtenerInventariosPorModulo(string modulo)
        {
            List<Inventario> inventarios = new List<Inventario>();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE Modulo = @Modulo ORDER BY FechaActualizacion DESC";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Modulo", modulo);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                inventarios.Add(MapearInventario(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener inventarios: " + ex.Message);
            }
            return inventarios;
        }

        public bool ActualizarCantidadInventario(int productoID, string modulo, decimal nuevaCantidad)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"UPDATE {TABLE_NAME} 
                                    SET CantidadActual = @CantidadActual,
                                        FechaActualizacion = GETDATE()
                                    WHERE ProductoID = @ProductoID AND Modulo = @Modulo";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", productoID);
                        cmd.Parameters.AddWithValue("@Modulo", modulo);
                        cmd.Parameters.AddWithValue("@CantidadActual", nuevaCantidad);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar inventario: " + ex.Message);
            }
        }

        public bool RegistrarMovimiento(MovimientoInventario movimiento)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"INSERT INTO {TABLE_MOVIMIENTOS}
                                    (ProductoID, TipoMovimiento, Cantidad, UnidadMedidaID, Modulo,
                                     ValeID, InformeRecepcionID, Observaciones, UsuarioID)
                                    VALUES (@ProductoID, @TipoMovimiento, @Cantidad, @UnidadMedidaID, @Modulo,
                                            @ValeID, @InformeRecepcionID, @Observaciones, @UsuarioID)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", movimiento.ProductoID);
                        cmd.Parameters.AddWithValue("@TipoMovimiento", movimiento.TipoMovimiento);
                        cmd.Parameters.AddWithValue("@Cantidad", movimiento.Cantidad);
                        cmd.Parameters.AddWithValue("@UnidadMedidaID", movimiento.UnidadMedidaID);
                        cmd.Parameters.AddWithValue("@Modulo", movimiento.Modulo);
                        cmd.Parameters.AddWithValue("@ValeID", movimiento.ValeID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@InformeRecepcionID", movimiento.InformeRecepcionID ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Observaciones", movimiento.Observaciones ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@UsuarioID", movimiento.UsuarioID);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar movimiento: " + ex.Message);
            }
        }

        private Inventario MapearInventario(SqlDataReader reader)
        {
            return new Inventario
            {
                InventarioID = (int)reader["InventarioID"],
                ProductoID = (int)reader["ProductoID"],
                Modulo = reader["Modulo"].ToString(),
                CantidadActual = (decimal)reader["CantidadActual"],
                UnidadMedidaID = (int)reader["UnidadMedidaID"],
                MonedaID = (int)reader["MonedaID"],
                FechaActualizacion = (DateTime)reader["FechaActualizacion"]
            };
        }
    }
}
