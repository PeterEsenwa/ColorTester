import type { PaletteColor } from '~/types/PaletteColor';

export enum MainColorRole {
	primary = 'primary',
	secondary = 'secondary',
	background = 'background',
	accent = 'accent',
	error = 'error',
	success = 'success',
	warning = 'warning',
}

export enum OnColorRole {
	onPrimary = 'onPrimary',
	onSecondary = 'onSecondary',
	onBackground = 'onBackground',
	onAccent = 'onAccent',
	onError = 'onError',
	onSuccess = 'onSuccess',
	onWarning = 'onWarning',
}

// group main colors and on colors (accessible at runtime)
export const AllColorRoles = {
	...MainColorRole,
	...OnColorRole,
}

export type MainColorRoles = { [key in MainColorRole]: PaletteColor };

export type OnColorRoles = { [key in OnColorRole]: PaletteColor };

export type ColorRoles = MainColorRoles & OnColorRoles;

export type ColorRole = `${ MainColorRole }` | `${ OnColorRole }`;
