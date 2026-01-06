# 🌟 API REST Test – .NET 8
API REST desarrollada en **.NET 8** como prueba técnica.  
Implementa autenticación con JWT, persistencia en base de datos en memoria, validaciones de usuario y consumo de una API externa.

---

## Tecnologías utilizadas

- .NET 8
- ASP.NET Core Web API
- JWT Authentication
- BCrypt.Net
- FluentValidation
- HttpClient
- Swagger / OpenAPI
- Base de datos en memoria

---

## Cómo ejecutar el proyecto

Desde la raíz de la solución:

```bash
dotnet restore
dotnet run --project Api
```
La API quedará disponible en:
```bash
http://localhost:5099
```

---

## Autenticación
La API utiliza JWT para proteger endpoints.

El token se genera al:
  - Registrar un usuario.
  - Iniciar sesión.

El token debe enviarse en el header:
```bash
Authorization: Bearer {token}
```

---

## Endpoints de Usuarios

**Registrar usuario**

POST **/api/users/register**

Valida:
  - Nombre no vacío
  - Correo válido
  - Contraseña segura (mayúsculas, minúsculas, símbolos y más de 8 caracteres)
  - Correo no registrado
    
Retorna:
  - Identificador único del usuario
  - Nombre
  - Correo
  - Token JWT

##

**Login**

POST **/api/users/login**

Recibe:

  - Email
  - Password
    
Retorna:
  - Token JWT

---

## Endpoints de Posts (protegidos con JWT)
Estos endpoints requieren un token JWT válido.
En caso contrario, retornan HTTP 401.

  - **Obtener posts** GET **/api/posts**

  - **Crear post** POST **/api/posts**

Consumen la API externa:
```bash
https://jsonplaceholder.typicode.com/posts
```

---

## Configuración
La clave JWT y la URL del servicio externo se configuran en el archivo:
```bash
appsettings.json
```

---

## Notas
  - La base de datos utilizada es InMemory.
  - La información se pierde al detener la aplicación.
  - Proyecto organizado en capas:
      - Api
      - Application
      - Domain
      - Infrastructure
