namespace RestaurantApp.Models
{
    public class CierreCaja
    {
        public string CierreID { get; set; }
        public int NumeroCierre { get; set; }
        public DateTime FechaCierre { get; set; }
        public int UsuarioID { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal TotalVentas { get; set; }
        public decimal TotalEgresos { get; set; }
        public decimal SaldoFinal { get; set; }
        public decimal Diferencia { get; set; }
        public string Observaciones { get; set; }

        // Propiedades de navegación
        public Usuario Usuario { get; set; }
    }
}
