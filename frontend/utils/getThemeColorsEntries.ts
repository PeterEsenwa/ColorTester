import type { MainColorRoles } from '~/types/ColorRole';
import type { Entries } from 'type-fest';

export const getThemeColorsEntries = (colors: MainColorRoles): Entries<MainColorRoles> => {
	return Object.entries(colors) as Entries<MainColorRoles>;
}
