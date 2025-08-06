# 📦 Proyecto de Facturación

Este documento explica cómo correr proyecto de facturación en tu entorno local.

---

## ✅ Requisitos Previos

- [.NET SDK](https://dotnet.microsoft.com/download) instalado en tu sistema.
- Una instancia funcional de **SQL Server**.
- Editor de código recomendado: [Visual Studio](https://visualstudio.microsoft.com/) o [Visual Studio Code](https://code.visualstudio.com/).

---

## 🚀 Pasos para Ejecutar el Proyecto
   
1. **Configurar la cadena de conexión**  
   Abre el archivo `appsettings.json` y localiza la sección `ConnectionStrings`. Reemplaza el valor de `DefaultConnection` con los datos de tu servidor SQL. Ejemplo:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=TU_SERVIDOR;Database=TU_BASE_DE_DATOS;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```
   
2. **Aplicar migraciones**  
   Antes de correr el proyecto, aplica las migraciones para crear las tablas necesarias en la base de datos. Abre el Administrador de Consola de Paquetes
   (Package Manager Console) y corre el siguiente comando:

   ```bash
   Update-Database
   ```

3. **Colocar configuración para JWT e integración con contabilidad**  
   Abre el archivo `appsettings.json` y coloca los valores descritos más abajo para cada clave:
   
   ```json
   "JwtSettings": {
     "Secret": "",
     "Issuer": "",
     "Audience": ""
   },
   "AccountingApiUrl": "",
   "ApiKey": ""
   ```
      
   - Secret: Cadena que se usa como clave secreta para firmar y verificar los tokens JWT. Debe ser una cadena larga y aleatoria con mínimo 64 bytes de longitud.
   - Issuer: Cadena que identifica quién emite el token, normalmente el servidor o API. Ejemplo: MyAuthServer.
   - Audience: Cadena que identifica a quién va dirigido el token, es decir, quién debería aceptarlo. Ejemplo: MiApiUsuarios.
   - AccountingApiUrl: Cadena que contienen la url del módulo de contabilidad.
   - ApiKey: Llave utilizada para poder interactuar con el módulo de contabilidad.

4. **Correr el proyecto**  
   Abre la terminal o consola de comandos en la carpeta raíz del proyecto y ejecuta:

   ```bash
   dotnet run
   ```

NOTA: La mayoría de los endpoints requieren de autenticación para poder usarlos, por lo tanto, debes usar primero el endpoint de `api/auth/register`.

## 📚 Acceder a la Documentación del Proyecto
Para acceder a la documentación tienes que agregar `/scalar` al final de la URL local. Por ejemplo:
```bash
http://localhost:5000/scalar
```
🌐 La URL puede variar dependiendo del puerto configurado en launchSettings.json o en tu archivo de configuración del servidor.
