using System;
using System.Data;
using System.Data.SqlClient;
using RestaurantApp.Models;
using System.Collections.Generic;

namespace RestaurantApp.Data.Repositories
{
    /// <summary>
    /// Repositorio para gestionar operaciones con usuarios
    /// </summary>
    public class UsuarioRepository
    {
        private const string TABLE_NAME = "Usuarios";

        public bool CrearUsuario(Usuario usuario)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"INSERT INTO {TABLE_NAME} (NombreUsuario, Contraseña, NombreCompleto, CargoID, Activo)
                                    VALUES (@NombreUsuario, @Contraseña, @NombreCompleto, @CargoID, @Activo)";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
                        cmd.Parameters.AddWithValue("@Contraseña", usuario.Contraseña);
                        cmd.Parameters.AddWithValue("@NombreCompleto", usuario.NombreCompleto);
                        cmd.Parameters.AddWithValue("@CargoID", usuario.CargoID);
                        cmd.Parameters.AddWithValue("@Activo", usuario.Activo);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear usuario: " + ex.Message);
            }
        }

        public Usuario ObtenerUsuarioPorNombre(string nombreUsuario)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} WHERE NombreUsuario = @NombreUsuario AND Activo = 1";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
                        
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapearUsuario(reader);
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario: " + ex.Message);
            }
        }

        public List<Usuario> ObtenerTodosLosUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $"SELECT * FROM {TABLE_NAME} ORDER BY NombreCompleto";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usuarios.Add(MapearUsuario(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuarios: " + ex.Message);
            }
            return usuarios;
        }

        public bool ActualizarUsuario(Usuario usuario)
        {
            try
            {
                using (SqlConnection conn = DatabaseConnection.GetConnection())
                {
                    string query = $@"UPDATE {TABLE_NAME} 
                                    SET NombreCompleto = @NombreCompleto, 
                                        CargoID = @CargoID, 
                                        Activo = @Activo,
                                        FechaUltimaModificacion = GETDATE()
                                    WHERE UsuarioID = @UsuarioID";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UsuarioID", usuario.UsuarioID);
                        cmd.Parameters.AddWithValue("@NombreCompleto", usuario.NombreCompleto);
                        cmd.Parameters.AddWithValue("@CargoID", usuario.CargoID);
                        cmd.Parameters.AddWithValue("@Activo", usuario.Activo);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar usuario: " + ex.Message);
            }
        }

        private Usuario MapearUsuario(SqlDataReader reader)
        {
            return new Usuario
            {
                UsuarioID = (int)reader["UsuarioID"],
                NombreUsuario = reader["NombreUsuario"].ToString(),
                Contraseña = reader["Contraseña"].ToString(),
                NombreCompleto = reader["NombreCompleto"].ToString(),
                CargoID = (int)reader["CargoID"],
                Activo = (bool)reader["Activo"],
                FechaCreacion = (DateTime)reader["FechaCreacion"],
                FechaUltimaModificacion = (DateTime)reader["FechaUltimaModificacion"]
            };
        }
    }
}
