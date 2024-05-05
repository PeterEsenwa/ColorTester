<template>
	<Story>
		<div class="flex flex-col gap-2">
			<svg-renderer
				:svg-path="StorySVG"
				:palette-colors="paletteColors"
			/>
		</div>
	</Story>
</template>

<script setup lang="ts">
import StorySVG from '@/samples/Chat_App.svg'
import type {PaletteColor} from "~/types/PaletteColor";
import {MainColorRole} from "~/types/ColorRole";
import {ColorType} from "~/types/ColorType";

const paletteColors: PaletteColor[] = [
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

/*const paletteColors: ColorSettings = {
	themeType: 'default',
	colors: {
		primary: {
			type: ColorType.Color,
			value: '#1D3557'
		},
		secondary: {
			type: ColorType.Color,
			value: '#A8DADC'
		},
		background: {
			type: ColorType.Color,
			value: '#F1FAEE'
		},
		accent: {
			type: ColorType.Color,
			value: '#457B9D'
		},
		error: {
			type: ColorType.Color,
			value: '#E63946'
		}
	}
}*/
</script>

<style scoped>

</style>
