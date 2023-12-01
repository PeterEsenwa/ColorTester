import type { HEX } from '~/types/PaletteColor';
import { type MainColorRoles, MainColorRole } from '~/types/ColorRole';

export const isHex = (
	value: string | undefined
): value is HEX => {
	if (value === undefined) {
		return false;
	}

	return /^#[0-9A-F]{6}$/i.test(value);
}

const isMainColorRoles = (value: unknown): value is MainColorRoles => {
	if (typeof value !== 'object' || value === null) {
		return false;
	}

	const keys = Object.values(MainColorRole);

	// if at least one key is found, then it's a valid object
	for (const key of keys) {
		if (key in value) {
			return true;
		}
	}

	return false;
}
