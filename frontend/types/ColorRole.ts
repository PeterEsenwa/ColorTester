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

const onColorRoleLookup: { [key in OnColorRole]: boolean } = Object.values(OnColorRole).reduce((acc, val) => {
	acc[val] = true;
	return acc;
}, {} as { [key in OnColorRole]: boolean });

export function isOnColorRole(role: string): role is OnColorRole {
	return onColorRoleLookup[role as OnColorRole];
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
