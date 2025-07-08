# Примеры архитектур 

Архитектуры и шаблоны для собственного использования, не претендуют на правильность.
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

<br>

### 📁 fullstack-web-app-rest

- **Backend**: ASP.NET Core 8+ (REST API + Clean Architecture)
- **Frontend**: React + TypeScript (Axios, Tailwind CSS)
- **Database**: PostgreSQL (ORM - Entity Framework Core)
- **Logging & Monitoring**: Serilog + Seq
- **Auth**: Refresh token-based authorization (JWT)

<br>

### 📁 fullstack-web-app-graphql

- **Backend**: ASP.NET Core 8+ (GraphQL + Clean Architecture)
- **Frontend**: React + TypeScript (Apollo Client, Tailwind CSS)
- **Database**: PostgreSQL (ORM - Entity Framework Core)
- **Logging & Monitoring**: Serilog + Seq
- **Auth**: -

<br>

### 📁 fullstack-web-app-cqrs


- **Backend**: ASP.NET Core 8 (REST API, CQRS + VSA + CA)
- **Frontend**: React + TypeScript (Axios, Tailwind CSS)
- **Database**: PostgreSQL (ORM - Entity Framework Core)
- **Logging & Monitoring**: Serilog + Seq
- **Auth**: -

⚠️ Упрощенный пример паттерна CQRS с использованием библиотеки MediatR (**MediatR ≠ CQRS**), без разделения на Read DD и Write DB и без оптимизации чтения и записи.

<br>
