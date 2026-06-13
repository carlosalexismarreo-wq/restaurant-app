namespace RestaurantApp.Models
{
    public class RespaldoDB
    {
        public int RespaldoID { get; set; }
        public DateTime FechaRespaldo { get; set; }
        public string RutaRespaldo { get; set; }
        public long? TamanoRespaldo { get; set; }
        public int UsuarioRealiza { get; set; }
        public string Observaciones { get; set; }

        // Propiedades de navegación
        public Usuario Usuario { get; set; }
    }
}
