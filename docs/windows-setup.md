# Configurar la CLI de .NET y LibMan en Windows

Sigue estos pasos para habilitar la CLI de .NET y restaurar las dependencias del proyecto **ServicioAhora!** en Windows.

## 1) Instala el SDK de .NET

La forma más rápida en Windows 10/11 es con **winget** (requiere tener la Microsoft Store activa):

```powershell
winget install Microsoft.DotNet.SDK.7
```

Alternativa: descarga el instalador (x64) desde <https://dotnet.microsoft.com/en-us/download> y sigue el asistente.

> El SDK trae la CLI `dotnet`. Verifica la instalación:
>
> ```powershell
> dotnet --info
> ```

## 2) Instala la CLI de LibMan (librerías cliente)

El proyecto usa LibMan para Bootstrap y otros recursos. Instala la herramienta global:

```powershell
dotnet tool install -g Microsoft.Web.LibraryManager.CLI
```

Reinicia la terminal o asegúrate de que la ruta de herramientas (`%USERPROFILE%\.dotnet\tools`) esté en `PATH`.

## 3) Restaura y compila el proyecto

Desde la raíz del repo (`ECommerceFrontend.sln`):

```powershell
cd front-servicios-prueba
libman restore
dotnet restore
dotnet build
```

## 4) Ejecuta la aplicación

Usa el proyecto Blazor WebAssembly como punto de entrada:

```powershell
dotnet run --project BlazorWasm
```

- La app quedará sirviendo en `http://localhost:5000` (o `https://localhost:5001` si se habilita HTTPS).
- Para desarrollo en caliente, puedes usar `dotnet watch --project BlazorWasm run`.

## 5) Errores comunes

- **"dotnet" no se reconoce**: abre una nueva terminal o agrega `%USERPROFILE%\.dotnet\` y `%USERPROFILE%\.dotnet\tools` a la variable de entorno `PATH`.
- **LibMan no se encuentra**: ejecuta `dotnet tool restore` si se registró en el manifiesto local, o reinstala con el comando del paso 2.
- **Puertos ocupados**: ajusta el puerto con `dotnet run --urls http://localhost:PORT --project BlazorWasm`.
