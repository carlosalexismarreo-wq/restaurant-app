using System;
using System.IO;
using System.Data.SqlClient;

namespace RestaurantApp.Utils
{
    /// <summary>
    /// Helper para operaciones de respaldo
    /// </summary>
    public class BackupHelper
    {
        public static bool VerificarRutaRespaldo(string ruta)
        {
            try
            {
                if (!Directory.Exists(ruta))
                    Directory.CreateDirectory(ruta);
                
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ObtenerRutaPredeterminada()
        {
            string carpetaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string rutaRespaldos = Path.Combine(carpetaDocumentos, "RestaurantApp", "Respaldos");
            
            if (!Directory.Exists(rutaRespaldos))
                Directory.CreateDirectory(rutaRespaldos);

            return rutaRespaldos;
        }

        public static long ObtenerTamanoArchivo(string rutaArchivo)
        {
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    FileInfo fileInfo = new FileInfo(rutaArchivo);
                    return fileInfo.Length;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static string FormatearTamano(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }
    }
}
