import { defineConfig, presetAttributify, presetUno, type Preset } from 'unocss'
import presetIcons from '@unocss/preset-icons'
import theme, { darkColors } from './configs/unocss/theme'
import transformerDirectives from '@unocss/transformer-directives'
import presetTheme from 'unocss-preset-theme';
import type { Theme } from 'unocss/preset-uno'
import type { IconifyJSON } from '@iconify/types';

export default defineConfig({
	presets: [
		presetAttributify({}),
		presetUno(),
		presetIcons({
			prefix: 'icon-',
			collections: {
				svgSpinners: () => import('@iconify-json/svg-spinners/icons.json').then((m) => m.default),
				googleMaterialIcons: () => import('@iconify-json/ic/icons.json').then((m) => m.default) as Promise<IconifyJSON>,
			}
		}),
		presetTheme<Theme>({
			theme: {
				dark: {
					colors: darkColors
				}
			}
		}) as Preset<Theme>
	],
	theme,
	transformers: [
		transformerDirectives()
	]
})
