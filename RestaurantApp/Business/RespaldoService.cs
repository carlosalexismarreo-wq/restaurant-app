using System;
using System.IO;
using System.Data.SqlClient;

namespace RestaurantApp.Business
{
    /// <summary>
    /// Servicio para gestionar respaldos de la base de datos
    /// </summary>
    public class RespaldoService
    {
        public bool RealizarRespaldo(int usuarioID, string rutaDestino)
        {
            try
            {
                if (!Directory.Exists(rutaDestino))
                    Directory.CreateDirectory(rutaDestino);

                string nombreArchivo = $"RestaurantDB_Respaldo_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                string rutaCompleta = Path.Combine(rutaDestino, nombreArchivo);

                SqlConnection conn = Data.DatabaseConnection.GetConnection();
                string query = $@"BACKUP DATABASE [RestaurantDB] 
                               TO DISK = '{rutaCompleta}' 
                               WITH INIT, COMPRESSION";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandTimeout = 300;
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                // Registrar respaldo en BD
                RegistrarRespaldoEnBD(usuarioID, rutaCompleta);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al realizar respaldo: " + ex.Message);
            }
        }

        private void RegistrarRespaldoEnBD(int usuarioID, string rutaRespaldo)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(rutaRespaldo);
                long tamano = fileInfo.Length;

                using (SqlConnection conn = Data.DatabaseConnection.GetConnection())
                {
                    string query = @"INSERT INTO RespaldosDB (RutaRespaldo, TamanoRespaldo, UsuarioRealiza, FechaRespaldo)
                                    VALUES (@RutaRespaldo, @TamanoRespaldo, @UsuarioRealiza, GETDATE())";
                    
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@RutaRespaldo", rutaRespaldo);
                        cmd.Parameters.AddWithValue("@TamanoRespaldo", tamano);
                        cmd.Parameters.AddWithValue("@UsuarioRealiza", usuarioID);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar respaldo: " + ex.Message);
            }
        }
    }
}
