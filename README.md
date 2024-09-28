# Task Management App

Una aplicación de gestión de tareas construida con .NET en el backend y React en el frontend. Esta aplicación permite a los usuarios registrarse, iniciar sesión y gestionar sus tareas de manera eficiente.

## Tecnologías Utilizadas

- **Backend:**
  - .NET 6
  - Entity Framework Core
  - PostgreSQL
  - JWT (Json Web Tokens) para autenticación
  - BCrypt para el hashing de contraseñas

- **Frontend:**
  - React
  - Axios para realizar solicitudes HTTP
  - React Router para la navegación

## Requisitos Previos

- [.NET SDK](https://dotnet.microsoft.com/download) (versión 6 o superior)
- [Node.js](https://nodejs.org/) (versión 14 o superior)
- [PostgreSQL](https://www.postgresql.org/download/) (versión 12 o superior)
  
## Instalación

### Backend

1. Clona el repositorio(Desde rama Master)
   ```bash
   git clone https://github.com/tu_usuario/tasksAppBack.git
   cd tasksAppBack
   
## Restaurar paquetes de Nuget
dotnet restore

## Despliegue de los Modelos en Postgres
dotnet ef database update
