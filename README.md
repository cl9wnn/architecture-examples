# Готовые шаблоны веб-приложений


📁 fullstack-web-app

- **Backend**: ASP.NET Core 8+
- **Frontend**: React.js (Tailwind CSS, Axios)

<br>

📁 full-fullstack-web-app

- **Backend**: ASP.NET Core 8+ (Clean Architecture, REST API)
- **Frontend**: React.js (Tailwind CSS, Axios)
- **Database**: PostgreSQL (ORM - Entity Framework Core)
- **Logging & Monitoring**: Serilog + Seq
- **Auth**: Refresh token-based authorization (JWT)

⚠️ Для запуска в IDE нужно скопировать структуру из **appsettings.json** в новый файл (appsettings.Secrets.json или appsettings.Development.json) и заполнить ее конфигурационными данными.

⚠️ Для запуска в Docker необходимо рядом с **docker.compose.yml** создать файл конфигурации **.env** и добавить в него все необходимые серкеты, нужные для запуска в docker-compose.yml.

**Пример:**
```
POSTGRES_USER=postgres
POSTGRES_PASSWORD=password
POSTGRES_DB=testDb
AUTH_SECRET_KEY=SECRETKEY
```
<br>
