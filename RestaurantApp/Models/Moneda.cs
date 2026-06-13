namespace RestaurantApp.Models
{
    public class Moneda
    {
        public int MonedaID { get; set; }
        public string Codigo { get; set; } // USD, CUP
        public string Nombre { get; set; }
        public string Simbolo { get; set; }
        public decimal TasaCambio { get; set; }
        public bool Activa { get; set; }
    }
}
