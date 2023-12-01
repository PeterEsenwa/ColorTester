import { ColorType } from '~/types/ColorType';
import type { ColorIntensity } from '~/types/ColorIntensity';
import type { ColorRole } from '~/types/ColorRole';
import type { RequireAtLeastOne } from 'type-fest';

interface ColorValueInterface {
	type: ColorType;
}

export interface ColorValue extends ColorValueInterface {
	type: ColorType.Color;
	value: HEX;
}

export interface ColorDictionaryValue extends ColorValueInterface {
	type: ColorType.Dictionary;
	value: Record<ColorIntensity, HEX>
}

export type HEX = `#${ string }`;

export interface PaletteColor {
	role: ColorRole;
	value: ColorValue | ColorDictionaryValue;
}


export type PaletteColors = RequireAtLeastOne<Record<ColorRole, PaletteColor>>;
