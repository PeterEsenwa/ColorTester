<template>
	<Story>
		<div class="flex flex-col gap-2">
			<svg-renderer
				:svg-path="StorySVG"
				:palette-colors="paletteColor"
			/>
		</div>
	</Story>
</template>

<script setup lang="ts">
import StorySVG from '@/samples/Chat_App.svg'
import PaletteService from '~/services/paletteService';
import type { PaletteColors } from '~/types/PaletteColor';

const paletteColor = ref<PaletteColors>()

onMounted(async () => {
	const result = await PaletteService.getPalettes()

	if (!result) {
		return
	}

	paletteColor.value = result[0].colors
})

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

/*const r = {
	'Primary': {
		'Id': '3fa85f64-5717-4562-b3fc-2c963f66afa6',
		'Role': 0,
		'Value': {
			'Type': 0,
			'Value': {
				'Red': 230,
				'Blue': 70,
				'Alpha': null,
				'Green': 57
			}
		}
	},
	"Secondary": {
		'Id': '3fa85f64-5717-4562-b3fc-2c963f66afa7',
		'Role': 'Secondary',
		'Value': {
			'Type': 0,
			'Value': {
				'Red': 168,
				'Blue': 220,
				'Alpha': null,
				'Green': 218
			}
		}
	},
	'Background': {
		'Id': '3fa85f64-5717-4562-b3fc-2c963f66afa8',
		'Role': 'Background',
		'Value': {
			'Type': 0,
			'Value': {
				'Red': 255,
				'Blue': 255,
				'Alpha': null,
				'Green': 255
			}
		}
	},
}*/
</script>

<style scoped>

</style>
