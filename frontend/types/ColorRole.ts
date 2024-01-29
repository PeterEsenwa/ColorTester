import type { PaletteColor } from '~/types/PaletteColor';

export enum MainColorRole {
	primary = 'Primary',
	secondary = 'Secondary',
	background = 'Background',
	accent = 'Accent',
	error = 'Error',
	success = 'Success',
	warning = 'Warning',
}

export enum OnColorRole {
	onPrimary = 'OnPrimary',
	onSecondary = 'OnSecondary',
	onBackground = 'OnBackground',
	onAccent = 'OnAccent',
	onError = 'OnError',
	onSuccess = 'OnSuccess',
	onWarning = 'OnWarning',
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
