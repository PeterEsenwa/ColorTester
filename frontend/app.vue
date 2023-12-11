<template>
	<div
		id="app"
	>
		<NuxtPage />
	</div>
</template>

<script setup lang="ts">
import { useDark } from '@vueuse/core';

useDark({
	selector: 'body',
});
</script>

<style lang="scss">
@import '@unocss/reset/tailwind-compat.css';
@import '@fontsource-variable/source-sans-3';

html, body {
	font-family: 'Source Sans 3', sans-serif;

	@apply h-full w-full m-0 p-0;
}

* {
	@apply box-border font-inherit;
}

#app {
	@apply h-100vh w-100vw bg-background text-on-background;

	/*	background-image: url('/FlowyColours.svg');
		background-repeat: no-repeat;
		background-position: center;
		background-size: cover;*/
}

button {
	font-size: 1rem;
	--webkit-appearance: none;
}
</style>
