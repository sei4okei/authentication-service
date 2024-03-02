# Сервис аутентификации пользователя
C использованием JWT

## Работа сервиса
Сервис содержит два endpoint'a:
1. Регистрация - http://localhost:8088/api/Account/register
![Register](https://github.com/sei4okei/authentication-service/blob/docker/Pasted%20image%2020230903204312.png)]
2. Вход - http://localhost:8088/api/Account/login
![Login](https://github.com/sei4okei/authentication-service/blob/docker/Pasted%20image%2020230903204337.png)]

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
3. Всё готово!
