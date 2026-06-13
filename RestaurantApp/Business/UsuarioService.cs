using System;
using System.Security.Cryptography;
using System.Text;
using RestaurantApp.Data.Repositories;
using RestaurantApp.Models;

namespace RestaurantApp.Business
{
    /// <summary>
    /// Servicio para gestionar operaciones de usuarios
    /// </summary>
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService()
        {
            _usuarioRepository = new UsuarioRepository();
        }

        public bool AutenticarUsuario(string nombreUsuario, string contrasena)
        {
            try
            {
                Usuario usuario = _usuarioRepository.ObtenerUsuarioPorNombre(nombreUsuario);
                if (usuario == null)
                    return false;

                string contrasenaEncriptada = EncriptarContrasena(contrasena);
                return usuario.Contrasena == contrasenaEncriptada && usuario.Activo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar usuario: " + ex.Message);
            }
        }

        public Usuario ObtenerUsuario(string nombreUsuario)
        {
            return _usuarioRepository.ObtenerUsuarioPorNombre(nombreUsuario);
        }

        public bool CrearUsuario(string nombreUsuario, string contrasena, string nombreCompleto, int cargoID)
        {
            try
            {
                Usuario usuario = new Usuario
                {
                    NombreUsuario = nombreUsuario,
                    Contrasena = EncriptarContrasena(contrasena),
                    NombreCompleto = nombreCompleto,
                    CargoID = cargoID,
                    Activo = true,
                    FechaCreacion = DateTime.Now,
                    FechaUltimaModificacion = DateTime.Now
                };

                return _usuarioRepository.CrearUsuario(usuario);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear usuario: " + ex.Message);
            }
        }

        private string EncriptarContrasena(string contrasena)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasena));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
