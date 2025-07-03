# Facturacion
Para poder correr este proyecto debe hacer lo siguiente:

1. Instalar las dependencias con `dotnet restore` usando el CLI dentro de la carpeta raíz del proyecto.
2. Cambiar la propiedad `DefaultConnection` localizada en el `appsettings.json` al string de conexión de la base de datos **SQL server** que vaya a utilizar.
3. Correr el comando `Update-Database` en la consola del administrador de paquetes para aplicar las migraciones de la base de datos.
