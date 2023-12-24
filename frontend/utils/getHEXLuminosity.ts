import type { HEX } from '~/types/PaletteColor';

const INVALID_HEX_COLOR_ERROR = 'Invalid HEX color';

export function getHEXLuminosity(hexColor: HEX): number {
	if (!isValidHexColor(hexColor)) {
		console.error(INVALID_HEX_COLOR_ERROR);
		return 0;
	}

	const rgb = hexToRGB(hexColor);
	return calculateLuminosity(rgb);
}

function isValidHexColor(hexColor: string): boolean {
	return /^#([0-9A-F]{3}){1,2}$/i.test(hexColor);
}

function hexToRGB(hexColor: string): { r: number, g: number, b: number } {
	let r, g, b;

	if (hexColor.length === 4) {
		r = parseInt(hexColor.charAt(1) + hexColor.charAt(1), 16);
		g = parseInt(hexColor.charAt(2) + hexColor.charAt(2), 16);
		b = parseInt(hexColor.charAt(3) + hexColor.charAt(3), 16);
	} else {
		r = parseInt(hexColor.substring(1, 3), 16);
		g = parseInt(hexColor.substring(3, 5), 16);
		b = parseInt(hexColor.substring(5, 7), 16);
	}

	return { r, g, b };
}

function calculateLuminosity(rgb: { r: number, g: number, b: number }): number {
	return 0.2126 * rgb.r + 0.7152 * rgb.g + 0.0722 * rgb.b;
}
