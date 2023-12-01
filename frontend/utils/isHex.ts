import type { HEX } from '~/types/PaletteColor';

export const isHex = (
	value: string | undefined
): value is HEX => {
	if (value === undefined) {
		return false;
	}

	return /^#[0-9A-F]{6}$/i.test(value);
}
