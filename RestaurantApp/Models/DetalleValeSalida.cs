namespace RestaurantApp.Models
{
    public class DetalleValeSalida
    {
        public int DetalleValeID { get; set; }
        public string ValeID { get; set; }
        public int ProductoID { get; set; }
        public decimal CantidadSolicitada { get; set; }
        public decimal? CantidadEntregada { get; set; }
        public int UnidadMedidaID { get; set; }

        // Propiedades de navegación
        public ValesSalida Vale { get; set; }
        public Producto Producto { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
    }
}
