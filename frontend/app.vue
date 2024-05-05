<template>
	<NuxtLayout
		id="app"
	>
		<NuxtPage />
	</NuxtLayout>
</template>

<script setup lang="ts">
</script>

<style lang="scss">
@import '@/assets/styles/typography.css';
@import '@unocss/reset/tailwind-compat.css';
@import '@fontsource-variable/source-sans-3';

html, body {
	@apply h-full w-full m-0 p-0 font-ss3v;
}

* {
	@apply box-border font-inherit;
}

#app {
	@apply min-h-100vh bg-background text-on-background;

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
