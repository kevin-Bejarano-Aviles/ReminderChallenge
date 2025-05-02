# Kevin Assessment

## Introducción

---

## Arquitectura del Proyecto

Se utilizó una **arquitectura por capas**, compuesta por:

- **Capa API:** Encargada del manejo HTTP, rutas y controladores.
- **Capa de Servicios:** Contiene la lógica de negocio.
- **Capa de Repositorio:** Gestión de acceso y persistencia en base de datos.

---

## Ejecución del Proyecto

Para ejecutar la solución:

1. Clonar o descargar el repositorio.
2. Abrir la solución en Visual Studio y ejecutarla desde allí (recomendado), o bien:
3. Desde la consola, ir al proyecto `ReminderChallenge.API` y ejecutar:

   ```bash
   dotnet run

## Base de Datos
Se utilizó SQLite, se considero Docker con sql Server pero se opto por algo mas liviano

Para visualizar la base de datos, se puede utilizar un gestor como TablePlus. Solo se debe abrir el archivo .db que se encuentra en el proyecto API.

## Configuración (appsettings.json)
Parámetros configurables:

1. Intervalo en horas para la revisión periódica de recordatorios.

2. Cantidad de días antes del vencimiento de un recordatorio.

3. Configuración de envío de correos vía SMTP (Host, Port, User, Password, From, To).

4. Tópico para el envío de notificaciones push.

## Implementación de Firebase
Para que la aplicación pueda enviar notificaciones push a la app de administradores:

1. Descargar el archivo de configuración JSON de Firebase desde el proyecto a conectar.
 
2. Colocar el archivo dentro de src/Resources/ con el nombre:

```
firebase-credentials.json
```

Si ocurre un error de conexión con Firebase, se registrarán mensajes en los logs.

## Configuración SMTP
Si ya dispone de un servidor SMTP, puede configurar sus credenciales en appsettings.json.

Ejemplo de configuración usada para pruebas:
```
"MailSettings": {
  "From": "hello@demomailtrap.co",
  "To": "yourMail",
  "SmtpHost": "live.smtp.mailtrap.io",
  "Port": 587,
  "User": "smtp@mailtrap.io",
  "Password": "password",
  "EnableSsl": true
}

```


## CRUD y Servicio en Segundo Plano
La aplicación incluye un mini CRUD para gestionar recordatorios mediante endpoints.

1. Se implementa un BackgroundService que:

2. Revisa periódicamente los recordatorios próximos a vencer o ya vencidos.

3. Envía un correo por cada recordatorio encontrado.

4. Emite notificaciones push asociadas.