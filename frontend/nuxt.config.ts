// https://nuxt.com/docs/api/configuration/nuxt-config

import { nuxtModulesConfig } from './configs/nuxt/nuxt-modules.config';

// Define Nuxt.js configuration.
export default defineNuxtConfig({
	...nuxtModulesConfig,
	// components: [{
	// 	path: '~/components',
	// 	pattern: '**/*.vue'
	// }],
	css: [
		'dragula/dist/dragula.css',
	],
	devtools: {
		enabled: false
	},
	formkit: {
		// Experimental support for auto loading (see note):
		autoImport: true
	},
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
	ssr: false,
	vite: {
		build: {
			target: 'esnext',
		},
		vue: {
			script: {
				defineModel: true,
				propsDestructure: true
			}
		}
	},
})
