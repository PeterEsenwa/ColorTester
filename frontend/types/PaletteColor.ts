import { ColorType } from '~/types/ColorType';
import type { ColorIntensity } from '~/types/ColorIntensity';
import type { ColorRole } from '~/types/ColorRole';

interface ColorValueInterface {
	type: ColorType;
}

interface ColorValue extends ColorValueInterface {
	type: ColorType.Color;
	value: HEX;
}

interface ColorDictionaryValue extends ColorValueInterface {
	type: ColorType.Dictionary;
	value: Record<ColorIntensity, HEX>
}

export type HEX = `#${ string }`;

export interface PaletteColor {
	role: ColorRole;
	value: ColorValue | ColorDictionaryValue;
}
