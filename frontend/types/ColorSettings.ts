import type { KeysOf } from '#app/composables/asyncData';
import type { RequireAtLeastOne } from 'type-fest';

export type ColorVariants =
	| { 50: string }
	| { 100: string }
	| { 200: string }
	| { 300: string }
	| { 400: string }
	| { 500: string }
	| { 600: string }
	| { 700: string }
	| { 800: string }
	| { 900: string }
	| string;

export enum ThemeColor {
	primary = 'primary',
	secondary = 'secondary',
	background = 'background',
	accent = 'accent',
	error = 'error',
	success = 'success',
	warning = 'warning',
	text = 'text',
}

export type ThemeColors = RequireAtLeastOne<{ [key in ThemeColor]: string }>;

const themeColors: ThemeColors = {
	primary: '',
}

export type ThemeColorKeys = KeysOf<ThemeColors>

type ThemeColorSettings = {
	themeType: 'default';
	colors: ThemeColors;
};

type LightColorSettings = {
	themeType: 'light';
	colors: ThemeColors;
};

type DarkColorSettings = {
	themeType: 'dark';
	colors: ThemeColors;
};

export type LightDarkColorSettings = LightColorSettings | DarkColorSettings;

export type ColorSettings = ThemeColorSettings | LightDarkColorSettings;
