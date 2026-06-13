namespace RestaurantApp.Models
{
    public class FichaTecnica
    {
        public int FichaTecnicaID { get; set; }
        public int ProductoID { get; set; }
        public string NombreFicha { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoTotal { get; set; }
        public decimal PorcentajeGanancia { get; set; }
        public decimal PrecioFinal { get; set; }
        public int MonedaID { get; set; }
        public bool Activa { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        // Propiedades de navegación
        public Producto Producto { get; set; }
        public Moneda Moneda { get; set; }
        public List<DetalleFichaTecnica> Detalles { get; set; } = new List<DetalleFichaTecnica>();
    }
}
