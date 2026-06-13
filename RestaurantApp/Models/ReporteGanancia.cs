namespace RestaurantApp.Models
{
    public class ReporteGanancia
    {
        public int ReporteID { get; set; }
        public DateTime Fecha { get; set; }
        public decimal TotalVentasUSD { get; set; }
        public decimal TotalVentasCUP { get; set; }
        public decimal TotalCostosUSD { get; set; }
        public decimal TotalCostosCUP { get; set; }
        public decimal TotalGastosUSD { get; set; }
        public decimal TotalGastosCUP { get; set; }
        public decimal GananciaNetaUSD { get; set; }
        public decimal GananciaNetaCUP { get; set; }
        public DateTime FechaGeneracion { get; set; }
    }
}
