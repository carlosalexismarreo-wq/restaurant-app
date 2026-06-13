namespace RestaurantApp.Models
{
    public class Inventario
    {
        public int InventarioID { get; set; }
        public int ProductoID { get; set; }
        public string Modulo { get; set; } // 'Bar', 'Cocina', 'Almacen'
        public decimal CantidadActual { get; set; }
        public int UnidadMedidaID { get; set; }
        public int MonedaID { get; set; }
        public DateTime FechaActualizacion { get; set; }

        // Propiedades de navegación
        public Producto Producto { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
        public Moneda Moneda { get; set; }
    }
}
