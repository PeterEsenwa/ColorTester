import { useDark } from '@vueuse/core';

export default defineNuxtPlugin(() => {
	useDark({
		selector: 'body',
	})
});
