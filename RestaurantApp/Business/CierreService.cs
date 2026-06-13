using System;
using System.Collections.Generic;
using RestaurantApp.Data.Repositories;
using RestaurantApp.Models;

namespace RestaurantApp.Business
{
    /// <summary>
    /// Servicio para gestionar cierres de caja
    /// </summary>
    public class CierreService
    {
        private readonly CierreRepository _cierreRepository;
        private readonly VentaRepository _ventaRepository;

        public CierreService()
        {
            _cierreRepository = new CierreRepository();
            _ventaRepository = new VentaRepository();
        }

        public bool CrearCierreCaja(int usuarioID, decimal saldoInicial, decimal totalEgresos, string observaciones = "")
        {
            try
            {
                DateTime hoy = DateTime.Now.Date;
                decimal totalVentas = CalcularTotalVentasDelDia(hoy);
                decimal saldoFinal = saldoInicial + totalVentas - totalEgresos;
                decimal diferencia = saldoFinal - saldoInicial;

                int numeroCierre = _cierreRepository.ObtenerProximoNumeroCierre();
                string cierreID = GenerarCierreID();

                CierreCaja cierre = new CierreCaja
                {
                    CierreID = cierreID,
                    NumeroCierre = numeroCierre,
                    FechaCierre = DateTime.Now,
                    UsuarioID = usuarioID,
                    SaldoInicial = saldoInicial,
                    TotalVentas = totalVentas,
                    TotalEgresos = totalEgresos,
                    SaldoFinal = saldoFinal,
                    Diferencia = diferencia,
                    Observaciones = observaciones
                };

                return _cierreRepository.CrearCierre(cierre);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear cierre de caja: " + ex.Message);
            }
        }

        public List<CierreCaja> ObtenerCierresPorFecha(DateTime fecha)
        {
            return _cierreRepository.ObtenerCierresPorFecha(fecha);
        }

        private decimal CalcularTotalVentasDelDia(DateTime fecha)
        {
            List<Venta> ventas = _ventaRepository.ObtenerVentasPorFecha(fecha);
            decimal total = 0;
            
            foreach (Venta venta in ventas)
            {
                total += venta.MontoNeto;
            }

            return total;
        }

        private string GenerarCierreID()
        {
            return "CIE-" + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}
