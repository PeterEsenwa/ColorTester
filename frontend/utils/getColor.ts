import type { PaletteColor } from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';
import { ColorIntensity } from '~/types/ColorIntensity';
import { isDef } from '@vueuse/shared';

export const getColor = (color: PaletteColor['value'] | undefined): string => {
	console.log(color);
	if (!isDef(color) || !isDef(color.value)) {
		return '';
	}

	if (color.type === ColorType.Color) {
		return color.value;
	}

	return color.value[ColorIntensity.Intensity500];
}
