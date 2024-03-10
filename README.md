# Documentaci�n de la API Rest de Control Gastos con .NET Core C#

Bienvenido a la documentaci�n de la API Rest de Control Gastos. Esta API proporciona funcionalidades para administrar gastos personales.

## Instalaci�n y Requisitos

Para utilizar esta API, aseg�rate de tener instalado:

- .NET Core SDK
- Visual Studio 2022 o Visual Studio Code
- SQL Server

## Autenticaci�n

La API utiliza autenticaci�n basada en tokens JWT (JSON Web Token). Para acceder a los endpoints protegidos, debes incluir el token de acceso en el encabezado de autorizaci�n de la solicitud HTTP.

## Endpoints

### 1. Registro de usuario

#### Descripci�n

Este endpoint permite registrar un nuevo usuario en el sistema.

#### M�todo HTTP

`POST`

#### Ruta
`/api/Authentication/register`

#### Cuerpo de la solicitud

```json
{
  "password": "string",
  "correo": "string",
  "primerNombre": "string",
  "segundoNombre": "string"
}
```
#### C�digos de respuesta

- `201 ok`: El usuario se ha registrado correctamente.
- `400 Bad Request`: La solicitud contiene datos inv�lidos.
- `409 Conflict`: El correo electr�nico ya est� en uso.
- `500 Internal Server Error`: Error interno del servidor.

### 2. Login

#### Descripci�n

Este endpoint permite que un usuario registrado inicie sesi�n en la aplicaci�n.

#### M�todo HTTP

`POST`

#### Ruta
`/api/Authenticacion/login`

#### Cuerpo de la solicitud

```json
{
  "correo": "string",
  "password": "string"
}
```
#### C�digos de respuesta

- `201 ok`: El usuario se ha logueado correctamente.
- `400 Bad Request`: La solicitud contiene datos inv�lidos.
- `401 Unauthorized`: Las credenciales proporcionadas son incorrectas..
- `500 Internal Server Error`: Error interno del servidor.

## Balance

El balance representa el saldo actual en la cuenta del usuario, reflejando tanto los ingresos como los gastos registrados a trav�s de movimientos financieros. Este saldo puede fluctuar a medida que se a�aden nuevos ingresos o se realizan gastos, reflejando as� la situaci�n financiera en tiempo real.

## Endpoints

### Obtener Balance

#### Descripci�n

Este endpoint permite obtener el balance actual del usuario logueado.

#### M�todo HTTP

`GET`

#### Ruta
`/api/Balance`

#### Par�metros de consulta

- `idUsuario`: ID del usuario para el cual se obtiene el balance. Este par�metro se utiliza para especificar el usuario del cual se desea conocer el saldo. El backend utilizar� autom�ticamente el usuario actualmente autenticado, extrayendo la informaci�n del token de autenticaci�n.

#### C�digos de respuesta

- `200 OK`: La solicitud se complet� satisfactoriamente.
- `401 Unauthorized`: No se proporcionaron credenciales de autenticaci�n v�lidas.
- `404 Not Found`: El balance para el usuario no pudo ser encontrado.
- `500 Internal Server Error`: Error interno del servidor.


## Ingresos

Esta funcionalidad te permite registrar tus ingresos para llevar un control detallado de tus finanzas.

## Endpoints

### 1. Obtener Ingresos por mes

#### Descripci�n

Este endpoint te permite obtener los ingresos registrados para el mes actual o para una fecha espec�fica proporcionada.

#### M�todo HTTP

`GET`

#### Ruta
`/api/Ingresos`

#### Par�metros de consulta

- `mes`: Especifica el mes para el cual deseas obtener los ingresos. Este par�metro debe ser proporcionado en formato de fecha y hora (datetime)
- `idUsuario`: ID del usuario para el cual se obtiene los ingresos por Usuario. Este par�metro se utiliza para especificar el usuario del cual se desea conocer el saldo. El backend utilizar� autom�ticamente el usuario actualmente autenticado, extrayendo la informaci�n del token de autenticaci�n.

