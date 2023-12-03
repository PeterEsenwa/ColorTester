// https://nuxt.com/docs/api/configuration/nuxt-config

import { nuxtModulesConfig } from './configs/nuxt/nuxt-modules.config';

// Define Nuxt.js configuration.
export default defineNuxtConfig({
	...nuxtModulesConfig,
	nitro: {
		preset: 'firebase',
		firebase: {
			nodeVersion: '18',
			gen: 2,
			httpsOptions: {
				maxInstances: 2,
			}
		},
	},

	devtools: {
		enabled: false
	}
})
