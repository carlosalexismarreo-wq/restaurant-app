namespace RestaurantApp.Models
{
    public class DetalleFichaTecnica
    {
        public int DetalleID { get; set; }
        public int FichaTecnicaID { get; set; }
        public int ProductoIngredienteID { get; set; }
        public decimal CantidadUtilizada { get; set; }
        public int UnidadMedidaID { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal CostoTotal { get; set; }

        // Propiedades de navegación
        public FichaTecnica FichaTecnica { get; set; }
        public Producto ProductoIngrediente { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
    }
}
