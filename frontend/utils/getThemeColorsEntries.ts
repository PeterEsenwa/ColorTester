import type { MainColorRoles } from '~/types/ColorRole';
import type { Entries, StringKeyOf } from 'type-fest';

export const getThemeColorsEntries = (colors: MainColorRoles): Entries<MainColorRoles> => {
	return Object.entries(colors) as Entries<MainColorRoles>;
}
