namespace RestaurantApp.Models
{
    public class ValesSalida
    {
        public string ValeID { get; set; }
        public int NumeroVale { get; set; }
        public DateTime FechaEmision { get; set; }
        public string ModuloDestino { get; set; } // 'Bar', 'Cocina'
        public int UsuarioSolicitante { get; set; }
        public int UsuarioAutoriza { get; set; }
        public string Estado { get; set; } // 'Pendiente', 'Autorizado', 'Entregado'
        public string Observaciones { get; set; }

        // Propiedades de navegación
        public Usuario Solicitante { get; set; }
        public Usuario Autoriza { get; set; }
        public List<DetalleValeSalida> Detalles { get; set; } = new List<DetalleValeSalida>();
    }
}
