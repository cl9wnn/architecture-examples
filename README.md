# Примеры архитектур 

Архитектуры и шаблоны для собственного использования, не претендуют на правильность (!). Все примеры и шаблоны легко разворачиваются в Docker.
## Важно!

⚠️ Для запуска в IDE нужно скопировать структуру из **appsettings.json** в новый файл (appsettings.Secrets.json или appsettings.Development.json) и заполнить ее конфигурационными данными. Также надо будет установить вручную все необходимые зависимости для React.

⚠️ Для запуска в Docker необходимо рядом с **docker.compose.yml** создать файл конфигурации **.env** и добавить в него все необходимые серкеты, нужные для запуска в docker-compose.yml.

**Пример:**
```
POSTGRES_USER=postgres
POSTGRES_PASSWORD=password
POSTGRES_DB=testDb
AUTH_SECRET_KEY=SECRETKEY
```

## Описание

### 📁 fullstack-web-app

- **Backend**: ASP.NET Core 8+
- **Frontend**: React.js (Axios)

📌 Готовый пустой шаблон фуллстек приложения со связанными web Api и фронтендом

<br>

### 📁 fullstack-web-app-rest

- **Backend**: ASP.NET Core 8+ (REST API + Clean Architecture)
- **Frontend**: React + TypeScript (Axios, Tailwind CSS)
- **Database**: PostgreSQL (ORM - Entity Framework Core)
- **Logging & Monitoring**: Serilog + Seq
- **Auth**: Refresh token-based authorization (JWT)

📌 Готовый шаблон фуллстек приложения на "чистой" архитектуре, включающий фронтенд, базу даных, логирование и авторизацию

<br>

### 📁 fullstack-web-app-graphql

- **Backend**: ASP.NET Core 8+ (GraphQL + Clean Architecture)
- **Frontend**: React + TypeScript (Apollo Client, Tailwind CSS)
- **Database**: PostgreSQL (ORM - Entity Framework Core)
- **Logging & Monitoring**: Serilog + Seq
- **Auth**: -

📌 Простой пример фуллстек приложения (CRUD) с использованием GraphQL

<br>

### 📁 fullstack-web-app-cqrs


- **Backend**: ASP.NET Core 8 (REST API, CQRS + VSA + CA)
- **Frontend**: React + TypeScript (Axios, Tailwind CSS)
- **Database**: PostgreSQL (ORM - Entity Framework Core)
- **Logging & Monitoring**: Serilog + Seq
- **Auth**: -

📌 Упрощенный пример паттерна CQRS с использованием библиотеки MediatR (**MediatR ≠ CQRS**), без разделения на Read DD и Write DB и без оптимизации чтения и записи.

<br>

### 📁 grpc-example


- **Server**: ASP.NET Core 8 (gRPC Service) 
- **Client**: ASP.NET Core 8 (Web API)

📌 Простой пример взаимодействия двух сервисов с помощью протокола gRPC

<br>
