using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantApp.Data.Repositories;
using RestaurantApp.Models;

namespace RestaurantApp.Business
{
    /// <summary>
    /// Servicio para gestionar operaciones de ventas
    /// </summary>
    public class VentaService
    {
        private readonly VentaRepository _ventaRepository;
        private readonly FichaTecnicaRepository _fichaTecnicaRepository;
        private readonly InventarioRepository _inventarioRepository;
        private readonly InventarioService _inventarioService;

        public VentaService()
        {
            _ventaRepository = new VentaRepository();
            _fichaTecnicaRepository = new FichaTecnicaRepository();
            _inventarioRepository = new InventarioRepository();
            _inventarioService = new InventarioService();
        }

        public Venta CrearVenta(int usuarioID, string tipoVenta, int monedaID, string formaPago, int usuarioCreador)
        {
            try
            {
                int numeroVenta = _ventaRepository.ObtenerProximoNumeroVenta();
                string ventaID = GenerarVentaID();

                Venta venta = new Venta
                {
                    VentaID = ventaID,
                    NumeroVenta = numeroVenta,
                    FechVenta = DateTime.Now,
                    UsuarioID = usuarioID,
                    TipoVenta = tipoVenta,
                    MonedaID = monedaID,
                    MontoTotal = 0,
                    Descuento = 0,
                    MontoNeto = 0,
                    FormaPago = formaPago,
                    Estado = "Completada"
                };

                _ventaRepository.CrearVenta(venta);
                return venta;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear venta: " + ex.Message);
            }
        }

        public bool AgregarProductoAVenta(string ventaID, int fichaTecnicaID, int cantidad, 
                                         decimal precioUnitario, string modulo, int usuarioID)
        {
            try
            {
                FichaTecnica ficha = _fichaTecnicaRepository.ObtenerFichaTecnicaPorID(fichaTecnicaID);
                if (ficha == null)
                    throw new Exception("Ficha técnica no encontrada");

                // Verificar disponibilidad de inventario
                if (!_inventarioService.VerificarDisponibilidad(ficha.ProductoID, modulo, cantidad))
                    throw new Exception("No hay suficiente stock");

                decimal precioTotal = cantidad * precioUnitario;

                DetalleVenta detalle = new DetalleVenta
                {
                    VentaID = ventaID,
                    FichaTecnicaID = fichaTecnicaID,
                    Cantidad = cantidad,
                    PrecioUnitario = precioUnitario,
                    PrecioTotal = precioTotal
                };

                // Registrar movimiento de salida en inventario
                _inventarioService.AjustarInventario(ficha.ProductoID, modulo, cantidad, 
                                                     "Salida", usuarioID, $"Venta {ventaID}");

                return _ventaRepository.AgregarDetalleVenta(detalle);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar producto a venta: " + ex.Message);
            }
        }

        public List<Venta> ObtenerVentasPorFecha(DateTime fecha)
        {
            return _ventaRepository.ObtenerVentasPorFecha(fecha);
        }

        public decimal CalcularTotalVentasPorFecha(DateTime fecha)
        {
            List<Venta> ventas = _ventaRepository.ObtenerVentasPorFecha(fecha);
            return ventas.Sum(v => v.MontoNeto);
        }

        private string GenerarVentaID()
        {
            return "VTA-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
