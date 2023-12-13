<template>
	<Story title="PaletteSquare">
		<div class="min-w-20vw w-full h-full items-center justify-center">
			<RadioGroupRoot
				v-model="selectedColor"
				class="flex flex-row gap-4"
			>
				<PaletteSquare :palette-colors="paletteColors" />
				<PaletteSquare :palette-colors="paletteColorsTwo" />
				<PaletteSquare :palette-colors="paletteColorsThree" />
			</RadioGroupRoot>
		</div>
	</Story>
</template>

<script setup lang="ts">
import type { PaletteColors } from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';

const paletteColors = ref<Partial<PaletteColors>>({
	accent: {
		role: 'accent',
		value: {
			type: ColorType.Color,
			value: '#FC5130',
		},
	},
	background: {
		role: 'background',
		value: {
			type: ColorType.Color,
			value: '#FDFDFD',
		},
	},
	error: {
		role: 'error',
		value: {
			type: ColorType.Color,
			value: '#D7263D',
		},
	},
	primary: {
		role: 'primary',
		value: {
			type: ColorType.Color,
			value: '#2F3061',
		},
	},
	secondary: {
		role: 'secondary',
		value: {
			type: ColorType.Color,
			value: '#FFD166',
		},
	},
})

const paletteColorsThree = ref<Partial<PaletteColors>>({
	accent: {
		role: 'accent',
		value: {
			type: ColorType.Color,
			value: '#FF9B85',
		},
	},
	background: {
		role: 'background',
		value: {
			type: ColorType.Color,
			value: '#FFF5EB',
		},
	},
	error: {
		role: 'error',
		value: {
			type: ColorType.Color,
			value: '#FF616D',
		},
	},
	primary: {
		role: 'primary',
		value: {
			type: ColorType.Color,
			value: '#6A8D73',
		},
	},
	secondary: {
		role: 'secondary',
		value: {
			type: ColorType.Color,
			value: '#A7BED3',
		},
	},
})

const paletteColorsTwo = ref<Partial<PaletteColors>>({
	accent: {
		role: 'accent',
		value: {
			type: ColorType.Color,
			value: '#FF9B85',
		},
	},
	background: {
		role: 'background',
		value: {
			type: ColorType.Color,
			value: '#FFF5EB',
		},
	},
	error: {
		role: 'error',
		value: {
			type: ColorType.Color,
			value: '#FF616D',
		},
	},
	primary: {
		role: 'primary',
		value: {
			type: ColorType.Color,
			value: '#6A8D73',
		},
	},
	secondary: {
		role: 'secondary',
		value: {
			type: ColorType.Color,
			value: '#A7BED3',
		},
	},
})

const selectedColor = ref()
</script>

<style scoped lang="scss">

</style>
