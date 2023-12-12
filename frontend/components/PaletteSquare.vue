<template>
	<RadioGroupItem
		class="palette"
		:value="paletteColors"
	>
		<div
			v-for="([name, color], index) in filteredColors"
			:key="index"
			:class="[name, 'color']"
			:style="{ backgroundColor: getColor(color) }"
		/>
	</RadioGroupItem>
</template>

<script setup lang="ts">
import { defineProps } from 'vue';
import type { PaletteColors, PaletteColor } from '~/types/PaletteColor';

const props = defineProps<{
	paletteColors: PaletteColors;
}>();

const filterOnRoles = (colors: PaletteColors) => Object.entries<PaletteColor>(colors).filter(([ name ]) => !name.toLowerCase().startsWith('on'));

const filteredColors = computed(() => filterOnRoles(props.paletteColors));
</script>

<style scoped lang="scss">
.palette {
	@apply flex flex-col;
	@apply w-3em h-3em box-content rounded-md overflow-hidden border-0.2em
	@apply bg-transparent border-transparent;

	&[data-state='checked'] {
		@apply border-accent;
	}

	.color {
		@apply flex-1 w-full h-full;
	}
}

</style>
