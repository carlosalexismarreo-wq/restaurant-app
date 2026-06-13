using System;
using System.Collections.Generic;
using RestaurantApp.Data.Repositories;
using RestaurantApp.Models;

namespace RestaurantApp.Business
{
    /// <summary>
    /// Servicio para gestionar operaciones de inventario
    /// </summary>
    public class InventarioService
    {
        private readonly InventarioRepository _inventarioRepository;
        private readonly ProductoRepository _productoRepository;

        public InventarioService()
        {
            _inventarioRepository = new InventarioRepository();
            _productoRepository = new ProductoRepository();
        }

        public bool AjustarInventario(int productoID, string modulo, decimal cantidad, 
                                      string tipoMovimiento, int usuarioID, string observaciones = "")
        {
            try
            {
                Inventario inventario = _inventarioRepository.ObtenerInventarioPorProductoYModulo(productoID, modulo);
                
                if (inventario == null)
                    return false;

                decimal nuevaCantidad = inventario.CantidadActual;

                if (tipoMovimiento == "Entrada")
                    nuevaCantidad += cantidad;
                else if (tipoMovimiento == "Salida")
                    nuevaCantidad -= cantidad;
                else if (tipoMovimiento == "Ajuste")
                    nuevaCantidad = cantidad;

                if (nuevaCantidad < 0)
                    throw new Exception("No hay suficiente stock");

                // Registrar el movimiento
                MovimientoInventario movimiento = new MovimientoInventario
                {
                    ProductoID = productoID,
                    TipoMovimiento = tipoMovimiento,
                    Cantidad = cantidad,
                    UnidadMedidaID = inventario.UnidadMedidaID,
                    Modulo = modulo,
                    Observaciones = observaciones,
                    UsuarioID = usuarioID,
                    FechaMovimiento = DateTime.Now
                };

                _inventarioRepository.RegistrarMovimiento(movimiento);

                // Actualizar cantidad
                return _inventarioRepository.ActualizarCantidadInventario(productoID, modulo, nuevaCantidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ajustar inventario: " + ex.Message);
            }
        }

        public Inventario ObtenerInventario(int productoID, string modulo)
        {
            return _inventarioRepository.ObtenerInventarioPorProductoYModulo(productoID, modulo);
        }

        public List<Inventario> ObtenerInventariosPorModulo(string modulo)
        {
            return _inventarioRepository.ObtenerInventariosPorModulo(modulo);
        }

        public bool VerificarDisponibilidad(int productoID, string modulo, decimal cantidadRequerida)
        {
            Inventario inventario = _inventarioRepository.ObtenerInventarioPorProductoYModulo(productoID, modulo);
            if (inventario == null)
                return false;

            return inventario.CantidadActual >= cantidadRequerida;
        }
    }
}
