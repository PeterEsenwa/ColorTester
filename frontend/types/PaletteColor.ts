import { ColorType } from '~/types/ColorType';
import type { ColorIntensity } from '~/types/ColorIntensity';
import type { ColorRole } from '~/types/ColorRole';

interface ColorValueInterface {
	type: ColorType;
}

export type HEX = `#${ string }`;

export const isHEX = (value: any): value is HEX => {
	return /^#[0-9A-F]{6}$/i.test(value);
}

interface ColorValue extends ColorValueInterface {
	type: ColorType.Color;
	value: HEX;
}

interface ColorDictionaryValue extends ColorValueInterface {
	type: ColorType.Dictionary;
	value: Record<ColorIntensity, HEX>
}

export interface PaletteColor {
	role: ColorRole;
	value: ColorValue | ColorDictionaryValue;
}
