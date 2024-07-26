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
			v-if="DemoSVG"
			:svg="DemoSVG"
			:palette-colors="selectedColor"
		/>
		<div v-else
			class="w-[365px] h-[300px]"
		>
		</div>
	</div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import type {PaletteColor, PaletteColors} from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';
import MockupService from "~/services/mockup.service";
import {useAuth0} from "@auth0/auth0-vue";

const authClient = useAuth0()
const {
	$apiFetch
} = useNuxtApp();

const DemoSVG = ref<string>();

// fetch mockup SVG from new API
watchEffect(async () => {
	// use MockupService
	const mockupService = new MockupService($apiFetch.value);
	mockupService.fetchMockup('Chat_App.svg').then((mockup) => {
		DemoSVG.value = mockup;
	});
});

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
