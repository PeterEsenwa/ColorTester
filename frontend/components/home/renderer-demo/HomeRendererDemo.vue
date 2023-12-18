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

const paletteColorsThree = ref<Partial<PaletteColors>>({
		accent: {
			role: 'accent',
			value: {
				type: ColorType.Color,
				value: '#C84B31',
			},
		},
		background: {
			role: 'background',
			value: {
				type: ColorType.Color,
				value: '#0D1B2A',
			},
		},
		error: {
			role: 'error',
			value: {
				type: ColorType.Color,
				value: '#E40017',
			},
		},
		primary: {
			role: 'primary',
			value: {
				type: ColorType.Color,
				value: '#415A77',
			},
		},
		secondary: {
			role: 'secondary',
			value: {
				type: ColorType.Color,
				value: '#EAE8FF',
			},
		},
	})

const selectedColor = ref();
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
