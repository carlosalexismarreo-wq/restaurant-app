namespace RestaurantApp.Models
{
    public class UnidadMedida
    {
        public int UnidadID { get; set; }
        public string NombreUnidad { get; set; }
        public string Abreviatura { get; set; }
        public string Tipo { get; set; } // 'Peso', 'Volumen', 'Unidad'
    }
}
