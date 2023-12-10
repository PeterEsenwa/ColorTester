/// <reference types="@histoire/plugin-nuxt/src" />
/// <reference types="@histoire/plugin-nuxt/dist" />

import type { NuxtApp } from '#app';

declare module "#app" {
	import { Auth0Client } from '@auth0/auth0-spa-js';

	interface NuxtApp {
		$auth0: Auth0Client
	}
}

declare module 'nuxt/dist/app/nuxt' {
	import { Auth0Client } from '@auth0/auth0-spa-js';

	interface NuxtApp {
		$auth0: Auth0Client
	}
}

declare function useNuxtApp(): NuxtApp;
