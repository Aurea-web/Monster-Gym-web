# MonsterGym

Proyecto ASP.NET Core MVC basado en la estructura de `PlantillaDePagos`, adaptado a las seis clases indicadas en `Classes.docx`.

## Clases
- Cliente
- Membresia
- Contrato
- Empleado
- Cargo
- Pago

## Estructura
Cada clase tiene su propio `Controller` y su carpeta de `Views` con:
- `Index.cshtml`
- `Create.cshtml`
- `Edit.cshtml`

No se incluye ninguna vista/acción de eliminación. Las acciones de las tablas usan el botón **Editar**.

## Base de datos
La conexión se encuentra en `MonsterGym/appsettings.json` con la base `MonsterGymDb`. El proyecto ejecuta `EnsureCreated()` al iniciar para una base nueva.

## Ejecución
Abrir `MonsterGym.slnx` en Visual Studio y ejecutar el perfil HTTPS. El proyecto conserva ASP.NET Core MVC, Entity Framework Core y Bootstrap del proyecto plantilla.
