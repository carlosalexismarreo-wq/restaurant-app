-- ===================================================================
-- SCRIPT DE CREACIÓN DE BASE DE DATOS - SISTEMA DE GESTIÓN RESTAURANTE
-- ===================================================================

USE master;
GO

-- Eliminar BD si existe
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'RestaurantDB')
BEGIN
    ALTER DATABASE RestaurantDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE RestaurantDB;
END
GO

-- Crear base de datos
CREATE DATABASE RestaurantDB;
GO

USE RestaurantDB;
GO

-- ===================================================================
-- TABLA: USUARIOS
-- ===================================================================
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contraseña VARCHAR(255) NOT NULL,
    NombreCompleto VARCHAR(100) NOT NULL,
    CargoID INT NOT NULL,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaUltimaModificacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ===================================================================
-- TABLA: CARGOS (Roles/Permisos)
-- ===================================================================
CREATE TABLE Cargos (
    CargoID INT PRIMARY KEY IDENTITY(1,1),
    NombreCargo VARCHAR(50) NOT NULL UNIQUE,
    Descripcion VARCHAR(250),
    AccesoCaja BIT NOT NULL DEFAULT 0,
    AccesoBar BIT NOT NULL DEFAULT 0,
    AccesoCocina BIT NOT NULL DEFAULT 0,
    AccesoAlmacen BIT NOT NULL DEFAULT 0,
    AccesoAdmin BIT NOT NULL DEFAULT 0,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ===================================================================
-- TABLA: UNIDADES DE MEDIDA
-- ===================================================================
CREATE TABLE UnidadesMedida (
    UnidadID INT PRIMARY KEY IDENTITY(1,1),
    NombreUnidad VARCHAR(50) NOT NULL UNIQUE,
    Abreviatura VARCHAR(10) NOT NULL,
    Tipo VARCHAR(20) NOT NULL
);
GO

-- ===================================================================
-- TABLA: MONEDAS
-- ===================================================================
CREATE TABLE Monedas (
    MonedaID INT PRIMARY KEY IDENTITY(1,1),
    Codigo VARCHAR(3) NOT NULL UNIQUE,
    Nombre VARCHAR(50) NOT NULL,
    Simbolo VARCHAR(5) NOT NULL,
    TasaCambio DECIMAL(10,4) NOT NULL DEFAULT 1.0,
    Activa BIT NOT NULL DEFAULT 1
);
GO

-- ===================================================================
-- TABLA: CATEGORÍAS DE PRODUCTOS
-- ===================================================================
CREATE TABLE CategoriaProductos (
    CategoriaID INT PRIMARY KEY IDENTITY(1,1),
    NombreCategoria VARCHAR(100) NOT NULL UNIQUE,
    Descripcion VARCHAR(250),
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ===================================================================
-- TABLA: PRODUCTOS
-- ===================================================================
CREATE TABLE Productos (
    ProductoID INT PRIMARY KEY IDENTITY(1,1),
    CodigoProducto VARCHAR(50) NOT NULL UNIQUE,
    NombreProducto VARCHAR(100) NOT NULL,
    CategoriaID INT NOT NULL,
    UnidadMedidaID INT NOT NULL,
    PrecioCosto DECIMAL(10,2) NOT NULL,
    PrecioVenta DECIMAL(10,2) NOT NULL,
    MonedaID INT NOT NULL,
    StockActual DECIMAL(10,3) NOT NULL DEFAULT 0,
    StockMinimo DECIMAL(10,3) NOT NULL DEFAULT 0,
    Activo BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (CategoriaID) REFERENCES CategoriaProductos(CategoriaID),
    FOREIGN KEY (UnidadMedidaID) REFERENCES UnidadesMedida(UnidadID),
    FOREIGN KEY (MonedaID) REFERENCES Monedas(MonedaID)
);
GO

-- ===================================================================
-- TABLA: FICHAS TÉCNICAS
-- ===================================================================
CREATE TABLE FichasTecnicas (
    FichaTecnicaID INT PRIMARY KEY IDENTITY(1,1),
    ProductoID INT NOT NULL,
    NombreFicha VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    CostoTotal DECIMAL(10,2) NOT NULL,
    PorcentajeGanancia DECIMAL(5,2) NOT NULL,
    PrecioFinal DECIMAL(10,2) NOT NULL,
    MonedaID INT NOT NULL,
    Activa BIT NOT NULL DEFAULT 1,
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
    FechaModificacion DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    FOREIGN KEY (MonedaID) REFERENCES Monedas(MonedaID)
);
GO

-- ===================================================================
-- TABLA: DETALLE FICHAS TÉCNICAS
-- ===================================================================
CREATE TABLE DetalleFichasTecnicas (
    DetalleID INT PRIMARY KEY IDENTITY(1,1),
    FichaTecnicaID INT NOT NULL,
    ProductoIngredienteID INT NOT NULL,
    CantidadUtilizada DECIMAL(10,3) NOT NULL,
    UnidadMedidaID INT NOT NULL,
    CostoUnitario DECIMAL(10,2) NOT NULL,
    CostoTotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (FichaTecnicaID) REFERENCES FichasTecnicas(FichaTecnicaID),
    FOREIGN KEY (ProductoIngredienteID) REFERENCES Productos(ProductoID),
    FOREIGN KEY (UnidadMedidaID) REFERENCES UnidadesMedida(UnidadID)
);
GO

-- ===================================================================
-- TABLA: INVENTARIOS
-- ===================================================================
CREATE TABLE Inventarios (
    InventarioID INT PRIMARY KEY IDENTITY(1,1),
    ProductoID INT NOT NULL,
    Modulo VARCHAR(50) NOT NULL,
    CantidadActual DECIMAL(10,3) NOT NULL DEFAULT 0,
    UnidadMedidaID INT NOT NULL,
    MonedaID INT NOT NULL,
    FechaActualizacion DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    FOREIGN KEY (UnidadMedidaID) REFERENCES UnidadesMedida(UnidadID),
    FOREIGN KEY (MonedaID) REFERENCES Monedas(MonedaID)
);
GO

-- ===================================================================
-- TABLA: MOVIMIENTOS DE INVENTARIO
-- ===================================================================
CREATE TABLE MovimientosInventario (
    MovimientoID INT PRIMARY KEY IDENTITY(1,1),
    ProductoID INT NOT NULL,
    TipoMovimiento VARCHAR(50) NOT NULL,
    Cantidad DECIMAL(10,3) NOT NULL,
    UnidadMedidaID INT NOT NULL,
    Modulo VARCHAR(50) NOT NULL,
    ValeID VARCHAR(50),
    InformeRecepcionID VARCHAR(50),
    Observaciones VARCHAR(500),
    UsuarioID INT NOT NULL,
    FechaMovimiento DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (UnidadMedidaID) REFERENCES UnidadesMedida(UnidadID)
);
GO

-- ===================================================================
-- TABLA: VALES DE SALIDA
-- ===================================================================
CREATE TABLE ValesSalida (
    ValeID VARCHAR(50) PRIMARY KEY,
    NumeroVale INT NOT NULL UNIQUE,
    FechaEmision DATETIME NOT NULL DEFAULT GETDATE(),
    ModuloDestino VARCHAR(50) NOT NULL,
    UsuarioSolicitante INT NOT NULL,
    UsuarioAutoriza INT NOT NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    Observaciones VARCHAR(500),
    FOREIGN KEY (UsuarioSolicitante) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (UsuarioAutoriza) REFERENCES Usuarios(UsuarioID)
);
GO

-- ===================================================================
-- TABLA: DETALLE VALES SALIDA
-- ===================================================================
CREATE TABLE DetalleValesSalida (
    DetalleValeID INT PRIMARY KEY IDENTITY(1,1),
    ValeID VARCHAR(50) NOT NULL,
    ProductoID INT NOT NULL,
    CantidadSolicitada DECIMAL(10,3) NOT NULL,
    CantidadEntregada DECIMAL(10,3),
    UnidadMedidaID INT NOT NULL,
    FOREIGN KEY (ValeID) REFERENCES ValesSalida(ValeID),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    FOREIGN KEY (UnidadMedidaID) REFERENCES UnidadesMedida(UnidadID)
);
GO

-- ===================================================================
-- TABLA: INFORMES DE RECEPCIÓN
-- ===================================================================
CREATE TABLE InformesRecepcion (
    InformeRecepcionID VARCHAR(50) PRIMARY KEY,
    NumeroInforme INT NOT NULL UNIQUE,
    FechaRecepcion DATETIME NOT NULL DEFAULT GETDATE(),
    ProveedorID INT,
    UsuarioRecibe INT NOT NULL,
    UsuarioAutoriza INT NOT NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Pendiente',
    Observaciones VARCHAR(500),
    FOREIGN KEY (UsuarioRecibe) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (UsuarioAutoriza) REFERENCES Usuarios(UsuarioID)
);
GO

-- ===================================================================
-- TABLA: DETALLE INFORMES DE RECEPCIÓN
-- ===================================================================
CREATE TABLE DetalleInformesRecepcion (
    DetalleInformeID INT PRIMARY KEY IDENTITY(1,1),
    InformeRecepcionID VARCHAR(50) NOT NULL,
    ProductoID INT NOT NULL,
    CantidadRecibida DECIMAL(10,3) NOT NULL,
    UnidadMedidaID INT NOT NULL,
    PrecioCosto DECIMAL(10,2) NOT NULL,
    CostoTotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (InformeRecepcionID) REFERENCES InformesRecepcion(InformeRecepcionID),
    FOREIGN KEY (ProductoID) REFERENCES Productos(ProductoID),
    FOREIGN KEY (UnidadMedidaID) REFERENCES UnidadesMedida(UnidadID)
);
GO

-- ===================================================================
-- TABLA: VENTAS
-- ===================================================================
CREATE TABLE Ventas (
    VentaID VARCHAR(50) PRIMARY KEY,
    NumeroVenta INT NOT NULL UNIQUE,
    FechVenta DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioID INT NOT NULL,
    TipoVenta VARCHAR(20) NOT NULL,
    MonedaID INT NOT NULL,
    MontoTotal DECIMAL(10,2) NOT NULL,
    Descuento DECIMAL(10,2) NOT NULL DEFAULT 0,
    MontoNeto DECIMAL(10,2) NOT NULL,
    FormaPago VARCHAR(50) NOT NULL,
    Estado VARCHAR(20) NOT NULL DEFAULT 'Completada',
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (MonedaID) REFERENCES Monedas(MonedaID)
);
GO

-- ===================================================================
-- TABLA: DETALLE VENTAS
-- ===================================================================
CREATE TABLE DetalleVentas (
    DetalleVentaID INT PRIMARY KEY IDENTITY(1,1),
    VentaID VARCHAR(50) NOT NULL,
    FichaTecnicaID INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(10,2) NOT NULL,
    PrecioTotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (VentaID) REFERENCES Ventas(VentaID),
    FOREIGN KEY (FichaTecnicaID) REFERENCES FichasTecnicas(FichaTecnicaID)
);
GO

-- ===================================================================
-- TABLA: CIERRES DE CAJA
-- ===================================================================
CREATE TABLE CierresCaja (
    CierreID VARCHAR(50) PRIMARY KEY,
    NumeroCierre INT NOT NULL UNIQUE,
    FechaCierre DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioID INT NOT NULL,
    SaldoInicial DECIMAL(10,2) NOT NULL,
    TotalVentas DECIMAL(10,2) NOT NULL,
    TotalEgresos DECIMAL(10,2) NOT NULL DEFAULT 0,
    SaldoFinal DECIMAL(10,2) NOT NULL,
    Diferencia DECIMAL(10,2) NOT NULL,
    Observaciones VARCHAR(500),
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID)
);
GO

-- ===================================================================
-- TABLA: GASTOS
-- ===================================================================
CREATE TABLE Gastos (
    GastoID INT PRIMARY KEY IDENTITY(1,1),
    FechaGasto DATETIME NOT NULL DEFAULT GETDATE(),
    UsuarioID INT NOT NULL,
    Descripcion VARCHAR(500) NOT NULL,
    Monto DECIMAL(10,2) NOT NULL,
    MonedaID INT NOT NULL,
    CategoriaGasto VARCHAR(100),
    EsContabilizado BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (UsuarioID) REFERENCES Usuarios(UsuarioID),
    FOREIGN KEY (MonedaID) REFERENCES Monedas(MonedaID)
);
GO

-- ===================================================================
-- TABLA: REPORTES DE GANANCIA DIARIA
-- ===================================================================
CREATE TABLE ReportesGanancia (
    ReporteID INT PRIMARY KEY IDENTITY(1,1),
    Fecha DATE NOT NULL UNIQUE,
    TotalVentasUSD DECIMAL(10,2) NOT NULL DEFAULT 0,
    TotalVentasCUP DECIMAL(10,2) NOT NULL DEFAULT 0,
    TotalCostosUSD DECIMAL(10,2) NOT NULL DEFAULT 0,
    TotalCostosCUP DECIMAL(10,2) NOT NULL DEFAULT 0,
    TotalGastosUSD DECIMAL(10,2) NOT NULL DEFAULT 0,
    TotalGastosCUP DECIMAL(10,2) NOT NULL DEFAULT 0,
    GananciaNetaUSD DECIMAL(10,2) NOT NULL,
    GananciaNetaCUP DECIMAL(10,2) NOT NULL,
    FechaGeneracion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- ===================================================================
-- TABLA: RESPALDOS DE BASE DE DATOS
-- ===================================================================
CREATE TABLE RespaldosDB (
    RespaldoID INT PRIMARY KEY IDENTITY(1,1),
    FechaRespaldo DATETIME NOT NULL DEFAULT GETDATE(),
    RutaRespaldo VARCHAR(500) NOT NULL,
    TamanoRespaldo BIGINT,
    UsuarioRealiza INT NOT NULL,
    Observaciones VARCHAR(500),
    FOREIGN KEY (UsuarioRealiza) REFERENCES Usuarios(UsuarioID)
);
GO

-- ===================================================================
-- CREAR ÍNDICES
-- ===================================================================
CREATE INDEX IX_Usuarios_NombreUsuario ON Usuarios(NombreUsuario);
CREATE INDEX IX_Productos_Codigo ON Productos(CodigoProducto);
CREATE INDEX IX_Productos_Categoria ON Productos(CategoriaID);
CREATE INDEX IX_Inventarios_Producto ON Inventarios(ProductoID);
CREATE INDEX IX_MovimientosInventario_Fecha ON MovimientosInventario(FechaMovimiento);
CREATE INDEX IX_Ventas_Fecha ON Ventas(FechVenta);
CREATE INDEX IX_Ventas_Usuario ON Ventas(UsuarioID);
CREATE INDEX IX_CierresCaja_Fecha ON CierresCaja(FechaCierre);
CREATE INDEX IX_Gastos_Fecha ON Gastos(FechaGasto);
CREATE INDEX IX_ReportesGanancia_Fecha ON ReportesGanancia(Fecha);

GO

-- ===================================================================
-- INSERTAR DATOS INICIALES
-- ===================================================================

-- Monedas
INSERT INTO Monedas (Codigo, Nombre, Simbolo, TasaCambio, Activa)
VALUES
    ('USD', 'Dólar Estadounidense', '$', 1.0, 1),
    ('CUP', 'Peso Cubano', '₱', 25.0, 1);

-- Unidades de Medida
INSERT INTO UnidadesMedida (NombreUnidad, Abreviatura, Tipo)
VALUES
    ('Kilogramo', 'kg', 'Peso'),
    ('Libra', 'lb', 'Peso'),
    ('Gramo', 'g', 'Peso'),
    ('Unidad', 'un', 'Unidad'),
    ('Litro', 'L', 'Volumen'),
    ('Mililitro', 'ml', 'Volumen');

-- Cargos
INSERT INTO Cargos (NombreCargo, Descripcion, AccesoCaja, AccesoBar, AccesoCocina, AccesoAlmacen, AccesoAdmin)
VALUES
    ('Administrador', 'Acceso completo', 1, 1, 1, 1, 1),
    ('Cajero', 'Acceso a caja', 1, 0, 0, 0, 0),
    ('Camarero Bar', 'Acceso a bar', 0, 1, 0, 0, 0),
    ('Chef/Cocina', 'Acceso a cocina', 0, 0, 1, 0, 0),
    ('Almacenero', 'Acceso a almacén', 0, 0, 0, 1, 0);

-- Usuario Administrador por defecto (contraseña: admin123 - SHA256)
INSERT INTO Usuarios (NombreUsuario, Contraseña, NombreCompleto, CargoID, Activo)
VALUES
    ('admin', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'Administrador Sistema', 1, 1);

GO

PRINT 'Base de datos RestaurantDB creada exitosamente.';
