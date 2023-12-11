import type { Theme } from 'unocss/preset-uno'

const lightColors = {
	accent: '#FFCC80',
	background: '#EFEFEF',
	error: '#FF7043',
	'on-accent': '#000000',
	'on-error': '#000000',
	'on-background': '#303030',
	'on-primary': '#FFFFFF',
	'on-secondary': '#FFFFFF',
	'on-surface': '#000000',
	primary: '#607D8B',
	secondary: '#4DB6AC',
	success: '#66BB6A',
	surface: '#FFFFFF',
};

export const darkColors: Theme['colors'] = {
	accent: '#FFA726',
	background: '#282828',
	error: '#E64A19',
	'on-accent': '#000000',
	'on-error': '#000000',
	'on-background': '#E0E0E0',
	'on-primary': '#FFFFFF',
	'on-secondary': '#FFFFFF',
	'on-surface': '#FFFFFF',
	primary: '#455A64',
	secondary: '#00897B',
	success: '#43A047',
	surface: '#212121',
}

const theme: Theme = {
	colors: lightColors,
	// TODO: Use Noto family of fonts.
	//  This will make it easier to support other languages.
	fontFamily: {
		/**
		 * @description Represents the font family for Source Sans Pro, a sans-serif font.
		 * @constant {string}
		 * @see https://fonts.google.com/specimen/Source+Sans+Pro
		 */
		ssp: 'Source Sans Pro, sans-serif',

		/**
		 * @description Represents the font family for Source Sans 3, a variable sans-serif font.
		 * @constant {string}
		 * @see https://fontsource.org/fonts/source-sans-3
		 */
		ss3v: '\'Source Sans 3 Variable\', sans-serif',
	},
	fontSize: {
		xs: [ 'var(--font-size-1)', '1em' ],
		sm: [ 'var(--font-size-2)', '1.25em' ],
		base: [ 'var(--font-size-3)', '1.5em' ],
		lg: [ 'var(--font-size-4)', '1.75em' ],
		xl: [ 'var(--font-size-5)', '1.75em' ],
		'2xl': [ 'var(--font-size-6)', '2em' ],
		'3xl': [ 'var(--font-size-7)', '2.25em' ],
		'4xl': [ 'var(--font-size-8)', '2.5em' ],
		'5xl': 'var(--font-size-9)',
		'6xl': 'var(--font-size-10)',
		'7xl': 'var(--font-size-11)',
		'8xl': 'var(--font-size-12)',
		'9xl': 'var(--font-size-13)',
	}
};

export default theme;
