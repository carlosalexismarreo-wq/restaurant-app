using System.Configuration;

namespace RestaurantApp.Config
{
    /// <summary>
    /// Gestor de configuración de la aplicación
    /// </summary>
    public class ConfigManager
    {
        private static ConfigManager _instance;

        public string ConexionBD { get; private set; }
        public string RutaRespaldos { get; private set; }
        public string NombreImpresora { get; private set; }
        public int TimeoutConexion { get; private set; }

        private ConfigManager()
        {
            CargarConfiguracion();
        }

        public static ConfigManager Instancia
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ConfigManager();
                }
                return _instance;
            }
        }

        private void CargarConfiguracion()
        {
            ConexionBD = ConfigurationManager.ConnectionStrings["RestaurantDB"]?.ConnectionString ?? "";
            RutaRespaldos = ConfigurationManager.AppSettings["RutaRespaldos"] ?? System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            NombreImpresora = ConfigurationManager.AppSettings["NombreImpresora"] ?? "";
            TimeoutConexion = int.TryParse(ConfigurationManager.AppSettings["TimeoutConexion"], out int timeout) ? timeout : 30;
        }
    }
}
