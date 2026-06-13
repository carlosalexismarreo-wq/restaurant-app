namespace RestaurantApp.Models
{
    public class DetalleVenta
    {
        public int DetalleVentaID { get; set; }
        public string VentaID { get; set; }
        public int FichaTecnicaID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal { get; set; }

        // Propiedades de navegación
        public Venta Venta { get; set; }
        public FichaTecnica FichaTecnica { get; set; }
    }
}
