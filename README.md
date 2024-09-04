# Freepository5 Backend

## Índice 
- [Developers](#Developers)
- [Descripción](#Descripción)
- [Instalación del proyecto](#Instalación-del-proyecto)
- [Estructura del proyecto](#Estructura-del-proyecto)
- [Tecnologías](#Tecnologías)
- [Convenciones del proyecto](#Convenciones-del-proyecto)
- [Testing](#Testing)

## Developers
- [Alexandr Chichizola](https://github.com/chichilahore)
- [Adi Radoine](https://github.com/AdiyieR26)
- [Santi Agudo Martínez](https://github.com/Santiiii12)
- [Oscar Herencia Sakkis](https://github.com/oscarh9)
- [Tecla Raimondo](https://github.com/teclir)


## Descripción
Este proyecto tiene como objetivo gestionar los recursos que el usuario sube a la aplicación, gracias a una base de datos que le permite crear, visualizar, editar y eliminar los mismos, pudiendo
añadir a cada recurso una o varias etiquetas, además de poder filtrar por etiquetas de tecnologías gracias a un buscador.

<b>Funcionalidades principales</b>:

- CRUD: Creación, visualización, edición y eliminación de recursos, bootcamps, programas y etiquetas.

- Login/Register: Implementación de las funciones de registro y acceso del usuario a la aplicación.
- Autenticación y Autorización: Autenticación y autorización JWT mediante Bearer Tokens e Identity Core.
- Buscador de etiquetas: Implementación de un buscador de recursos por etiquetas. 

<b>Objetivo del proyecto</b>:
<br>
A través de este proyecto, buscamos entregar una herramienta útil, eficaz e intuitiva para los formadores, que permita gestionar y acceder fácilmente a todos los recursos de las escuelas mediante un sistema organizado y un buscador efciente.

## Enlace al repositorio:
https://github.com/freepository5/freepository5Back


## Instalación del proyecto
1. Clona el repositorio: https://github.com/freepository5/freepository5Back
2. Verifica que tengas instalados los NuGets necesarios: 
- Microsoft.AspNetCore.Authentication.JwtBearer 8.0.7
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore 8.0.7
- Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.7
- Microsoft.AspNetCore.Identity.UI 8.0.7
- Microsoft.AspNetCore.Mvc.NewtonsoftJson 8.0.7
- Microsoft.AspnetCore.Mvc.Razor.RuntimeCompilation 8.0.7
- Microsoft.AspNetCore.OpenApi 8.0.7
- Microsoft.EntityFrameworkCore 8.0.7
- Microsoft.EntityFrameworkCore.Design 8.0.7
- Microsoft.EntityFrameworkCore.SqlServer 8.0.7
- Microsoft.EntityFrameworkCore.Tools 8.0.7
- Microsoft.VisualStudio.Web.CodeGeneration.Design 8.0.3
- AutoMapper
- Swashbuckle.AspNetCore 6.4.0
- System.Text.Json 8.0.4
- Microsoft.IdentityModel.Tokens 8.0.1
3. Crea una base de datos SQLServer y conéctala con tu proyecto.
4. Haz una migración para crear tus tablas.
5. Crea una nueva rama: _'git checkout -b feature-name'_.
6. Haz tus cambios.
7. Haz push de tu rama: _'git push origin feature-name'_.
8. Haz un pull request.


## Estructura del proyecto
<pre>
├── freepository5Back
├── Freepository
|   ├── bin
│   ├── Controllers
│   ├── Data
│   ├── DTO's
│   ├── Migrations
│   ├── Models
│   ├── obj
│   ├── Properties
│   ├── Repositories
│   ├── Services
│   └── Utilities
├── appsettings.json
├── appsettings.Development.json
├── .dockerignore
├── Dockerfile
├── Freepository.csproj
├── Freepository.http
├── Program.cs
├── .dockerignore
├── .gitignore
├── docker-compose.yml
├── Freepository.sln
├── README.md
├── Scratches and Consoles
├── .idea
</pre>


- **Controllers/:** Contiene los controladores de las entidades.
- **Data/:** Contiene el archivo DbContext para la gestión de la conexión a la base datos.
- **DTO's/:** Contiene las clases necesarias para la transferencia de datos entre el cliente y el servidor.
- **Migrations/:** Contiene las migraciones de las entidades.
- **Models/:** Contiene las entidades de la aplicación.
- **Repositories/:** Contiene los archivos de interfaces y las clases que implementan las interfaces de repositorio, proporcionando la lógica específica para interactuar con la base de datos.
- **Services/:** Contiene los servicios responsables de gestionar la generación, validación y administración de tokens de autenticación.
- **Utilities/:** Contiene el archivo AutoMapperProfiles, utilizado para configurar mapeos entre diferentes modelos de datos.

## Tecnologías
- C#
- .NET
- ASP.NET Core
- Entity Framework Core
- SQL Server
- IdentityServer
- Docker
- Azure Data Studio
- XUnit (Testing)

## Convenciones del proyecto
- Naming en inglés.
- Uso de Pascal Case.
- Uso de Cammel Case para variables.


## Distribución de tareas
Para organizar y gestionar nuestro proyecto, hemos trabajado con la metodología agile y el marco de trabajo Kanban, que nos ha ayudado a distribuir y visualizar el flujo de trabajo entre el equipo de front y de back en nuestro tablero de [Trello](https://trello.com/b/JSj5vGyi/freepository5).

## Pendientes para futuros Sprints
Somos conscientes del gran potencial que tiene nuestro proyecto actual para la escuela de expandirse y mejorar. 
Aunque ya hemos alcanzado importantes hitos, sabemos que hay muchas otras funcionalidades que podrían enriquecer aún más nuestro producto y que no se han 
podido implementar hasta el momento debido a restricciones de tiempo. Sin embargo, nuestro objetivo es la mejora continua y planeamos integrar estas nuevas
características en un futuro: 

- Implementación de roles de usuario: que el admin pueda añadir nuevos bootcamps.
- Potenciar el buscador: búsquedas por nombre, categoría o descripciones.
- Añadir niveles a las etiquetas (nivel básico, intermedio, avanzado).

## Testing
Hemos realizado el testing de la parte principal de la aplicación, que incluye los controladores y repositorios de las entidades Resource y Tag. 
Queremos seguir testeando la aplicación para que sea 100% funcional.

Para correr los tests realizados, asegúrate de tener los siguientes NuGets instalados: 
 - Moq 4.20.0
 - xunit 2.5.0
 - xunit.runner.visualstudio 2.5.0
 - Microsoft.EntityFrameworkCore.InMemory 8.0.7
 - Microsoft.NET.test.Sdk 17.10.0






