import { defineConfig, presetAttributify, presetUno } from 'unocss'
import presetIcons from '@unocss/preset-icons'
import transformerDirectives from '@unocss/transformer-directives'
import type { IconifyJSON } from '@iconify/types'

export default defineConfig({
	presets: [
		presetUno(),
		presetIcons({
			prefix: 'icon-',
			collections: {
				svgSpinners: () => import('@iconify-json/svg-spinners/icons.json').then((m) => m.default),
				googleMaterialIcons: () => import('@iconify-json/ic/icons.json').then((m) => m.default) as Promise<IconifyJSON>,
			}
		}),
		presetAttributify({}),
	],
	transformers: [
		transformerDirectives()
	]
})
