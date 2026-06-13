namespace RestaurantApp.Models
{
    public class MovimientoInventario
    {
        public int MovimientoID { get; set; }
        public int ProductoID { get; set; }
        public string TipoMovimiento { get; set; } // 'Entrada', 'Salida', 'Ajuste'
        public decimal Cantidad { get; set; }
        public int UnidadMedidaID { get; set; }
        public string Modulo { get; set; }
        public string ValeID { get; set; }
        public string InformeRecepcionID { get; set; }
        public string Observaciones { get; set; }
        public int UsuarioID { get; set; }
        public DateTime FechaMovimiento { get; set; }

        // Propiedades de navegación
        public Producto Producto { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
        public Usuario Usuario { get; set; }
    }
}
