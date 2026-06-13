namespace RestaurantApp.Models
{
    public class Venta
    {
        public string VentaID { get; set; }
        public int NumeroVenta { get; set; }
        public DateTime FechVenta { get; set; }
        public int UsuarioID { get; set; }
        public string TipoVenta { get; set; } // 'Bar', 'Cocina', 'Mostrador'
        public int MonedaID { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal MontoNeto { get; set; }
        public string FormaPago { get; set; } // 'Efectivo', 'Tarjeta', 'Mixto'
        public string Estado { get; set; } // 'Completada', 'Cancelada'

        // Propiedades de navegación
        public Usuario Usuario { get; set; }
        public Moneda Moneda { get; set; }
        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}
