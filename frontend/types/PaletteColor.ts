import { ColorType } from '~/types/ColorType';
import { ColorIntensity } from '~/types/ColorIntensity';
import type { ColorRole } from '~/types/ColorRole';
import z from 'zod';
import { AllColorRoles, MainColorRole, OnColorRole } from '~/types/ColorRole';

export type HEX = `#${ string }`;
const HEXSchema = z.custom<HEX>((value: unknown) => {
	const regex = /^#([0-9a-f]{3}|[0-9a-f]{6}|[0-9a-f]{8})$/i;
	if (!regex.test(value as string)) {
		throw new Error('Invalid HEX color code');
	}
	return value as HEX;
});

interface ColorValueInterface {
	type: ColorType;
}

/**
 * Represents a color value as a HEX string.
 *
 * @example
 * const colorValue: ColorValue = {
 *   type: ColorType.Color,
 *   value: '#000000' // 6-digit HEX
 * }
 *
 * const shortColorValue: ColorValue = {
 *   type: ColorType.Color,
 *   value: '#000' // 3-digit HEX shorthand
 * }
 *
 * const alphaColorValue: ColorValue = {
 *   type: ColorType.Color,
 *   value: '#000000FF' // 8-digit HEX with alpha
 * }
 */
export interface ColorValue extends ColorValueInterface {
	type: ColorType.Color;
	value: HEX;
}

export const ColorValueSchema = z.object({
	// "type" here can only be "Color"
	type: z.literal(ColorType.Color),
	value: HEXSchema,
});

export const isColorValue = (value: unknown): value is ColorValue => {
	return ColorValueSchema.safeParse(value).success;
}

/**
 * Represents a color value as a dictionary of HEX strings, where the keys
 * correspond to color intensities defined in the `ColorIntensity` enum, and
 * the values are the respective HEX color codes.
 *
 * @example
 * const colorValue: ColorDictionaryValue = {
 *   type: ColorType.Dictionary,
 *   value: {
 *     [ColorIntensity.Light]: '#FFF', // Light intensity color with 3-digit HEX shorthand
 *     [ColorIntensity.Dark]: '#000000'   // Dark intensity color
 *   }
 * }
 */
export interface ColorDictionaryValue extends ColorValueInterface {
	type: ColorType.Dictionary;
	value: Record<ColorIntensity, HEX>
}

export const ColorDictionaryValueSchema = z.object({
	type: z.literal(ColorType.Dictionary),
	value: z.record(z.nativeEnum(ColorIntensity), HEXSchema),
});

export const isColorDictionaryValue = (value: unknown): value is ColorDictionaryValue => {
	return ColorDictionaryValueSchema.safeParse(value).success;
}

export interface PaletteColor {
	role: ColorRole;
	value?: ColorValue | ColorDictionaryValue | undefined;
}

export const PaletteColorSchema = z.object({
	// any non-undefined value
	role: z.nativeEnum(AllColorRoles),
	value: z.union([ColorValueSchema, ColorDictionaryValueSchema]).optional(),
});

export type PaletteColors = Partial<Record<MainColorRole | OnColorRole, PaletteColor>>;

export const PaletteColorsSchema = z.record(
	z.nativeEnum(AllColorRoles),
	PaletteColorSchema
);

// utility function to convert Record<ColorRole, PaletteColor> to PaletteColor[]
export const PaletteColorsToArray = (
	paletteColors: PaletteColors
): PaletteColor[] => {
	return Object.entries(paletteColors).map(([role, value]) => {
		return value;
	});
};
