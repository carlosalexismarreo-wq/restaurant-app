namespace RestaurantApp.Models
{
    public class Gasto
    {
        public int GastoID { get; set; }
        public DateTime FechaGasto { get; set; }
        public int UsuarioID { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public int MonedaID { get; set; }
        public string CategoriaGasto { get; set; }
        public bool EsContabilizado { get; set; }

        // Propiedades de navegación
        public Usuario Usuario { get; set; }
        public Moneda Moneda { get; set; }
    }
}
