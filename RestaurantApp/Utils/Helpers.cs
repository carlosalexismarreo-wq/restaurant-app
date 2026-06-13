using System;

namespace RestaurantApp.Utils
{
    /// <summary>
    /// Funciones auxiliares generales
    /// </summary>
    public class Helpers
    {
        public static string FormatearMoneda(decimal valor, string simboloMoneda = "$")
        {
            return $"{simboloMoneda}{valor:F2}";
        }

        public static string FormatearFecha(DateTime fecha)
        {
            return fecha.ToString("dd/MM/yyyy");
        }

        public static string FormatearFechaHora(DateTime fechaHora)
        {
            return fechaHora.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string FormatearCantidad(decimal cantidad, string abreviatura)
        {
            return $"{cantidad} {abreviatura}";
        }

        public static bool EsFinDeSemana(DateTime fecha)
        {
            return fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday;
        }

        public static int ObtenerDiasTranscurridos(DateTime fechaInicio, DateTime fechaFin)
        {
            return (fechaFin - fechaInicio).Days;
        }

        public static string GenerarCodigoUnico()
        {
            return Guid.NewGuid().ToString().Substring(0, 8).ToUpper();
        }

        public static decimal CalcularPorcentaje(decimal valor, decimal porcentaje)
        {
            return valor * (porcentaje / 100);
        }

        public static decimal CalcularValorConDescuento(decimal valor, decimal descuento)
        {
            return valor - (valor * (descuento / 100));
        }
    }
}
