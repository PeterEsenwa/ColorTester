/// <reference types="@histoire/plugin-nuxt/src" />
/// <reference types="@histoire/plugin-nuxt/dist" />

import type { NuxtApp } from '#app';
import { Auth0VueClient } from '@auth0/auth0-spa-js';

declare module "#app" {
	interface NuxtApp {
		auth: Auth0VueClient
	}
}

declare module 'nuxt/dist/app/nuxt' {
	interface NuxtApp {
		auth: Auth0VueClient
	}
}

declare function useNuxtApp(): NuxtApp;
