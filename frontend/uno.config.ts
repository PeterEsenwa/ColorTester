import { defineConfig, presetAttributify, presetUno } from 'unocss'
import presetIcons from '@unocss/preset-icons'
import theme, { darkColors } from './configs/unocss/theme'
import transformerDirectives from '@unocss/transformer-directives'
import type { IconifyJSON } from '@iconify/types'
import presetTheme from 'unocss-preset-theme';
import type { Theme } from 'unocss/preset-uno'

export default defineConfig({
	presets: [
		presetAttributify({}),
		presetUno(),
		presetIcons({
			prefix: 'icon-',
			collections: {
				// svgSpinners: () => import('@iconify-json/svg-spinners/icons.json').then((m) => m.default),
				// googleMaterialIcons: () => import('@iconify-json/ic/icons.json').then((m) => m.default) as Promise<IconifyJSON>,
			}
		}),
		presetTheme<Theme>({
			theme: {
				dark: {
					colors: darkColors
				}
			}
		})
	],
	theme,
	transformers: [
		transformerDirectives()
	]
})
