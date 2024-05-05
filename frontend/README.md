# Nuxt 3 and Vue 3 Frontend Project

This project is a frontend application built with Nuxt 3 and Vue 3. It uses TypeScript as the primary language and is styled with UnoCSS.

## Project Structure

The project is structured as follows:

- `components`: This directory contains all the Vue components used in the project.
- `configs`: This directory contains configuration files for UnoCSS and other configurations.
- `types`: This directory contains TypeScript type definitions.
- `utils`: This directory contains utility functions and constants.
- `assets`: This directory contains static assets like images, fonts, etc.
- `layouts`: This directory contains layout components that wrap around your views.
- `middleware`: This directory contains functions that have access to the context of the application.
- `pages`: This directory contains application views and routes.
- `plugins`: This directory contains JavaScript plugins that you want to run before instantiating the root Vue.js application.
- `store`: This directory contains Vuex Store files.
- `test`: This directory contains test files and related configurations.

Please note that some directories might not be present in your project if you haven't used their features.

## Key Files

- `uno.config.ts`: This file contains the configuration for UnoCSS, including presets and shortcuts.
- `components/color-role/ColorRole.vue`: This is a Vue component for handling color roles.
- `pages/index.vue`: This is the main page of your application. It contains the logic for fetching and displaying color palettes, as well as the logic for adding new palettes.
- `plugins/useDarkBody.ts`: This plugin is used to enable dark mode in the application.
- `plugins/01.api.client.ts`: This plugin provides a `$fetch` instance with default settings to the Nuxt app. It also includes logic for handling user authentication and setting the Authorization header for authenticated requests.

## Additional Documentation

- [Authentication](./docs/authentication.md): Detailed explanation of how authentication is handled in the project.
- [Color Palette Management](./docs/color-palette-management.md): Explanation of how color palettes are managed in the project.
- [Dark Mode](./docs/dark-mode.md): Explanation of how dark mode is implemented in the project.
- [API Client](./docs/api-client.md): Explanation of how the API client is set up and how to use it.
- [Project Configuration](./docs/project-configuration.md): Explanation of how to configure the project.
- [Testing](./docs/testing.md): Explanation of how to run the tests and how to add new tests.
- [Deployment](./docs/deployment.md): Detailed instructions for deploying the project.

## Setup

First, install the dependencies:

```bash
# pnpm
pnpm install
```

Then, run the development server:

```bash
# pnpm
pnpm dev
```
