# Сайт-визитка Front-End разработчика

Полноценное фул-стек приложение: **Blazor WebAssembly** на клиенте, **ASP.NET Core** + **SQLite** на сервере.
Сайт двуязычный (русский и английский), весь контент хранится в базе, а не зашит в разметку.

---

## Стек

| Слой | Технологии |
|------|------------|
| Front-end | Blazor WebAssembly, Bootstrap 5, HTML, CSS |
| Back-end | ASP.NET Core 8 Minimal API, C# |
| База данных | SQLite + Entity Framework Core 8 (миграции) |
| Тесты | xUnit, FluentAssertions |

---

## Структура решения

```
Portfolio.sln
├── src
│   ├── Portfolio.Domain          — сущности. Ни от чего не зависит
│   ├── Portfolio.Shared          — контракты API (DTO), список языков, правила валидации
│   ├── Portfolio.Application     — бизнес-логика и интерфейсы хранилищ
│   ├── Portfolio.Infrastructure  — EF Core, SQLite, миграции, репозитории, контент
│   ├── Portfolio.Api             — HTTP-эндпоинты, защита, хостинг клиента
│   └── Portfolio.Client          — Blazor WASM: страницы, компоненты, переводы интерфейса
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
| `src/Portfolio.Infrastructure/Persistence/Seed/RussianContent.cs` | **весь текст сайта, русская версия** |
| `src/Portfolio.Infrastructure/Persistence/Seed/EnglishContent.cs` | **весь текст сайта, английская версия** |
| `src/Portfolio.Client/Localization/RussianStrings.cs` | подписи интерфейса (кнопки, заголовки секций) |
| `src/Portfolio.Client/Localization/EnglishStrings.cs` | то же на английском |
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

Откройте `https://localhost:7150`. При первом запуске приложение само применит миграции
и заполнит базу `portfolio.db` содержимым обоих языков.

Тесты:

```bash
dotnet test Portfolio.sln
```

---

## Две языковые версии

Язык определяется так: сохранённый выбор в браузере → язык браузера → русский.
Переключатель **RU / EN** стоит в шапке, выбор запоминается в `localStorage`.

Контент и подписи разделены:

* **контент** (обо мне, проекты, опыт) лежит в базе, у каждой записи есть `LanguageCode`;
  API отдаёт нужную версию по параметру `?lang=`;
* **подписи интерфейса** (кнопки, заголовки секций, тексты ошибок) лежат в классах
  `RussianStrings` / `EnglishStrings`.

Класс `UiStrings` объявляет все подписи как `required`, поэтому забыть перевести новую
строку невозможно — проект просто не соберётся.

### Добавить третий язык

1. Дописать код языка в `Portfolio.Shared/Contracts/Languages.cs`.
2. Добавить пакет контента рядом с `RussianContent.cs` и зарегистрировать его в `PortfolioSeedData`.
3. Добавить набор подписей рядом с `RussianStrings.cs` и вернуть его из `UiStringsCatalog`.

Ни один компонент интерфейса при этом не меняется.

---

## Как поменять содержимое под себя

1. Откройте `Persistence/Seed/RussianContent.cs` и `EnglishContent.cs`, впишите свои данные.
2. Удалите файл `src/Portfolio.Api/portfolio.db`.
3. Запустите приложение — база создастся заново с новым текстом.

Фото и резюме кладутся в `src/Portfolio.Client/wwwroot/`:

* фото — `wwwroot/img/avatar.jpg`, в `PhotoUrl` укажите `img/avatar.jpg`;
* резюме — `wwwroot/files/cv.pdf`, в `ResumeUrl` укажите `files/cv.pdf`.

Цвета и шрифт меняются в одном месте — блок `:root` в `wwwroot/css/app.css`.

---

## Миграции базы данных

Схема базы описана миграциями EF Core в `src/Portfolio.Infrastructure/Migrations`.
При старте приложение применяет все непринятые миграции автоматически.

Если поменяли сущности — создайте новую миграцию:

```bash
dotnet tool install --global dotnet-ef          # один раз

dotnet ef migrations add ИмяМиграции \
  --project src/Portfolio.Infrastructure \
  --startup-project src/Portfolio.Api

dotnet ef database update \
  --project src/Portfolio.Infrastructure \
  --startup-project src/Portfolio.Api
```

Проверить, что снимок модели совпадает с сущностями:

```bash
dotnet ef migrations has-pending-model-changes \
  --project src/Portfolio.Infrastructure \
  --startup-project src/Portfolio.Api
```

Откатить последнюю ещё не применённую миграцию — `dotnet ef migrations remove` с теми же ключами.

---

## API

| Метод | Адрес | Что делает |
|-------|-------|------------|
| GET | `/api/profile?lang=ru` | данные первого экрана и блока «Обо мне» |
| GET | `/api/skills?lang=ru` | технологии по категориям |
| GET | `/api/projects?lang=ru` | проекты |
| GET | `/api/experience?lang=ru` | опыт работы |
| POST | `/api/contact` | приём сообщения из формы |

Параметр `lang` принимает `ru` и `en`; любое другое значение молча заменяется на русский.
GET-ответы кэшируются на 10 минут **отдельно по каждому языку**, поэтому база не нагружается
при наплыве посетителей.

`POST /api/contact` возвращает код исхода (`accepted`, `rate_limited`, `failed`), а не готовый
текст: текст подставляет клиент на нужном языке.

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

* отправку письма на почту при новом сообщении из формы;
* админ-панель с авторизацией, чтобы править контент без пересборки;
* размещение Bootstrap и шрифта на своём домене — тогда `Content-Security-Policy` сократится до `'self'`;
* отдельные адреса для языковых версий (`/en/...`) — это лучше индексируется поисковиками,
  чем переключение через `localStorage`.
