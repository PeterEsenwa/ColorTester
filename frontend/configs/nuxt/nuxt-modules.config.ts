import type { InputConfig } from 'c12';
import type { NuxtConfig } from '@nuxt/schema';

export const nuxtModulesConfig: InputConfig<NuxtConfig> = {
	modules: [
		'@unocss/nuxt',
		'nuxt-headlessui',
		'nuxt-monaco-editor',
	],
}
