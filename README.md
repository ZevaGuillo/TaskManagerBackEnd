# TaskManager

# Buber Breakfast API

- [Task API](#buber-breakfast-api)
  - [Create Task](#create-breakfast)
    - [Create Task Request](#create-Task-request)
    - [Create Task Response](#create-Task-response)
  - [Update Task](#update-Task)
    - [Update Task Request](#update-Task-request)
    - [Update Task Response](#update-Task-response)
  - [Delete Task](#delete-Task)
    - [Delete Task Request](#delete-Task-request)
    - [Delete Task Response](#delete-Task-response)

## Create Task

### Create Task Request

```js
POST /Task
```

```json
{
    "titulo": "Realizar tarea de matemacica",
    "descripcion": "tabla de multiplicas del 30",
    "fechaFin": "2023-02-17T04:33:45.761Z",
    "fechaInicio": "2023-02-16T04:33:45.761Z",
}
```

### Create Task Response

```js
201 Created
```

```json
{
    "id": "1983f378-4a5f-4e34-8e67-24d707dbd7db",
    "titulo": "Realizar tarea de matemacica",
    "descripcion": "tabla de multiplicas del 30",
    "fechaFin": "2023-02-17T04:33:45.761Z",
    "fechaInicio": "2023-02-16T04:33:45.761Z",
    "estado": false
}
```

## Get Tasks

### Get Tasks Request

```js
GET /Tasks/{{id}}
```

### Get Task Response

```js
200 Ok
```

```json
{
    "1983f378-4a5f-4e34-8e67-24d707dbd7db": {
        "id": "1983f378-4a5f-4e34-8e67-24d707dbd7db",
        "titulo": "Realizar tarea de matemacica",
        "descripcion": "tabla de multiplicas del 30",
        "fechaFin": "2023-02-17T04:33:45.761Z",
        "fechaInicio": "2023-02-16T04:33:45.761Z",
        "estado": true
    }
}
```

## Update Task

### Update Task Request

```js
PUT /Tasks/{{id}}
```

```json
{
    "titulo": "Realizar tarea de matemacica",
    "descripcion": "tabla de multiplicas del 30",
    "fechaFin": "2023-02-17T04:33:45.761Z",
    "fechaInicio": "2023-02-16T04:33:45.761Z",
    "estado": true
}
```

### Update Task Response

```js
204 No Content
```

or

```js
201 Created
```

## Delete Task

### Delete Task Request

```js
DELETE /Task/{{id}}
```

### Delete Task Response

```js
204 No Content
```