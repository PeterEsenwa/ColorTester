import type { ConfigBase } from '@unocss/core';

const theme: ConfigBase['theme'] = {
	colors: {
		accent: {
			DEFAULT: '#FFCC80',
			light: '#FFCC80',
			dark: '#FFA726',
		},
		background: {
			DEFAULT: '#FFFFFF',
			light: '#FFFFFF',
			dark: '#212121',
		},
		error: {
			DEFAULT: '#FF7043',
			light: '#FF7043',
			dark: '#E64A19',
		},
		onaccent: {
			DEFAULT: '#000000',
			light: '#000000',
			dark: '#000000',
		},
		onbackground: {
			DEFAULT: '#303030',
			light: '#303030',
			dark: '#E0E0E0',
		},
		onprimary: {
			DEFAULT: '#FFFFFF',
			light: '#FFFFFF',
			dark: '#FFFFFF',
		},
		onsecondary: {
			DEFAULT: '#FFFFFF',
			light: '#FFFFFF',
			dark: '#FFFFFF',
		},
		primary: {
			DEFAULT: '#607D8B',
			light: '#607D8B',
			dark: '#455A64',
		},
		secondary: {
			DEFAULT: '#4DB6AC',
			light: '#4DB6AC',
			dark: '#00897B',
		},
		success: {
			DEFAULT: '#66BB6A',
			light: '#66BB6A',
			dark: '#43A047',
		}
	},

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
