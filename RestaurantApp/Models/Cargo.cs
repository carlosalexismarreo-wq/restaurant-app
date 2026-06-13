namespace RestaurantApp.Models
{
    public class Cargo
    {
        public int CargoID { get; set; }
        public string NombreCargo { get; set; }
        public string Descripcion { get; set; }
        public bool AccesoCaja { get; set; }
        public bool AccesoBar { get; set; }
        public bool AccesoCocina { get; set; }
        public bool AccesoAlmacen { get; set; }
        public bool AccesoAdmin { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
