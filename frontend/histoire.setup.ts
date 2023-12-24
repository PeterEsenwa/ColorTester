import 'virtual:uno.css'
import '@unocss/reset/tailwind-compat.css'
import '@fontsource-variable/source-sans-3';
import { defineSetupVue3 } from '@histoire/plugin-vue';

export const setupVue3 = defineSetupVue3(({ addWrapper }) => {
	addWrapper({
		// use setup() instead of render()
		setup(props, { slots }) {
			return () => {
				return h(
					'div', {
						class: 'flex flex-col w-full h-full font-ss3v'
					}, slots?.default?.()
				);
			}
		}
	})
});

