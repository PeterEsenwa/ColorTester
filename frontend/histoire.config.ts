import { defineConfig } from 'histoire'
import { HstVue } from '@histoire/plugin-vue'
import { HstNuxt } from '@histoire/plugin-nuxt'
import { resolve } from 'path'

export default defineConfig({
	plugins: [
		HstVue(),
		HstNuxt()
	],
	setupFile: './histoire.setup.ts',
	storyMatch: [
		'**/*.story.vue',
		'**/story.vue'
	],
	storyIgnored: [
		'**/node_modules/**',
		'**/dist/**',
	],
	theme: {
		darkClass: 'dark-theme'
	},
	vite: {
		// watch files
		build: {
			watch: {
				include: [
					'**/*.vue',
					'**/*.ts',
				]
			}
		},
		css: {
			preprocessorOptions: {
				scss: {
					// NOTE: this was a workaround to include the headless-web-components styles
					// additionalData: `
					// 	@import './node_modules/headless-web-components/dist/style.css';
					// `
				}
			}
		},
		// alias @
		resolve: {
			alias: {
				'@': resolve(__dirname, './'),
				'~': resolve(__dirname, './'),
				'vue': 'vue/dist/vue.esm-bundler.js'
			}
		}
	}
})