#### C�digos de respuesta

- `200 OK`: La solicitud se complet� satisfactoriamente.
- `401 Unauthorized`: No se proporcionaron credenciales de autenticaci�n v�lidas.
- `500 Internal Server Error`: Error interno del servidor.


### 2. Registrar Ingresos

#### Descripci�n

Este endpoint te permite registrar los ingresos en tu cuenta para llevar un seguimiento preciso de tus finanzas.

#### M�todo HTTP

`POST`

#### Ruta
`/api/Ingresos`

#### Par�metros de consulta

- `idUsuario`: Este par�metro se utiliza para especificar el usuario del cual se desea conocer el saldo. El backend utilizar� autom�ticamente el usuario actualmente autenticado, extrayendo la informaci�n del token de autenticaci�n.
- `idBalance`:  Si el usuario no tiene un balance asociado, al realizar la solicitud de registro de ingreso, se crear� autom�ticamente un balance para el usuario.
- `idIngreso`:  Este identificador se genera autom�ticamente en el backend como un GUID �nico para cada ingreso registrado.


- #### Cuerpo de la solicitud

```json
{
  "monto": 0,
  "fechaIngreso": "2024-03-10",
  "descripcion": "string"
}
```

#### C�digos de respuesta

- `200 OK`: La solicitud se complet� satisfactoriamente.
- `401 Unauthorized`: No se proporcionaron credenciales de autenticaci�n v�lidas.
- `500 Internal Server Error`: Error interno del servidor.


## Transacci�n (Gastos)

Esta funcionalidad te permite registrar y consultar tus gastos/Transacciones para llevar un control detallado de tus finanzas.

## Endpoints

### 1. Obtener Transacci�n por mes

#### Descripci�n

Este endpoint te permite obtener las transacciones registrados para el mes actual o para una fecha espec�fica proporcionada.

#### M�todo HTTP

`GET`

#### Ruta
`/api/Transaccion`

#### Par�metros de consulta

- `mes`: Especifica el mes para el cual deseas obtener las transacciones. Este par�metro debe ser proporcionado en formato de fecha y hora (datetime)
- `idUsuario`: ID del usuario para el cual se obtiene las transacciones por Usuario. Este par�metro se utiliza para especificar el usuario del cual se desea conocer el saldo. El backend utilizar� autom�ticamente el usuario actualmente autenticado, extrayendo la informaci�n del token de autenticaci�n.

#### C�digos de respuesta

- `200 OK`: La solicitud se complet� satisfactoriamente.
- `401 Unauthorized`: No se proporcionaron credenciales de autenticaci�n v�lidas.
- `500 Internal Server Error`: Error interno del servidor.


### 2. Registrar Transacciones

#### Descripci�n

Este endpoint te permite registrar las transacciones en tu cuenta para llevar un seguimiento preciso de tus finanzas.

#### M�todo HTTP

`POST`

#### Ruta
`/api/Transaccion`

#### Par�metros de consulta

- `idUsuario`: Este par�metro se utiliza para especificar el usuario del cual se desea conocer el saldo. El backend utilizar� autom�ticamente el usuario actualmente autenticado, extrayendo la informaci�n del token de autenticaci�n.
- `idTransaccion`:  Este identificador se genera autom�ticamente en el backend como un GUID �nico para cada ingreso registrado.


- #### Cuerpo de la solicitud

```json
{
  "idCategoria": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "idLugar": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "idMetodoPago": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "descripcion": "string",
  "producto": "string",
  "monto": 0,
  "fechaTransaccion": "2024-03-10T04:03:50.323Z",
}
```

#### C�digos de respuesta

- `200 OK`: La solicitud se complet� satisfactoriamente.
- `401 Unauthorized`: No se proporcionaron credenciales de autenticaci�n v�lidas.
- `500 Internal Server Error`: Error interno del servidor.
