# Guía de Instalación y Configuración

## Requisitos Previos

1. **Visual Studio 2027** - Con soporte para .NET 8.0
2. **SQL Server** - LocalDB o SQL Server Express
3. **.NET 8.0 Runtime** o superior
4. **Windows 10/11**

## Pasos de Instalación

### 1. Crear la Base de Datos

1. Abre **SQL Server Management Studio**
2. Conecta al servidor local
3. Abre el archivo `Database/01_CreateDatabase.sql`
4. Ejecuta el script para crear la BD y sus tablas

### 2. Configurar Visual Studio

1. Abre la solución en Visual Studio 2027
2. Restaura las dependencias de NuGet:
   - Tools → NuGet Package Manager → Package Manager Console
   - Ejecuta: `dotnet restore`

### 3. Configurar la Conexión a BD

1. Edita `RestaurantApp/App.config`
2. Modifica la cadena de conexión si es necesario:
   ```xml
   <add name="RestaurantDB" connectionString="Server=.\\SQLEXPRESS;Database=RestaurantDB;Integrated Security=true;" />
   ```

3. Opcionales:
   - `RutaRespaldos`: Ruta donde se guardarán los respaldos
   - `NombreImpresora`: Nombre de la impresora térmica
   - `TimeoutConexion`: Timeout en segundos (por defecto 30)

### 4. Compilar y Ejecutar

1. En Visual Studio: **Build → Build Solution** (Ctrl+Shift+B)
2. Presiona **F5** para ejecutar
3. Inicia sesión con:
   - **Usuario**: admin
   - **Contraseña**: admin123

## Estructura del Proyecto

```
RestaurantApp/
├── Models/                 # Clases de entidades (Usuarios, Productos, etc.)
├── Data/
│   └── Repositories/      # Acceso a datos
├── Business/              # Lógica de negocio (Services)
├── UI/
│   ├── Caja/             # Interfaz de Caja
│   ├── Bar/              # Interfaz de Bar
│   ├── Cocina/           # Interfaz de Cocina
│   ├── Almacen/          # Interfaz de Almacén
│   └── Admin/            # Interfaz de Administración
├── Utils/                # Utilidades y helpers
├── Config/               # Configuración
└── App.config            # Archivo de configuración
```

## Credenciales Iniciales

- **Usuario**: admin
- **Contraseña**: admin123
- **Cargo**: Administrador (acceso completo)

## Creación de Usuarios Adicionales

1. Inicia sesión como Administrador
2. Ve a Administración → Usuarios
3. Crea nuevo usuario con su cargo respectivo
4. Los cargos disponibles son:
   - Administrador
   - Cajero
   - Camarero Bar
   - Chef/Cocina
   - Almacenero

## Configuración de Productos y Fichas Técnicas

1. Ve a Administración → Productos
2. Crea categorías de productos (opcional)
3. Crea los productos base (ingredientes)
4. Ve a Administración → Fichas Técnicas
5. Crea fichas técnicas para cada plato/bebida
6. Agrega ingredientes y costos automáticamente

## Uso de Módulos

### Módulo de Caja
- Registrar ventas
- Procesar pagos
- Realizar cierre de caja
- Imprimir tiquets

### Módulo de Bar
- Ver inventario de bar
- Registrar ventas
- Solicitar vales de almacén

### Módulo de Cocina
- Ver inventario de cocina
- Registrar pedidos/producción
- Solicitar vales de almacén

### Módulo de Almacén
- Gestionar inventario central
- Autorizar vales de salida
- Registrar recepciones
- Ajustar inventarios

### Módulo de Administración
- Gestionar usuarios
- Administrar productos y categorías
- Crear fichas técnicas
- Ver reportes de ganancias
- Registrar gastos adicionales
- Realizar respaldos de BD

## Respaldos de Base de Datos

1. Ve a Administración → Respaldos
2. Haz clic en "Realizar Respaldo"
3. El sistema automáticamente:
   - Crea un archivo de respaldo
   - Lo guarda en la ruta configurada
   - Registra la operación en la BD

## Solución de Problemas

### Error de conexión a BD
- Verifica que SQL Server esté corriendo
- Confirma el nombre del servidor en App.config
- Prueba la conexión desde SSMS

### Puerto ocupado
- Cierra Visual Studio
- Limpia la carpeta bin/obj
- Recompila el proyecto

### Problemas con impresora
- Configura el nombre de la impresora en App.config
- Prueba la conexión de la impresora desde Windows
- Verifica los permisos de usuario

## Contacto y Soporte

Para reportar problemas o sugerencias, contacta al administrador del sistema.
