<template>
	<div class="palette-view">
		<LazyColorRole
			v-for="color in props.colors"
			:key="color.role"
			v-model:color="color.value"
			v-model:role="color.role"
		/>
	</div>
</template>

<script setup lang="ts">
import type { PaletteColor } from '~/types/PaletteColor';

provide(liteModeKey, ref(true));

const props = defineProps<{
	colors: PaletteColor[]
}>()
</script>

<style scoped lang="scss">
.palette-view {
	--palette-view-color-size: 3rem;
	--palette-view-orientation: row;

	@apply flex flex-[--palette-view-orientation] gap-0;
	@apply w-max;
	@apply rounded-md overflow-hidden;
}
</style>
