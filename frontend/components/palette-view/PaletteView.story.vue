<template>
	<Story name="PaletteView">
		<PaletteView :colors="colors" />
	</Story>
</template>

<script setup lang="ts">
import PaletteView from '~/components/palette-view/PaletteView.vue';
import type { PaletteColor } from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';
import { MainColorRole } from '~/types/ColorRole';

// Provide some mock colors for the story
const colors: PaletteColor[] = [
	{
		role: MainColorRole.accent,
		value: {
			type: ColorType.Color,
			value: '#FDFDFD',
		},
	},
	{
		role: MainColorRole.primary,
		value: {
			type: ColorType.Color,
			value: '#2F3061',
		},
	},
	{
		role: MainColorRole.secondary,
		value: {
			type: ColorType.Color,
			value: '#FFD166',
		},
	},
];
</script>

<style scoped lang="scss">

</style>
