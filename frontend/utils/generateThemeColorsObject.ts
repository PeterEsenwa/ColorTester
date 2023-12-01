import type { PaletteColor } from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';
import {
	type MainColorRoles,
	MainColorRole,
	type OnColorRoles,
	OnColorRole
} from '~/types/ColorRole';

export const generateThemeColorsObject = (): MainColorRoles => {
	const keys = Object.values(MainColorRole);

	const colors = {} as MainColorRoles;

	for (const key of keys) {
		colors[key] = {
			role: key,
			value: {
				type: ColorType.Color,
				value: '#FFFFFF'
			}
		} as PaletteColor;
	}

	return colors;
};

export const generateThemeOnColorsObject = (): OnColorRoles => {
	const keys = Object.values(OnColorRole);

	const colors = {} as OnColorRoles;

	for (const key of keys) {
		colors[key] = {
			role: key,
			value: {
				type: ColorType.Color,
				value: '#FFFFFF'
			}
		} as PaletteColor;
	}

	return colors;
}
