<template>
	<div
		v-if="showPalette"
		class="palette-view"
	>
		<TooltipProvider>
			<LazyColorRole
				v-for="color in shownColors"
				:key="color.role"
				v-model:color="color.value"
				v-model:role="color.role"
			/>
		</TooltipProvider>
	</div>
</template>

<script setup lang="ts">
import type { PaletteColor } from '~/types/PaletteColor';
import { TooltipProvider } from 'radix-vue';
import sortPriorityRoles from '~/utils/sortPriorityRoles';
import type { ColorRole } from '~/types/ColorRole';

provide(liteModeKey, ref(true));

const props = withDefaults(defineProps<{
	colors: PaletteColor[];
	isLoading?: boolean;
	showOnColors?: boolean;
	maxColors?: number;
}>(), {
	showOnColors: false,
	maxColors: 5,
	isLoading: true,
})

const priorityRoles: ColorRole[] = [ 'Primary', 'OnPrimary', 'Secondary', 'OnSecondary', 'Background' ];

const shownColors = computed(() => {
	let colors = props.showOnColors ? props.colors : props.colors.filter(color => !color.role.toLowerCase().startsWith('on'));

	colors = sortPriorityRoles(priorityRoles, colors);

	colors = colors.slice(0, props.maxColors);

	if (props.showOnColors) {
		const mainColorIndices: Record<string, number> = colors.reduce((map, color, index) => {
			if (!color.role.startsWith('on')) {
				map[color.role] = index;
			}
			return map;
		}, {} as Record<string, number>);

		for (let i = 0; i < colors.length; i++) {
			const color = colors[i];
			if (color.role.startsWith('on')) {
				const mainColorRole = color.role.substring(2);
				const mainColorIndex = mainColorIndices[mainColorRole];
				if (mainColorIndex > -1 && mainColorIndex < i) {
					colors.splice(i, 1);
					colors.splice(mainColorIndex + 1, 0, color);
				}
			}
		}
	}

	return colors;
});

// A Palette must have at least 2 colors
const showPalette = computed(() => shownColors.value.length > 1);
</script>

<style scoped lang="scss">
.palette-view {
	--palette-view-color-size: 3rem;

	@apply flex flex-row gap-0;
	@apply w-full;
	@apply rounded-md;
}
</style>
