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

### 📁 fullstack-web-app-template

- **Backend**: ASP.NET Core 8
- **Frontend**: React.js (Axios)

📌 Готовый **пустой** шаблон фуллстек приложения, содержащий связанные бэкенд и фронтенд

<br>

### 📁 fullstack-web-app-rest-template

- **Backend**: ASP.NET Core 8 (REST API + Clean Architecture)
- **Frontend**: React + TypeScript (Axios, Tailwind CSS)
- **Database**: PostgreSQL (ORM - Entity Framework Core)
- **Logging & Monitoring**: Serilog + Seq
- **Auth**: Refresh token-based authorization (JWT)

📌 Готовый шаблон фуллстек приложения на "Чистой архитектуре", включающий в себя бэкенд, фронтенд, базу даных, логирование и авторизацию

<br>

### 📁 fullstack-web-app-cqrs-template


- **Backend**: ASP.NET Core 8 (REST API, CQRS + VSA + CA)
- **Frontend**: React + TypeScript (Axios, Tailwind CSS)
- **Database**: PostgreSQL (ORM - Entity Framework Core)
- **Logging & Monitoring**: Serilog + Seq

📌 Готовый шаблон паттерна CQRS с использованием библиотеки MediatR (**MediatR ≠ CQRS**), без разделения на Read DB и Write DB и без оптимизации чтения и записи.

<br>

### 📁 graphql-example

- **Backend**: ASP.NET Core 8 (HotChocolate в .NET)
- **Frontend**: React + TypeScript (Apollo Client, Tailwind CSS)
- **Database**: PostgreSQL (ORM - Entity Framework Core)
- **Logging & Monitoring**: Serilog + Seq

📌 Простой пример фуллстек приложения с использованием GraphQL в связке с фронтендом на React

<br>

### 📁 grpc-example


- **MathService**: ASP.NET Core 8 (gRPC Service) 
- **CalculatorService**: ASP.NET Core 8 (Web API)

📌 Простой пример **синхронного** взаимодействия двух сервисов с помощью протокола gRPC

<br>

### 📁 kafka-example


- **OrderService**: ASP.NET Core 8 (Producer) 
- **NotificationService**: ASP.NET Core 8 (Consumer)
- **Message Broker**: Kafka (Confluent.Kafka в .NET)
- **Kafka UI** (веб-интерфейс для Kafka от provectuslabs)

**Важно!** Топики должны создаваться централизованно и управляемо в инфраструктуре (IaC, devOps-практики) на старте приложения. На старте приложения все топики уже должны быть созданы и сконфигурированы.

📌 Простой пример **асинхронного** взаимодействия двух сервисов с помощью брокера сообщений Kafka
<br>
