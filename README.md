# Сервис аутентификации пользователя
C использованием JWT

## Работа сервиса
Сервис содержит endpoint'ы:
1. Регистрация - http://localhost:8088/api/Account/register
![Register](https://github.com/sei4okei/authentication-service/blob/docker/Pasted%20image%2020230903204312.png)]
2. Вход - http://localhost:8088/api/Account/login
![Login](https://github.com/sei4okei/authentication-service/blob/docker/Pasted%20image%2020230903204337.png)]
3. Обновление токена - http://localhost:8088/api/Account/refresh (В заголовок необходимо добавить - "Refresh" с рефреш токеном)
4. Проверка статуса пользователя - http://localhost:8088/api/Account/status (В заголовок необходимо добавить - "Authorization" с токеном доступа)

Данные для подключения к PostgreSQL:
- DB: Users
- User: postgres
- Password: postgres
## Запуск
Для запуска необходимо:

1. Создать снимок приложения (в папке проекта)
```cmd
docker build . -t authenticationservice
```
2. Запустить контейнеры
```cmd
docker-compose up
```
3.  Вставить строку подключения в appsettings.json
```json
"ConnectionStrings": {
  "DefaultConnection": "User ID=postgres;Password=postgres;Server=localhost;Port=5433;Database=Users;IntegratedSecurity=true;Pooling=true"
},
```
4.  Применить миграцию через консоль диспетчера пакетов
```cmd
Update-Database
```
5. Всё готово!
