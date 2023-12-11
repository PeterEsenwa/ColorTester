import type { Theme } from 'unocss/preset-uno'

const lightColors = {
	accent: '#FFCC80',
	background: '#FFFFFF',
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
	surface: '#FAFAFA',
};

export const darkColors: Theme['colors'] = {
	accent: '#FFA726',
	background: '#212121',
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
	surface: '#424242',
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
	}
};

export default theme;
