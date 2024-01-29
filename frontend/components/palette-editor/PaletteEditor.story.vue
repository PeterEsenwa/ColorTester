<template>
	<Story name="PaletteEditor">
		<PaletteEditor />
	</Story>
</template>

<script setup lang="ts">
import PaletteEditor from '~/components/palette-editor/PaletteEditor.vue';

provide(liteModeKey, ref(true));
</script>

<style scoped lang="scss">

</style>
