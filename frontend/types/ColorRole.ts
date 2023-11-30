import type { RequireAtLeastOne } from 'type-fest';
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

export type MainColorRoles = RequireAtLeastOne<{ [key in MainColorRole]: PaletteColor['value'] }>;

export type OnColorRoles = RequireAtLeastOne<{ [key in OnColorRole]: PaletteColor['value'] }>;

export type ColorRoles = MainColorRoles & OnColorRoles;

export type ColorRole = `${ MainColorRole }` | `${ OnColorRole }`;
