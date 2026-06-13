using System;
using System.Data;
using System.Data.SqlClient;
using RestaurantApp.Models;
using System.Collections.Generic;

namespace RestaurantApp.Data.Repositories
{
    /// <summary>
    /// Repositorio para gestionar operaciones con productos
    /// </summary>
    public class ProductoRepository
    {
        private const string TABLE_NAME = "Productos";

        public bool CrearProducto(Producto producto)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"INSERT INTO {TABLE_NAME} 
                                    (CodigoProducto, NombreProducto, CategoriaID, UnidadMedidaID, 
                                     PrecioCosto, PrecioVenta, MonedaID, StockActual, StockMinimo, Activo)
                                    VALUES (@CodigoProducto, @NombreProducto, @CategoriaID, @UnidadMedidaID,
                                            @PrecioCosto, @PrecioVenta, @MonedaID, @StockActual, @StockMinimo, @Activo)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CodigoProducto", producto.CodigoProducto);
                        cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                        cmd.Parameters.AddWithValue("@CategoriaID", producto.CategoriaID);
                        cmd.Parameters.AddWithValue("@UnidadMedidaID", producto.UnidadMedidaID);
                        cmd.Parameters.AddWithValue("@PrecioCosto", producto.PrecioCosto);
                        cmd.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                        cmd.Parameters.AddWithValue("@MonedaID", producto.MonedaID);
                        cmd.Parameters.AddWithValue("@StockActual", producto.StockActual);
                        cmd.Parameters.AddWithValue("@StockMinimo", producto.StockMinimo);
                        cmd.Parameters.AddWithValue("@Activo", producto.Activo);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear producto: " + ex.Message);
            }
        }

        public Producto ObtenerProductoPorCodigo(string codigoProducto)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE CodigoProducto = @CodigoProducto AND Activo = 1";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CodigoProducto", codigoProducto);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapearProducto(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener producto: " + ex.Message);
            }
        }

        public Producto ObtenerProductoPorID(int productoID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE ProductoID = @ProductoID";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", productoID);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapearProducto(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener producto: " + ex.Message);
            }
        }

        public List<Producto> ObtenerProductosPorCategoria(int categoriaID)
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE CategoriaID = @CategoriaID AND Activo = 1 ORDER BY NombreProducto";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CategoriaID", categoriaID);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(MapearProducto(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos: " + ex.Message);
            }
            return productos;
        }

        public List<Producto> ObtenerTodosLosProductos()
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE Activo = 1 ORDER BY NombreProducto";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                productos.Add(MapearProducto(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener productos: " + ex.Message);
            }
            return productos;
        }

        public bool ActualizarProducto(Producto producto)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"UPDATE {TABLE_NAME} 
                                    SET NombreProducto = @NombreProducto, 
                                        CategoriaID = @CategoriaID,
                                        UnidadMedidaID = @UnidadMedidaID,
                                        PrecioCosto = @PrecioCosto,
                                        PrecioVenta = @PrecioVenta,
                                        MonedaID = @MonedaID,
                                        StockMinimo = @StockMinimo,
                                        Activo = @Activo
                                    WHERE ProductoID = @ProductoID";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", producto.ProductoID);
                        cmd.Parameters.AddWithValue("@NombreProducto", producto.NombreProducto);
                        cmd.Parameters.AddWithValue("@CategoriaID", producto.CategoriaID);
                        cmd.Parameters.AddWithValue("@UnidadMedidaID", producto.UnidadMedidaID);
                        cmd.Parameters.AddWithValue("@PrecioCosto", producto.PrecioCosto);
                        cmd.Parameters.AddWithValue("@PrecioVenta", producto.PrecioVenta);
                        cmd.Parameters.AddWithValue("@MonedaID", producto.MonedaID);
                        cmd.Parameters.AddWithValue("@StockMinimo", producto.StockMinimo);
                        cmd.Parameters.AddWithValue("@Activo", producto.Activo);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar producto: " + ex.Message);
            }
        }

        public bool ActualizarStock(int productoID, decimal nuevaCantidad)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"UPDATE {TABLE_NAME} SET StockActual = @StockActual WHERE ProductoID = @ProductoID";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", productoID);
                        cmd.Parameters.AddWithValue("@StockActual", nuevaCantidad);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar stock: " + ex.Message);
            }
        }

        private Producto MapearProducto(SqlDataReader reader)
        {
            return new Producto
            {
                ProductoID = (int)reader["ProductoID"],
                CodigoProducto = reader["CodigoProducto"].ToString(),
                NombreProducto = reader["NombreProducto"].ToString(),
                CategoriaID = (int)reader["CategoriaID"],
                UnidadMedidaID = (int)reader["UnidadMedidaID"],
                PrecioCosto = (decimal)reader["PrecioCosto"],
                PrecioVenta = (decimal)reader["PrecioVenta"],
                MonedaID = (int)reader["MonedaID"],
                StockActual = (decimal)reader["StockActual"],
                StockMinimo = (decimal)reader["StockMinimo"],
                Activo = (bool)reader["Activo"],
                FechaCreacion = (DateTime)reader["FechaCreacion"]
            };
        }
    }
}
