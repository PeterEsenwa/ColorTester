import type { PaletteColor } from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';
import { ColorIntensity } from '~/types/ColorIntensity';

export const getColor = (color: PaletteColor['value'] | undefined): string => {
	if (color === undefined) {
		return '';
	}

	if (color.type === ColorType.Color) {
		return color.value;
	}

	return color.value[ColorIntensity.Intensity500];
}
