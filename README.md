# Front-End Developer Portfolio

A full-stack application: **Blazor WebAssembly** on the client, **ASP.NET Core** + **SQLite** on the server.
The site is bilingual (Ukrainian and English) and all content lives in the database rather than
being hardcoded into the markup.

---

## Stack

| Layer | Technologies |
|-------|--------------|
| Front-end | Blazor WebAssembly, Bootstrap 5, HTML, CSS |
| Back-end | ASP.NET Core 8 Minimal API, C# |
| Database | SQLite + Entity Framework Core 8 (migrations) |
| Tests | xUnit, FluentAssertions |

---

## Solution structure

```
Portfolio.sln
├── src
│   ├── Portfolio.Domain          — entities. Depends on nothing
│   ├── Portfolio.Shared          — API contracts (DTOs), language list, validation rules
│   ├── Portfolio.Application     — business logic and repository interfaces
│   ├── Portfolio.Infrastructure  — EF Core, SQLite, migrations, repositories, content
│   ├── Portfolio.Api             — HTTP endpoints, security, client hosting
│   └── Portfolio.Client          — Blazor WASM: pages, components, UI translations
└── tests
    └── Portfolio.Tests           — logic tests and database integration tests
```

Dependencies point in one direction only:

```
Client ─┐
        ├──► Shared ◄─── Application ───► Domain
Api ────┘                    ▲
                             │
                     Infrastructure
```

`Domain` knows nothing about EF or HTTP. `Application` describes *what* the application does,
`Infrastructure` — *how* it is stored, `Api` — *how* it is exposed.
That is why SQLite can be swapped for PostgreSQL without touching a single line of business logic.

### Key files

| File | Purpose |
|------|---------|
| `src/Portfolio.Infrastructure/Persistence/Seed/UkrainianContent.cs` | **all site copy, Ukrainian version** |
| `src/Portfolio.Infrastructure/Persistence/Seed/EnglishContent.cs` | **all site copy, English version** |
| `src/Portfolio.Client/Localization/UkrainianStrings.cs` | UI labels (buttons, section headings) |
| `src/Portfolio.Client/Localization/EnglishStrings.cs` | the same in English |
| `src/Portfolio.Client/wwwroot/css/app.css` | design system: colours, spacing, components |
| `src/Portfolio.Api/Middleware/SecurityHeadersMiddleware.cs` | security headers |
| `src/Portfolio.Api/Extensions/RateLimitingExtensions.cs` | request rate limits |

---

## Running locally

Requires [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0).

```bash
dotnet restore Portfolio.sln
dotnet run --project src/Portfolio.Api
```

Open `https://localhost:7150`. On the first run the application applies migrations
and fills `portfolio.db` with the content of both languages by itself.

Tests:

```bash
dotnet test Portfolio.sln
```

---

## Two language versions

The language is resolved in this order: stored choice in the browser → browser language → Ukrainian.
The **UK / EN** switch sits in the header and the choice is remembered in `localStorage`.

Content and labels are kept apart:

* **content** (about, projects, experience) lives in the database, every row has a `LanguageCode`;
  the API returns the right version based on the `?lang=` parameter;
* **UI labels** (buttons, section headings, error messages) live in the
  `UkrainianStrings` / `EnglishStrings` classes.

The `UiStrings` class declares every label as `required`, so forgetting to translate a new
string is impossible — the project simply will not compile.

### Adding a third language

1. Add the language code to `Portfolio.Shared/Contracts/Languages.cs`.
2. Add a content pack next to `UkrainianContent.cs` and register it in `PortfolioSeedData`.
3. Add a label set next to `UkrainianStrings.cs` and return it from `UiStringsCatalog`.

No UI component needs to change.

---

## Making the content your own

1. Open `Persistence/Seed/UkrainianContent.cs` and `EnglishContent.cs`, put your own data in.
2. Delete `src/Portfolio.Api/portfolio.db`.
3. Start the application — the database is recreated with the new copy.

Photo and CV go into `src/Portfolio.Client/wwwroot/`:

* photo — `wwwroot/img/avatar.jpg`, set `PhotoUrl` to `img/avatar.jpg`;
* CV — `wwwroot/files/cv.pdf`, set `ResumeUrl` to `files/cv.pdf`.

Colours and the font are changed in a single place — the `:root` block in `wwwroot/css/app.css`.

---

## Database migrations

The schema is described by EF Core migrations in `src/Portfolio.Infrastructure/Migrations`.
On startup the application applies every pending migration automatically.

If you change the entities, create a new migration:

```bash
dotnet tool install --global dotnet-ef          # once

dotnet ef migrations add MigrationName \
  --project src/Portfolio.Infrastructure \
  --startup-project src/Portfolio.Infrastructure
```

Check that the model snapshot matches the entities:

```bash
dotnet ef migrations has-pending-model-changes \
  --project src/Portfolio.Infrastructure \
  --startup-project src/Portfolio.Infrastructure
```

The startup project here is `Portfolio.Infrastructure` itself, not `Portfolio.Api`:
the context for the tool is created by `PortfolioDbContextFactory`. Thanks to that the
`Microsoft.EntityFrameworkCore.Design` package is only needed in the data layer
and is not pulled into the web application.

Applying migrations by hand is not needed — the application does it on startup.

To roll back the last migration that has not been applied yet, run `dotnet ef migrations remove`
with the same options.

---

## API

| Method | Path | Purpose |
|--------|------|---------|
| GET | `/api/profile?lang=uk` | hero section and "about" data |
| GET | `/api/skills?lang=uk` | technologies grouped by category |
| GET | `/api/projects?lang=uk` | projects |
| GET | `/api/experience?lang=uk` | work experience |
| POST | `/api/contact` | accepts a message from the contact form |
| GET | `/health` | liveness probe, returns `{"status":"ok"}` |

