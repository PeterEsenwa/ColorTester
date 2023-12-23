import { defineConfig, presetAttributify, presetUno, type Preset } from 'unocss'
import presetIcons from '@unocss/preset-icons'
import theme, { darkColors } from './configs/unocss/theme'
import transformerDirectives from '@unocss/transformer-directives'
import presetTheme from 'unocss-preset-theme';
import type { Theme } from 'unocss/preset-uno'
import type { IconifyJSON } from '@iconify/types';
import presetRemToEM from './configs/unocss/remToEM';

export default defineConfig({
	presets: [
		presetAttributify({}),
		presetRemToEM(),
		presetUno(),
		presetIcons({
			prefix: 'icon-',
			collections: {
				svgSpinners: () => import('@iconify-json/svg-spinners/icons.json').then((m) => m.default),
				googleMaterialIcons: () => import('@iconify-json/ic/icons.json').then((m) => m.default) as Promise<IconifyJSON>,
				// Use Tabler Icons
				tablerIcons: () => import('@iconify-json/tabler/icons.json').then((m) => m.default),
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
	],
	shortcuts: {
		'text-body': 'text-base sm:text-lg md:text-xl',
		'text-body2': 'text-sm sm:text-base md:text-lg',
		'text-h1': 'text-4xl sm:text-5xl md:text-6xl',
		'text-h2': 'text-3xl sm:text-4xl md:text-5xl',
	}
})
