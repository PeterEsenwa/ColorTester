<template>
	<div id="app">
		<NuxtPage />
	</div>
</template>

<script setup lang="ts">
</script>

<style lang="scss">
html, body {
	font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;

	@apply h-full w-full m-0 p-0;

	#app {
		@apply p-8;
	}
}

* {
	@apply box-border font-inherit;
}

button {
	font-size: 1rem;
	--webkit-appearance: none;
}
</style>
