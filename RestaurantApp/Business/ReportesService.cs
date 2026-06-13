using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantApp.Data.Repositories;
using RestaurantApp.Models;

namespace RestaurantApp.Business
{
    /// <summary>
    /// Servicio para gestionar reportes y ganancias
    /// </summary>
    public class ReportesService
    {
        private readonly VentaRepository _ventaRepository;

        public ReportesService()
        {
            _ventaRepository = new VentaRepository();
        }

        public decimal CalcularGananciaDelDia(DateTime fecha, int monedaID)
        {
            try
            {
                List<Venta> ventas = _ventaRepository.ObtenerVentasPorFecha(fecha);
                return ventas.Where(v => v.MonedaID == monedaID).Sum(v => v.MontoNeto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular ganancia del día: " + ex.Message);
            }
        }

        public decimal CalcularGananciaDelPeriodo(DateTime fechaInicio, DateTime fechaFin, int monedaID)
        {
            try
            {
                decimal gananciaTotal = 0;
                
                for (DateTime fecha = fechaInicio; fecha <= fechaFin; fecha = fecha.AddDays(1))
                {
                    gananciaTotal += CalcularGananciaDelDia(fecha, monedaID);
                }

                return gananciaTotal;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al calcular ganancia del período: " + ex.Message);
            }
        }

        public Dictionary<string, decimal> ObtenerGananciasPorTipo(DateTime fecha)
        {
            try
            {
                List<Venta> ventas = _ventaRepository.ObtenerVentasPorFecha(fecha);
                Dictionary<string, decimal> ganancias = new Dictionary<string, decimal>();

                var tiposVenta = ventas.Select(v => v.TipoVenta).Distinct();

                foreach (var tipo in tiposVenta)
                {
                    decimal total = ventas.Where(v => v.TipoVenta == tipo).Sum(v => v.MontoNeto);
                    ganancias[tipo] = total;
                }

                return ganancias;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ganancias por tipo: " + ex.Message);
            }
        }
    }
}
