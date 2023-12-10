import type { PaletteColor } from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';
import { ColorIntensity } from '~/types/ColorIntensity';
import { isDef } from '@vueuse/shared';

export const getColor = (color: PaletteColor | undefined): string => {
	if (!isDef(color) || !isDef(color.value)) {
		return '';
	}

	const colorValue = color.value;

	if (colorValue.type === ColorType.Color) {
		return colorValue.value;
	}

	return colorValue.value[ColorIntensity.Intensity500];
}
