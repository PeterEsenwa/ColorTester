<template>
	<div class="home-renderer-demo">
		<RadioGroupRoot
			v-model="selectedColor"
			class="palette-selector"
		>
			<PaletteSquare
				:palette-colors="paletteColors"
			/>
			<PaletteSquare
				:palette-colors="paletteColorsTwo"
			/>
			<PaletteSquare
				:palette-colors="paletteColorsThree"
			/>
		</RadioGroupRoot>
		<SvgRenderer
			:svg-path="StorySVG"
			:palette-colors="selectedColor"
		/>
	</div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import StorySVG from '@/samples/Chat_App.svg';
import type {PaletteColor, PaletteColors} from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';


const paletteColors = ref<PaletteColor[]>([
	{
		role: 'Accent',
		value: {
			value: '#FC5130',
			type: ColorType.Color,
		},
	},
	{
		role: 'Background',
		value: {
			value: '#FDFDFD',
			type: ColorType.Color,
		},
	},
	{
		role: 'Error',
		value: {
			value: '#D7263D',
			type: ColorType.Color,
		},
	},
	{
		role: 'Primary',
		value: {
			value: '#2F3061',
			type: ColorType.Color,
		},
	},
	{
		role: 'Secondary',
		value: {
			value: '#FFD166',
			type: ColorType.Color,
		},
	},
])

const paletteColorsTwo = ref<PaletteColor[]>([
	{
		role: 'Accent',
		value: {
			value: '#FF9B85',
			type: ColorType.Color,
		},
	},
	{
		role: 'Background',
		value: {
			value: '#FFF5EB',
			type: ColorType.Color,
		},
	},
	{
		role: 'Error',
		value: {
			value: '#FF616D',
			type: ColorType.Color,
		},
	},
	{
		role: 'Primary',
		value: {
			value: '#6A8D73',
			type: ColorType.Color,
		},
	},
	{
		role: 'Secondary',
		value: {
			value: '#A7BED3',
			type: ColorType.Color,
		},
	},
])

const paletteColorsThree = ref<PaletteColor[]>([
	{
		role: 'Accent',
		value: {
			value: '#C84B31',
			type: ColorType.Color,
		},
	},
	{
		role: 'Background',
		value: {
			value: '#0D1B2A',
			type: ColorType.Color,
		},
	},
	{
		role: 'Error',
		value: {
			value: '#E40017',
			type: ColorType.Color,
		},
	},
	{
		role: 'Primary',
		value: {
			value: '#415A77',
			type: ColorType.Color,
		},
	},
	{
		role: 'Secondary',
		value: {
			value: '#EAE8FF',
			type: ColorType.Color,
		},
	},
])

const selectedColor = ref<PaletteColor[]>(paletteColors.value);
</script>

<style scoped lang="scss">
.home-renderer-demo {
	@apply grid gap-4 justify-center items-center aspect-square overflow-hidden;
	@apply bg-surface rounded-lg shadow-lg;
	@apply border-1em border-surface;

	.palette-selector {
		@apply flex flex-row gap-4 justify-center;
	}
}
</style>
