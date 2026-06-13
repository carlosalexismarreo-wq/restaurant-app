namespace RestaurantApp.Utils
{
    /// <summary>
    /// Constantes del sistema
    /// </summary>
    public class Constants
    {
        // Módulos
        public const string MODULO_CAJA = "Caja";
        public const string MODULO_BAR = "Bar";
        public const string MODULO_COCINA = "Cocina";
        public const string MODULO_ALMACEN = "Almacen";

        // Tipos de movimiento
        public const string MOVIMIENTO_ENTRADA = "Entrada";
        public const string MOVIMIENTO_SALIDA = "Salida";
        public const string MOVIMIENTO_AJUSTE = "Ajuste";

        // Estados
        public const string ESTADO_PENDIENTE = "Pendiente";
        public const string ESTADO_AUTORIZADO = "Autorizado";
        public const string ESTADO_ENTREGADO = "Entregado";
        public const string ESTADO_COMPLETADA = "Completada";
        public const string ESTADO_CANCELADA = "Cancelada";

        // Tipos de venta
        public const string TIPO_VENTA_BAR = "Bar";
        public const string TIPO_VENTA_COCINA = "Cocina";
        public const string TIPO_VENTA_MOSTRADOR = "Mostrador";

        // Formas de pago
        public const string FORMA_PAGO_EFECTIVO = "Efectivo";
        public const string FORMA_PAGO_TARJETA = "Tarjeta";
        public const string FORMA_PAGO_MIXTO = "Mixto";

        // Monedas
        public const int MONEDA_USD = 1;
        public const int MONEDA_CUP = 2;

        // Categorías de gastos
        public const string CATEGORIA_MANTENIMIENTO = "Mantenimiento";
        public const string CATEGORIA_SERVICIOS = "Servicios";
        public const string CATEGORIA_UTILES = "Útiles";
        public const string CATEGORIA_OTROS = "Otros";

        // Unidades de medida
        public const int UNIDAD_KILOGRAMO = 1;
        public const int UNIDAD_LIBRA = 2;
        public const int UNIDAD_GRAMO = 3;
        public const int UNIDAD_UNIDAD = 4;
        public const int UNIDAD_LITRO = 5;
        public const int UNIDAD_MILILITRO = 6;
    }
}
