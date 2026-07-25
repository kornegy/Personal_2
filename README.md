# Сайт-визитка Front-End разработчика

Полноценное фул-стек приложение: **Blazor WebAssembly** на клиенте, **ASP.NET Core** + **SQLite** на сервере.
Весь контент (обо мне, технологии, проекты, опыт) хранится в базе, а не зашит в разметку, — сайт правится в одном месте.

---

## Стек

| Слой | Технологии |
|------|------------|
| Front-end | Blazor WebAssembly, Bootstrap 5, HTML, CSS |
| Back-end | ASP.NET Core 8 Minimal API, C# |
| База данных | SQLite + Entity Framework Core 8 |
| Тесты | xUnit, FluentAssertions |

---

## Структура решения

```
Portfolio.sln
├── src
│   ├── Portfolio.Domain          — сущности. Ни от чего не зависит
│   ├── Portfolio.Shared          — контракты API (DTO) + правила валидации
│   ├── Portfolio.Application     — бизнес-логика и интерфейсы хранилищ
│   ├── Portfolio.Infrastructure  — EF Core, SQLite, репозитории, начальные данные
│   ├── Portfolio.Api             — HTTP-эндпоинты, защита, хостинг клиента
│   └── Portfolio.Client          — Blazor WASM: страницы и компоненты
└── tests
    └── Portfolio.Tests           — тесты логики и связки с базой
```

Зависимости идут в одну сторону:

```
Client ─┐
        ├──► Shared ◄─── Application ───► Domain
Api ────┘                    ▲
                             │
                     Infrastructure
```

`Domain` не знает ни про EF, ни про HTTP. `Application` описывает *что* делает приложение,
`Infrastructure` — *как* это хранится, `Api` — *как* это отдаётся наружу.
Поэтому заменить SQLite на PostgreSQL можно, не тронув ни одной строчки бизнес-логики.

### Основные файлы

| Файл | Зачем нужен |
|------|-------------|
| `src/Portfolio.Infrastructure/Persistence/PortfolioSeedData.cs` | **весь текст сайта** |
| `src/Portfolio.Client/wwwroot/css/app.css` | дизайн-система: цвета, отступы, компоненты |
| `src/Portfolio.Api/Middleware/SecurityHeadersMiddleware.cs` | заголовки безопасности |
| `src/Portfolio.Api/Extensions/RateLimitingExtensions.cs` | лимиты частоты запросов |

---

## Запуск

Нужен [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
dotnet restore Portfolio.sln
dotnet run --project src/Portfolio.Api
```

Откройте `https://localhost:7150`. База `portfolio.db` создастся сама при первом запуске
и наполнится данными из `PortfolioSeedData.cs`.

Тесты:

```bash
dotnet test Portfolio.sln
```

---

## Как поменять содержимое под себя

1. Откройте `src/Portfolio.Infrastructure/Persistence/PortfolioSeedData.cs` и впишите свои данные.
2. Удалите файл `src/Portfolio.Api/portfolio.db`.
3. Запустите приложение — база пересоздастся с новым текстом.

Фото и резюме кладутся в `src/Portfolio.Client/wwwroot/`:

* фото — `wwwroot/img/avatar.jpg`, в `PhotoUrl` укажите `img/avatar.jpg`;
* резюме — `wwwroot/files/cv.pdf`, в `ResumeUrl` укажите `files/cv.pdf`.

Цвета и шрифт меняются в одном месте — блок `:root` в `wwwroot/css/app.css`.

---

## API

| Метод | Адрес | Что делает |
|-------|-------|------------|
| GET | `/api/profile` | данные первого экрана и блока «Обо мне» |
| GET | `/api/skills` | технологии по категориям |
| GET | `/api/projects` | проекты |
| GET | `/api/experience` | опыт работы |
| POST | `/api/contact` | приём сообщения из формы |

GET-ответы кэшируются на 10 минут, поэтому база не нагружается при наплыве посетителей.

---

## Защита

Сайт публичный, поэтому исходим из того, что любой запрос может быть вредоносным.

| Угроза | Что сделано |
|--------|-------------|
| XSS | Blazor экранирует любой вывод; жёсткий `Content-Security-Policy` запрещает сторонние скрипты |
| Clickjacking | `X-Frame-Options: DENY` + `frame-ancestors 'none'` |
| MIME-sniffing | `X-Content-Type-Options: nosniff` |
| SQL-инъекции | EF Core параметризует все запросы, «сырого» SQL в коде нет |
| Подбор и флуд | лимиты запросов по IP: 120/мин на сайт, 3/10 мин на форму |
| Спам в форме | скрытое поле-ловушка (honeypot) + лимит 5 сообщений в час с одного адреса |
| Мусорные данные | валидация по атрибутам — и в браузере, и **обязательно** на сервере |
| Перегрузка тела запроса | тело запроса ограничено 64 КБ |
| Утечка деталей ошибок | необработанные исключения отдают нейтральный JSON, подробности только в логах |
| Хранение персональных данных | IP отправителя сохраняется в виде SHA-256 с солью, а не в открытом виде |
| Перехват трафика | HSTS на год + перенаправление на HTTPS |

### Перед публикацией в интернете

Задайте соль для хеширования IP — без неё приложение не стартует вне режима разработки:

```bash
export Contact__IpHashSalt="строка-минимум-16-символов"
```

Если сайт стоит за reverse proxy (nginx, Cloudflare), включите обработку `X-Forwarded-For` —
иначе все посетители будут выглядеть как один IP и лимиты сработают неверно.

---

## Что можно добавить дальше

* админ-панель с авторизацией, чтобы править контент без пересборки;
* отправку писем на почту при новом сообщении из формы;
* переход с `EnsureCreated` на миграции EF Core, когда схема начнёт меняться:
  `dotnet ef migrations add Initial --project src/Portfolio.Infrastructure --startup-project src/Portfolio.Api`;
* размещение Bootstrap и шрифта на своём домене — тогда `Content-Security-Policy` сократится до `'self'`;
* английскую версию сайта для зарубежных заказчиков.
