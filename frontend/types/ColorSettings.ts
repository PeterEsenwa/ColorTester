import type { MainColorRoles } from '~/types/ColorRole';

type ThemeColorSettings = {
	themeType: 'default';
	colors: MainColorRoles;
};

type LightColorSettings = {
	themeType: 'light';
	colors: MainColorRoles;
};

type DarkColorSettings = {
	themeType: 'dark';
	colors: MainColorRoles;
};

export type LightDarkColorSettings = LightColorSettings | DarkColorSettings;

export type ColorSettings = ThemeColorSettings | LightDarkColorSettings;
