# ColorTester

ColorTester lets you try colour palettes on real UI mockups before committing to them. You build a palette,
assign its colours to **colour roles** (primary, secondary, background, accent, error, success, warning and their
"on" counterparts), and see the result applied live to an SVG mockup. Palettes and mockup projects are stored per user.

- **Frontend:** Nuxt 3 (SPA mode), Vue 3, TypeScript, UnoCSS, Pinia, Auth0 for sign-in, Histoire for component stories.
- **Backend:** ASP.NET Core 8 Web API, layered by domain (Domain, Application, Infrastructure), EF Core with PostgreSQL,
  Auth0 JWT bearer auth with permission-based policies, Google Cloud Storage for mockup SVGs (signed URLs).
- **Hosting:** the frontend deploys to Firebase Hosting; the API ships as a Docker image.

## Repository layout

```
backend/
  ColorTesterAPI/                              HTTP layer: controllers, auth, startup
  ColorTesterAPI.Application/                  application services, DTOs, Cloud Storage service
  ColorTesterAPI.Domain/                       entities (Palette, PaletteColor, Project) and value objects (Color, ColorRole, ...)
  ColorTesterAPI.Domain.Tests/                 xUnit tests for the domain model
  ColorTesterAPI.Infrastructure.Persistence.*  EF Core / PostgreSQL and in-memory repositories
frontend/
  pages/ components/ stores/ services/         Nuxt app (palette editor, SVG renderer, project view)
  docs/SVG_Structure.md                        how to tag colourable elements in a mockup SVG
```

## Prerequisites

- .NET SDK 8
- Node.js 22 and pnpm 8 (pinned in `frontend/package.json`)
- PostgreSQL, only to run the API

## Build and test

```bash
# backend
cd backend
dotnet build ColorTesterAPI.sln
dotnet test ColorTesterAPI.Domain.Tests

# frontend
cd frontend
pnpm install --frozen-lockfile
pnpm lint
pnpm generate   # static SPA output in frontend/.output/public
```

If `pnpm` fails with `Cannot find matching keyid`, your Corepack is older than npm's current signing keys.
Update it with `npm install -g corepack@latest`, or run once with `COREPACK_INTEGRITY_KEYS=0`.

## Run locally

```bash
# API (ports are in Properties/launchSettings.json)
cd backend/ColorTesterAPI
dotnet run

# frontend
cd frontend
pnpm dev
```

The API needs a reachable PostgreSQL instance (connection string in `appsettings.Development.json`), an Auth0 tenant
(`Auth0:Domain` and `Auth0:Audience`), and a Google Cloud Storage bucket (`GoogleCloudStorage:BucketName`, with
credentials in `../keys/`, which is not committed). The frontend reads its API and Auth0 settings from
`frontend/auth.config.json` and the Nuxt runtime config.

## Component stories

```bash
cd frontend
pnpm story:dev
```

## Status

The project was paused in July 2024 and resumed in November 2026. The first step was making it reproducible again:
the backend moved from .NET 7 to .NET 8, the domain tests now run from the command line, lint passes, and CI builds
and tests both halves on every push and pull request.
