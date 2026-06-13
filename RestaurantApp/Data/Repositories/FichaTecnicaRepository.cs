using System;
using System.Data;
using System.Data.SqlClient;
using RestaurantApp.Models;
using System.Collections.Generic;

namespace RestaurantApp.Data.Repositories
{
    /// <summary>
    /// Repositorio para gestionar operaciones con fichas técnicas
    /// </summary>
    public class FichaTecnicaRepository
    {
        private const string TABLE_NAME = "FichasTecnicas";
        private const string TABLE_DETALLE = "DetalleFichasTecnicas";

        public bool CrearFichaTecnica(FichaTecnica ficha)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"INSERT INTO {TABLE_NAME} 
                                    (ProductoID, NombreFicha, Descripcion, CostoTotal, PorcentajeGanancia, 
                                     PrecioFinal, MonedaID, Activa)
                                    VALUES (@ProductoID, @NombreFicha, @Descripcion, @CostoTotal, @PorcentajeGanancia,
                                            @PrecioFinal, @MonedaID, @Activa)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", ficha.ProductoID);
                        cmd.Parameters.AddWithValue("@NombreFicha", ficha.NombreFicha);
                        cmd.Parameters.AddWithValue("@Descripcion", ficha.Descripcion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CostoTotal", ficha.CostoTotal);
                        cmd.Parameters.AddWithValue("@PorcentajeGanancia", ficha.PorcentajeGanancia);
                        cmd.Parameters.AddWithValue("@PrecioFinal", ficha.PrecioFinal);
                        cmd.Parameters.AddWithValue("@MonedaID", ficha.MonedaID);
                        cmd.Parameters.AddWithValue("@Activa", ficha.Activa);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear ficha técnica: " + ex.Message);
            }
        }

        public FichaTecnica ObtenerFichaTecnicaPorID(int fichaTecnicaID)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE FichaTecnicaID = @FichaTecnicaID";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FichaTecnicaID", fichaTecnicaID);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapearFichaTecnica(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ficha técnica: " + ex.Message);
            }
        }

        public List<FichaTecnica> ObtenerFichasTecnicasPorProducto(int productoID)
        {
            List<FichaTecnica> fichas = new List<FichaTecnica>();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE ProductoID = @ProductoID AND Activa = 1";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ProductoID", productoID);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fichas.Add(MapearFichaTecnica(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener fichas técnicas: " + ex.Message);
            }
            return fichas;
        }

        public List<FichaTecnica> ObtenerTodasLasFichasTecnicas()
        {
            List<FichaTecnica> fichas = new List<FichaTecnica>();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE Activa = 1 ORDER BY NombreFicha";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                fichas.Add(MapearFichaTecnica(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener fichas técnicas: " + ex.Message);
            }
            return fichas;
        }

        public bool AgregarDetalleAFicha(DetalleFichaTecnica detalle)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"INSERT INTO {TABLE_DETALLE}
                                    (FichaTecnicaID, ProductoIngredienteID, CantidadUtilizada, UnidadMedidaID, 
                                     CostoUnitario, CostoTotal)
                                    VALUES (@FichaTecnicaID, @ProductoIngredienteID, @CantidadUtilizada, @UnidadMedidaID,
                                            @CostoUnitario, @CostoTotal)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FichaTecnicaID", detalle.FichaTecnicaID);
                        cmd.Parameters.AddWithValue("@ProductoIngredienteID", detalle.ProductoIngredienteID);
                        cmd.Parameters.AddWithValue("@CantidadUtilizada", detalle.CantidadUtilizada);
                        cmd.Parameters.AddWithValue("@UnidadMedidaID", detalle.UnidadMedidaID);
                        cmd.Parameters.AddWithValue("@CostoUnitario", detalle.CostoUnitario);
                        cmd.Parameters.AddWithValue("@CostoTotal", detalle.CostoTotal);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar detalle a ficha: " + ex.Message);
            }
        }

        public bool ActualizarFichaTecnica(FichaTecnica ficha)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"UPDATE {TABLE_NAME} 
                                    SET NombreFicha = @NombreFicha, 
                                        Descripcion = @Descripcion,
                                        CostoTotal = @CostoTotal,
                                        PorcentajeGanancia = @PorcentajeGanancia,
                                        PrecioFinal = @PrecioFinal,
                                        FechaModificacion = GETDATE(),
                                        Activa = @Activa
                                    WHERE FichaTecnicaID = @FichaTecnicaID";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@FichaTecnicaID", ficha.FichaTecnicaID);
                        cmd.Parameters.AddWithValue("@NombreFicha", ficha.NombreFicha);
                        cmd.Parameters.AddWithValue("@Descripcion", ficha.Descripcion ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@CostoTotal", ficha.CostoTotal);
                        cmd.Parameters.AddWithValue("@PorcentajeGanancia", ficha.PorcentajeGanancia);
                        cmd.Parameters.AddWithValue("@PrecioFinal", ficha.PrecioFinal);
                        cmd.Parameters.AddWithValue("@Activa", ficha.Activa);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar ficha técnica: " + ex.Message);
            }
        }

        private FichaTecnica MapearFichaTecnica(SqlDataReader reader)
        {
            return new FichaTecnica
            {
                FichaTecnicaID = (int)reader["FichaTecnicaID"],
                ProductoID = (int)reader["ProductoID"],
                NombreFicha = reader["NombreFicha"].ToString(),
                Descripcion = reader["Descripcion"] != DBNull.Value ? reader["Descripcion"].ToString() : null,
                CostoTotal = (decimal)reader["CostoTotal"],
                PorcentajeGanancia = (decimal)reader["PorcentajeGanancia"],
                PrecioFinal = (decimal)reader["PrecioFinal"],
                MonedaID = (int)reader["MonedaID"],
                Activa = (bool)reader["Activa"],
                FechaCreacion = (DateTime)reader["FechaCreacion"],
                FechaModificacion = (DateTime)reader["FechaModificacion"]
            };
        }
    }
}
