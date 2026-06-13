# 🍽️ Sistema de Gestión de Restaurante - Visual Studio 2027

## Descripción General
Aplicación Windows Forms desarrollada en Visual Studio 2027 para la gestión integral de restaurantes con 4 módulos principales.

## 📦 Módulos del Sistema
1. **Módulo de Caja** - Gestión de ventas y cierre de caja
2. **Módulo de Bar** - Control de inventarios y ventas de bar
3. **Módulo de Cocina** - Control de inventarios y preparación de platos
4. **Módulo de Almacén** - Gestión centralizada de inventarios

## ✨ Funcionalidades Clave

### Usuarios y Permisos
- Administrador con control total
- Control de acceso según cargo
- 5 tipos de cargos predefinidos
- Gestión de usuarios activos e inactivos

### Fichas Técnicas
- Creación de fichas técnicas para productos
- Cálculo automático de costos de ingredientes
- Porcentaje de ganancia configurable
- Precio final automático

### Inventarios
- Control por módulo (Bar, Cocina, Almacén)
- Múltiples unidades de medida (kg, lb, g, unidad, L, ml)
- Movimientos rastreados (Entrada, Salida, Ajuste)
- Vales de salida con autorización
- Informes de recepción

### Monedas
- Soporte dual: USD y CUP
- Tasa de cambio configurable
- Conversión automática en reportes

### Ventas
- Registro de ventas por tipo (Bar, Cocina, Mostrador)
- Cálculo automático de costos según ficha técnica
- Descuentos aplicables
- Múltiples formas de pago

### Reportes
- Ganancia diaria por moneda
- Cierre de caja automático
- Cálculo de ganancias netas
- Gastos adicionales no contabilizados en ficha

### Impresión
- Tiquets de venta desde todos los módulos
- Formato configurable
- Impresora térmica compatible

### Respaldo
- Respaldo automático de BD al cierre diario
- Historial de respaldos
- Registro de usuario que realiza respaldo

## 🛠️ Requisitos Técnicos

- **Visual Studio 2027**
- **Windows Forms (.NET 8.0+)**
- **SQL Server Local (LocalDB o Express)**
- **Windows 10 o superior**

## 🚀 Instalación

1. **Crear BD SQL Server Local**
   - Ejecutar script en `Database/01_CreateDatabase.sql`

2. **Visual Studio 2027**
   - Crear nuevo proyecto Windows Forms
   - Configurar App.config

3. **Compilar y ejecutar**
   - Build -> Build Solution
   - F5 para ejecutar

## 📝 Credenciales por Defecto

- **Usuario**: admin
- **Contraseña**: admin123
