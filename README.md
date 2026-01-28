# InfotecsTask API

## Описание проекта
**InfotecsTask API** — это ASP.NET Core Web API для загрузки и хранения значений из CSV-файлов в базу данных PostgreSQL.  
Проект реализует обработку больших файлов, валидацию данных и сохранение их в БД через массовую вставку (`BulkCreate`).  

---

## Технологии
- .NET 8  
- ASP.NET Core Web API  
- Entity Framework Core (PostgreSQL)  
- PostgreSQL  
- Swagger  

## Эндпоинты API

| Метод | URL | Описание |
|-------|-----|----------|
| `POST` | `/api/values/upload` | Загрузка CSV-файла, валидация и сохранение данных |
| `GET` | `/api/values/sorted?fileName={file_name}` | Получение отсортированных значений по имени файла |
| `GET` | `/api/results/filtered` | Получение отфильтрованных значений по заданным параметрам |

> Логика обработки находится в `ValuesService`, контроллер делегирует работу сервису.

---

## Установка и запуск

1. Клонируйте репозиторий:  
git clone https://github.com/DmitryGorny/infotecs_task.git
cd InfotecsTask

2. Настройте подключение к PostgreSQL в appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=infotecsdb;Username=postgres;Password=yourpassword"
}

3. Установите зависимости:
dotnet restore

4. Миграции
dotnet ef migrations add InitialCreate
dotnet ef database update

5. Запуск
dotnet watch run


