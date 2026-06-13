namespace RestaurantApp.Models
{
    public class DetalleInformeRecepcion
    {
        public int DetalleInformeID { get; set; }
        public string InformeRecepcionID { get; set; }
        public int ProductoID { get; set; }
        public decimal CantidadRecibida { get; set; }
        public int UnidadMedidaID { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal CostoTotal { get; set; }

        // Propiedades de navegación
        public InformeRecepcion InformeRecepcion { get; set; }
        public Producto Producto { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
    }
}
