// https://nuxt.com/docs/api/configuration/nuxt-config

import { nuxtModulesConfig } from './configs/nuxt/nuxt-modules.config';

// Define Nuxt.js configuration.
export default defineNuxtConfig({
	...nuxtModulesConfig,

	devtools: {
		enabled: false
	}
})
