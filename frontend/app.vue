<template>
	<div id="app">
		<NuxtPage />
	</div>
</template>

<script setup lang="ts">
</script>

<style lang="scss">
@import url('@fontsource-variable/source-sans-3');

html, body {
	font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;

	@apply h-full w-full m-0 p-0;
}

* {
	@apply box-border font-inherit;
}

#app {
	@apply p-8 h-100vh w-100vw;
	@apply flex flex-col items-center justify-center gap-4;

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
