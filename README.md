# 📦 Proyecto de Facturación

Este documento explica cómo correr proyecto de facturación en tu entorno local.

---

## ✅ Requisitos Previos

- [.NET SDK](https://dotnet.microsoft.com/download) instalado en tu sistema.
- Una instancia funcional de **SQL Server**.
- Editor de código recomendado: [Visual Studio](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/).

---

## 🚀 Pasos para Ejecutar el Proyecto

1. **Restaurar las dependencias del proyecto**  
   Abre la terminal o consola de comandos en la carpeta raíz del proyecto y ejecuta:

   ```bash
   dotnet restore
   ```
   
2. **Configurar la cadena de conexión**  
   Abre el archivo `appsettings.json` y localiza la sección `ConnectionStrings`. Reemplaza el valor de `DefaultConnection` con los datos de tu servidor SQL. Ejemplo:

   ```json
   "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=TU_BASE_DE_DATOS;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
   
3. **Aplicar migraciones**  
   Antes de correr el proyecto, aplica las migraciones para crear las tablas necesarias en la base de datos. Abre el Administrador de Consola de Paquetes
   (Package Manager Console) y corre el siguiente comando:

   ```bash
   Update-Database
   ```
   
4. **Correr el proyecto**  
   Abre la terminal o consola de comandos en la carpeta raíz del proyecto y ejecuta:

   ```bash
   dotnet run
   ```

## 📚 Acceder a la Documentación del Proyecto
Para acceder a la documentación tienes que agregar `/scalar` al final de la URL local. Por ejemplo:
```bash
http://localhost:5000/scalar
```
🌐 La URL puede variar dependiendo del puerto configurado en launchSettings.json o en tu archivo de configuración del servidor.
