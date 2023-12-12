<template>
	<Story title="PaletteSquare">
		<div class="min-w-20vw w-full h-full items-center justify-center">
			<RadioGroupRoot
				v-model="selectedColor"
				class="flex flex-row gap-4"
			>
				<PaletteSquare :palette-colors="paletteColors" />
				<PaletteSquare :palette-colors="paletteColorsOther" />
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
			value: '#457B9D',
		},
	},
	background: {
		role: 'background',
		value: {
			type: ColorType.Color,
			value: '#F1FAEE',
		},
	},
	error: {
		role: 'error',
		value: {
			type: ColorType.Color,
			value: '#E63946',
		},
	},
	primary: {
		role: 'primary',
		value: {
			type: ColorType.Color,
			value: '#1D3557',
		},
	},
	secondary: {
		role: 'secondary',
		value: {
			type: ColorType.Color,
			value: '#A8DADC',
		},
	},
	onAccent: {
		role: 'onAccent',
		value: {
			type: ColorType.Color,
			value: '#F1FAEE',
		},
	},
	onBackground: {
		role: 'onBackground',
		value: {
			type: ColorType.Color,
			value: '#1D3557',
		},
	},
	onError: {
		role: 'onError',
		value: {
			type: ColorType.Color,
			value: '#F1FAEE',
		},
	},
	onPrimary: {
		role: 'onPrimary',
		value: {
			type: ColorType.Color,
			value: '#F1FAEE',
		},
	},
	onSecondary: {
		role: 'onSecondary',
		value: {
			type: ColorType.Color,
			value: '#1D3557',
		},
	},
})

const paletteColorsOther = ref<Partial<PaletteColors>>({
	accent: {
		role: 'accent',
		value: {
			type: ColorType.Color,
			value: '#589d45',
		},
	},
	background: {
		role: 'background',
		value: {
			type: ColorType.Color,
			value: '#F1FAEE',
		},
	},
	error: {
		role: 'error',
		value: {
			type: ColorType.Color,
			value: '#c51c28',
		},
	},
	primary: {
		role: 'primary',
		value: {
			type: ColorType.Color,
			value: '#441d57',
		},
	},
	secondary: {
		role: 'secondary',
		value: {
			type: ColorType.Color,
			value: '#A8DADC',
		},
	},
	onAccent: {
		role: 'onAccent',
		value: {
			type: ColorType.Color,
			value: '#ffffff',
		},
	},
	onBackground: {
		role: 'onBackground',
		value: {
			type: ColorType.Color,
			value: '#1D3557',
		},
	},
	onError: {
		role: 'onError',
		value: {
			type: ColorType.Color,
			value: '#F1FAEE',
		},
	},
	onPrimary: {
		role: 'onPrimary',
		value: {
			type: ColorType.Color,
			value: '#F1FAEE',
		},
	},
	onSecondary: {
		role: 'onSecondary',
		value: {
			type: ColorType.Color,
			value: '#1D3557',
		},
	},

})

const selectedColor = ref()
</script>

<style scoped lang="scss">

</style>
