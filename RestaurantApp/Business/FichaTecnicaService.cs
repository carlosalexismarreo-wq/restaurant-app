using System;
using System.Collections.Generic;
using RestaurantApp.Data.Repositories;
using RestaurantApp.Models;

namespace RestaurantApp.Business
{
    /// <summary>
    /// Servicio para gestionar operaciones con fichas técnicas
    /// </summary>
    public class FichaTecnicaService
    {
        private readonly FichaTecnicaRepository _fichaTecnicaRepository;
        private readonly ProductoRepository _productoRepository;

        public FichaTecnicaService()
        {
            _fichaTecnicaRepository = new FichaTecnicaRepository();
            _productoRepository = new ProductoRepository();
        }

        public bool CrearFichaTecnica(int productoID, string nombreFicha, string descripcion, 
                                     decimal porcentajeGanancia, int monedaID)
        {
            try
            {
                FichaTecnica ficha = new FichaTecnica
                {
                    ProductoID = productoID,
                    NombreFicha = nombreFicha,
                    Descripcion = descripcion,
                    CostoTotal = 0, // Se calculará al agregar ingredientes
                    PorcentajeGanancia = porcentajeGanancia,
                    PrecioFinal = 0,
                    MonedaID = monedaID,
                    Activa = true,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now
                };

                return _fichaTecnicaRepository.CrearFichaTecnica(ficha);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear ficha técnica: " + ex.Message);
            }
        }

        public bool AgregarIngrediente(int fichaTecnicaID, int productoIngredienteID, 
                                       decimal cantidadUtilizada, int unidadMedidaID, decimal costoUnitario)
        {
            try
            {
                decimal costoTotal = cantidadUtilizada * costoUnitario;

                DetalleFichaTecnica detalle = new DetalleFichaTecnica
                {
                    FichaTecnicaID = fichaTecnicaID,
                    ProductoIngredienteID = productoIngredienteID,
                    CantidadUtilizada = cantidadUtilizada,
                    UnidadMedidaID = unidadMedidaID,
                    CostoUnitario = costoUnitario,
                    CostoTotal = costoTotal
                };

                return _fichaTecnicaRepository.AgregarDetalleAFicha(detalle);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar ingrediente: " + ex.Message);
            }
        }

        public decimal CalcularPrecioFinal(decimal costoTotal, decimal porcentajeGanancia)
        {
            return costoTotal + (costoTotal * porcentajeGanancia / 100);
        }

        public List<FichaTecnica> ObtenerFichasPorProducto(int productoID)
        {
            return _fichaTecnicaRepository.ObtenerFichasTecnicasPorProducto(productoID);
        }

        public FichaTecnica ObtenerFichaTecnica(int fichaTecnicaID)
        {
            return _fichaTecnicaRepository.ObtenerFichaTecnicaPorID(fichaTecnicaID);
        }
    }
}
