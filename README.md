# SafeVault


El proyecto es una aplicación web desarrollada con Razor Pages sobre .NET 9. Razor Pages es un modelo de programación de ASP.NET Core que facilita la creación de interfaces web dinámicas y estructuradas, separando la lógica de presentación y la lógica de negocio en archivos organizados por página.
Características principales del proyecto:
•	Utiliza el framework .NET 9, aprovechando las últimas mejoras en rendimiento y seguridad.
•	La estructura está basada en páginas Razor (.cshtml y .cshtml.cs), lo que permite un desarrollo modular y fácil de mantener.
•	Incluye soporte para archivos estáticos, configuración centralizada y buenas prácticas de desarrollo web.
•	Es compatible con Visual Studio 2022 o superior, facilitando el desarrollo, depuración y despliegue.
Este enfoque permite construir aplicaciones web modernas, seguras y escalables, ideales para sitios corporativos, portales internos o sistemas de gestión.



El proyecto Razor Pages en .NET 9 implementa funcionalidades de seguridad típicas para aplicaciones web modernas:

•	Autenticación: El proyecto puede estar configurado para requerir que los usuarios se autentiquen antes de acceder a ciertas páginas o recursos. Esto se logra comúnmente mediante cookies, autenticación basada en tokens (como JWT) o integración con proveedores externos (por ejemplo, Microsoft, Google).

•	Autorización: Se pueden definir políticas y roles para restringir el acceso a páginas específicas según el perfil del usuario. Esto asegura que solo los usuarios autorizados puedan realizar ciertas acciones o ver información sensible.

•	Protección de datos: El framework proporciona mecanismos para proteger datos sensibles, como cifrado de cookies y protección contra ataques de falsificación de solicitudes (CSRF).

•	Gestión de sesiones: Se utilizan mecanismos seguros para el manejo de sesiones de usuario, evitando la exposición de información sensible.

•	Buenas prácticas de seguridad: El proyecto aprovecha las características de seguridad integradas en ASP.NET Core, como la validación de entradas, protección contra ataques XSS y CSRF, y el uso de HTTPS por defecto.

Estas funcionalidades ayudan a garantizar que la aplicación sea segura, protegiendo tanto los datos de los usuarios como la integridad del sistema. Si necesitas detalles específicos sobre la configuración de seguridad (por ejemplo, autenticación implementada, políticas de autorización, etc.), indícalo para generar un resumen más detallado.
