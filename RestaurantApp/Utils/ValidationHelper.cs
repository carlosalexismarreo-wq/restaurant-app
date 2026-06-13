using System;
using System.Text.RegularExpressions;

namespace RestaurantApp.Utils
{
    /// <summary>
    /// Validadores para datos del sistema
    /// </summary>
    public class ValidationHelper
    {
        public static bool ValidarNombreUsuario(string nombreUsuario)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                return false;

            if (nombreUsuario.Length < 3 || nombreUsuario.Length > 50)
                return false;

            return Regex.IsMatch(nombreUsuario, @"^[a-zA-Z0-9_]*$");
        }

        public static bool ValidarContrasena(string contrasena)
        {
            if (string.IsNullOrWhiteSpace(contrasena))
                return false;

            return contrasena.Length >= 6;
        }

        public static bool ValidarEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValidarCodigoProducto(string codigo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                return false;

            return codigo.Length >= 3 && codigo.Length <= 50;
        }

        public static bool ValidarMonto(decimal monto)
        {
            return monto > 0;
        }

        public static bool ValidarPorcentaje(decimal porcentaje)
        {
            return porcentaje >= 0 && porcentaje <= 100;
        }

        public static bool ValidarCantidad(decimal cantidad)
        {
            return cantidad > 0;
        }
    }
}
