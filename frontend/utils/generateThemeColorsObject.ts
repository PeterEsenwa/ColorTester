import type { PaletteColor } from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';
import { type MainColorRoles, MainColorRole } from '~/types/ColorRole';

export const generateThemeColorsObject = (): MainColorRoles => {
	const keys = Object.values(MainColorRole);

	const colors = {} as MainColorRoles;

	for (const key of keys) {
		colors[key] = {
			type: ColorType.Color,
			value: '#000000'
		} as PaletteColor['value'];
	}

	return colors;
};
