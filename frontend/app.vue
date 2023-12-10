<template>
	<div id="app">
		<NuxtPage />
	</div>
</template>

<script setup lang="ts">
</script>

<style lang="scss">
@import '@unocss/reset/tailwind-compat.css';
@import '@fontsource-variable/source-sans-3';

html, body {
	font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;

	@apply h-full w-full m-0 p-0;
}

* {
	@apply box-border font-inherit;
}

#app {
	@apply p-2 h-100vh w-100vw;

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
