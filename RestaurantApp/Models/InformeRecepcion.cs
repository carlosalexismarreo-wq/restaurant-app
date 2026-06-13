namespace RestaurantApp.Models
{
    public class InformeRecepcion
    {
        public string InformeRecepcionID { get; set; }
        public int NumeroInforme { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public int? ProveedorID { get; set; }
        public int UsuarioRecibe { get; set; }
        public int UsuarioAutoriza { get; set; }
        public string Estado { get; set; } // 'Pendiente', 'Autorizado'
        public string Observaciones { get; set; }

        // Propiedades de navegación
        public Usuario Recibe { get; set; }
        public Usuario Autoriza { get; set; }
        public List<DetalleInformeRecepcion> Detalles { get; set; } = new List<DetalleInformeRecepcion>();
    }
}
