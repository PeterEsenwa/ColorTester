import { type ThemeColors, ThemeColor, type ColorVariants } from '~/types/ColorSettings';
import type { Entries } from 'type-fest';

export const generateThemeColorsObject = (): ThemeColors => {
	const keys = Object.values(ThemeColor);

	const colors = {} as ThemeColors;

	for (const key of keys) {
		colors[key] = '';
	}

	return colors;
};

export const getColor = (color: ColorVariants | undefined): string => {
	if (!color) {
		return '';
	}

	if (typeof color === 'string') {
		return color;
	}

	return Object.values(color)[0];
}

export const getThemeColorsEntries = (colors: ThemeColors): Entries<ThemeColors> => {
	return Object.entries(colors) as Entries<ThemeColors>;
}
