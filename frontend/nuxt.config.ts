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
		// The app is a client-side SPA (ssr: false), so it deploys as static files to Firebase Hosting.
		preset: 'static',
	},
	runtimeConfig: {
		public: {
			baseURL: '',
			apiURL: '',
			bugsnagApiKey: process.env.BUGSNAG_API_KEY,
			forgotPasswordURL: '',
			signUpURL: ''
		}
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