The `lang` parameter accepts `uk` and `en`; any other value silently falls back to Ukrainian.
GET responses are cached for 10 minutes **separately per language**, so the database is not
hammered when traffic spikes.

`POST /api/contact` returns an outcome code (`accepted`, `rate_limited`, `failed`) rather than
ready-made text: the client renders the message in the current language.

---

## Security

The site is public, so every request is treated as potentially hostile.

| Threat | Mitigation |
|--------|------------|
| XSS | Blazor encodes all output; a strict `Content-Security-Policy` blocks third-party scripts |
| Clickjacking | `X-Frame-Options: DENY` + `frame-ancestors 'none'` |
| MIME sniffing | `X-Content-Type-Options: nosniff` |
| SQL injection | EF Core parameterises every query, there is no raw SQL in the code |
| Brute force and flooding | per-IP rate limits: 120/min site-wide, 3 per 10 min on the form |
| Form spam | hidden honeypot field + a limit of 5 messages per hour from one address |
| Junk data | attribute-based validation in the browser and, crucially, **again on the server** |
| Oversized payloads | request body capped at 64 KB |
| Error detail leakage | unhandled exceptions return neutral JSON, details go to the logs only |
| Personal data storage | the sender IP is stored as a salted SHA-256 hash, never in plain text |
| Traffic interception | HSTS for one year + redirect to HTTPS |

### HTTPS

Any http request gets a permanent redirect (308) to https, and in production a
`Strict-Transport-Security` header valid for a year is added — the browser will no longer
use the insecure protocol even if the address is typed by hand.

**On your own machine** the browser first shows "Not secure": the development certificate is
self-signed and nobody vouched for it. The connection is already encrypted — only trust in
the certificate is missing.

On Windows and macOS a single command is enough:

```bash
dotnet dev-certs https --trust
```

On Linux that command does not finish the job everywhere: the certificate is not handed to the
system store, and Chrome and Firefox do not trust the system store anyway — they keep their own
databases (NSS). For Linux the repository ships a script that covers all three places at once:

```bash
sudo apt install libnss3-tools   # once, if certutil is missing
bash scripts/trust-dev-cert.sh
```

After running it, close the browser completely and reopen it — the padlock appears.
The script is safe to re-run, the old entry is replaced.

None of this is needed on the live site: there the certificate comes from the hosting
provider or Let's Encrypt.

**Behind a reverse proxy** (nginx, Cloudflare) the certificate usually lives on the proxy while
the application receives plain http. So that the application sees the real protocol and client IP,
list the proxy addresses in the configuration:

```json
"Security": {
  "HttpsPort": 443,
  "KnownProxies": [ "10.0.0.5" ]
}
```

While the list is empty the `X-Forwarded-*` headers are not read at all — and that is the right
default: trusting them from anyone would let any visitor forge their IP and bypass the rate limits.

### Before going public

Set the salt used to hash IP addresses — without it the application refuses to start outside
development mode:

```bash
export Contact__IpHashSalt="at-least-16-characters"
```

---

## Deployment

The application is packaged with Docker: the build runs in an SDK image, and only the finished
application — without sources — ends up in the final image.

### Render

The repository contains `render.yaml`, so nothing has to be configured by hand:

1. Go to [render.com](https://render.com) → **New** → **Blueprint**.
2. Connect the repository and pick the branch with this code.
3. Render reads `render.yaml`, creates the service and generates `Contact__IpHashSalt`.
4. The first build takes 5–10 minutes because Blazor WASM is compiled.

The site becomes available on a subdomain such as `artem-koval-portfolio.onrender.com`,
with a certificate issued automatically.

What `render.yaml` sets and why:

| Variable | Value | Reason |
|----------|-------|--------|
| `ASPNETCORE_ENVIRONMENT` | `Production` | enables HSTS and hides error details |
| `Contact__IpHashSalt` | generated by Render | the application will not start without it |
| `Security__TrustAllProxies` | `true` | TLS terminates on Render's proxy; without this every visitor looks like the same IP and the rate limits misfire |

**Free tier limitations.** The service goes to sleep after 15 minutes of inactivity and the first
request after that waits about a minute. There is no persistent disk on the free tier, so the
database is recreated on every restart: the site content is restored from code, but messages sent
through the form are lost. If the form matters, either move to a paid tier with a disk or send the
messages by email.

### Any other Docker host

```bash
docker build -t portfolio .
docker run -p 8080:8080 \
  -e Contact__IpHashSalt="at-least-16-characters" \
  -e Security__TrustAllProxies=true \
  -v portfolio-data:/data \
  portfolio
```

The port is taken from the `PORT` variable when the platform provides one, otherwise 8080.
The database lives in the `/data` volume — without it the data disappears on the next deploy.

Liveness probe — `GET /health`, returns `{"status":"ok"}`.

### Behind your own nginx

If TLS terminates on your nginx, name its address explicitly instead of using
`TrustAllProxies` — that is the safer option:

```json
"Security": {
  "HttpsPort": 443,
  "KnownProxies": [ "127.0.0.1" ]
}
```

---

## Possible next steps

* email notification when a new message arrives through the form;
* an admin panel with authentication so the content can be edited without a rebuild;
* self-hosting Bootstrap and the font — that shrinks `Content-Security-Policy` down to `'self'`;
* separate URLs per language (`/en/...`) — search engines index those better than a
  `localStorage`-based switch.
