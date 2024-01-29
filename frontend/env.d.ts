/// <reference types="@histoire/plugin-nuxt/src" />
/// <reference types="@histoire/plugin-nuxt/dist" />

import type { NuxtApp } from '#app';
import { Auth0VueClient } from '@auth0/auth0-spa-js';
import type { $Fetch } from 'nitropack';
import type { Ref } from 'vue';

declare module "#app" {
	interface NuxtApp {
		auth: Auth0VueClient
		$apiFetch: Ref<$Fetch | undefined>
	}
}

declare module 'nuxt/dist/app/nuxt' {
	interface NuxtApp {
		auth: Auth0VueClient
		$apiFetch: Ref<$Fetch | undefined>
	}
}

declare function useNuxtApp(): NuxtApp;
