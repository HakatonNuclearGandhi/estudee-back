# estudee-back
Back-end application for estudee.

## Table of contents
-  [API documentation](#api-docs)
    - [User/Register](#Register)
    - [User/Login](#Login)
    - [User/Delete](#User-delete)
    - [Subject/Create](#subject-get-all)
    - [Subject/GetAllSubject](#subject-get-all)
    - [Subject/GetSubject](#subject-get)
    - [Subject/UpdateSubjectRequest](#subject-update)
    - [Subject/Delete](#subject-delete)
    - [Task/Create](#create-task)
    - [Task/GetAll](#get-all-task)
    - [Task/Get](#get-task)
    - [Task/Delete](#delete-task)
    - [Task/UpdateTaskRequest](#update-task)
    - [Dashboard/getProgress](#progress)
    - [Status/getStatus](#status)
    - [Subject/getListName](#subject-name-list)



<a name="api-docs"/></a>
## API documentation
<a name="Register"/></a>
### User/Register
Register a new user account.
#### Method: POST
Endpoint: user/register

Request Body:
```js
{
    "username": "string",
    "email": "user@example.com",
    "password": "string",
}
```
```
Response: None
```

---
<a name="Login"/></a>
### User/Login

Authenticate a user and return a token.
#### Method: POST
Endpoint: user/login

Request Body:
```js
{
    "email": "string",
    "password": "string"
}
```
UserDto.
Response:
```js
{
    "token": "string",
    "refreshToken": "string",
    "expiration": "2023-05-13T19:09:02Z"
}
```

---
<a name="User-delete"/></a>
### User deletion
#### Method: DELETE
Endpoint: /settings

Request header:
```js
{
  "token": "string"
}
```
Request Body:
```js
{
  "password": "string",
}
```
```
Response: None
```


---
<a name="subject-create"></a>
### Add Subject
Add a new subject to the system.
#### Method: POST
Endpoint: /subjects
CreateSubjectDto

Request header:
```js
{
  "token": "string"
}
```

Request Body:
```js
{
    "subjectName": "string",
    "description": "string",
    "maxScore": 0
}
```
Response:
```
Status: 200 OK
```
Body: None

---
<a name="subject-delete"></a>
### Delete Subject
Delete a subject from the system.
#### Method: DELETE
Endpoint: /subjects/{subjectId}

Response:
Request header:
```js
{
  "token": "string"
}
```
Status: 200 OK
Body: None
---

<a name="Get-Subject"></a>
### Get Subject
Retrieve details of a specific subject.
#### Method: GET
Endpoint: /subjects/{subjectId}
SubjectDto

Request header:
```js
{
  "token": "string"
}
```

Response:
```js
{
    "subjectId": "string",
    "subjectName": "string",
    "description": "string",
    "maxScore": 0,
    "currentScore": 100
}
```
```
Status: 200 OK
```

<a name="subject-get-all"></a>

### Get All Subjects
Retrieve a list of all subjects.
#### Method: GET
Endpoint: /subjects
SubjectDto

Request header:
```js
{
  "token": "string"
}
```
Response:
```
Status: 200 OK
```
Body:
```js
[
    "subjects": 
    [     
        {
            "subjectId": "string",
            "subjectName": "string",
            "description": "string",
            "maxScore": 0,
            
        },
        {
            "subjectId": "string",
            "subjectName": "string",
            "description": "string",
            "maxScore": 0,
            "currentScore": 100
        },
        ...
    ]
]
```

---
<a name="create-task"></a>
### Add Task
Add a new task to the system.
#### Method: POST
Endpoint: /tasks
CreateTaskDto

Request header:
```js
{
  "token": "string"
}
```

Request Body:
```js
{
    "subjectId": "string",
    "taskName": "string",
    "maxScore": 0,
    "deadline": "yyyy-MM-dd"
}
```
Response:
```
Status: 200 OK
Body: None
```

<a name="get-task"></a>
---
### Get Task
Retrieve details of a specific task.
#### Method: GET
Endpoint: /tasks/{taskId}
TaskDto.

Request header:
```js
{
  "token": "string"
}
```

Response:
```
Status: 200 OK
```
Body:
```js
{
    "subjectName": "string",
    "taskId": "string",
    "taskName": "string",
    "maxScore": 0,
    "score": 0,
    "deadline": "yyyy-MM-dd",
    "statusName": "string"
}
```
<a name="delete-task"></a>

### Delete Task
Delete a task from the system.
#### Method: DELETE
Endpoint: /tasks/{taskId}

Request header:
```js
{
  "token": "string"
}
```
Response:
```
Status: 200 OK
```
Body: None

---
<a name="get-all-task"></a>
### Get All Tasks
Retrieve a list of all tasks.
#### Method: GET
Endpoint: dashboard/tasks
TaskDto
Request header:
```js
{
  "token": "string"
}
```
Response:
```
Status: 200 OK
```
Body:
```js
{
    "tasks":
    [
        {
            "subjectName": "string",
            "taskId": "string",
            "taskName": "string",
            "maxScore": 0,
            "score": 0,
            "deadline": "yyyy-MM-dd",
            "statusName": "string"
        },
        {
            "subjectName": "string",
            "taskId": "string",
            "taskName": "string",
            "maxScore": 0,
            "score": 0,
            "deadline": "yyyy-MM-dd",
            "statusName": "string"
        },
        ...
    ]
}
```

<a name="update-task"></a>

### Update Task
Update cuurent task
#### Method: PUT
Endpoint: /tasks/update

Request header:
```js
{
  "token": "string"
}
```
Request body:
```js
{
    "taskId": "string",
    "subjectId": "string"
    "taskName": "string",
    "maxScore": 0,
    "score": 0,
    "deadline": "yyyy-MM-dd"
}
```

Response:
```
Status: 200 OK
```
Response Body: None

<a name="progress"></a>

### Get user progress
get user progress by score
#### Method: GET
Endpoint: /dashboard/progress

Request header:
```js
{
  "token": "string"
}
```
Request body:

```
Response Body:```js
{
    "progress": "70"
}
```

Response:
```
Status: 200 OK
```

<a name="status"></a>

### Get all status
get all staatus
#### Method: Get
Endpoint: /status

Request header:
```js
{
  "token": "string"
}
```
Request body:

Response Body:
```js
{
    "status": [
        {
            "statusName": "string"
        },
        {
            "statusName": "string"
        },
        {
            "statusName": "string"
        }
    ]
}
```

Response:
```
Status: 200 OK
```

<a name="subject-name-list"></a>

### Get all subject name
 Get all subject name
#### Method: Get
Endpoint: /subject/getAllName

Request header:
```js
{
  "token": "string"
}
```
Request body:

Response Body:
```js
{
    "subjectNames": [
        {
            "SubjectId": "string",
            "SubjectName": "string"
        },
        {
            "SubjectId": "string",
            "SubjectName": "string"
        },
        {
            "SubjectId": "string",
            "SubjectName": "string"
        }
    ]
}
```

Response:
```
Status: 200 OK
```
