import type { ColorRole } from '~/types/ColorRole';
import type { PaletteColor } from '~/types/PaletteColor';

export default (
	priorityRoles: ColorRole[],
	colorRoles: PaletteColor[]
) => {
	// move priority roles to the front
	return colorRoles.sort((a, b) => {
		const aIndex = priorityRoles.indexOf(a.role);
		const bIndex = priorityRoles.indexOf(b.role);

		if (aIndex === -1) {
			return 1;
		}
		if (bIndex === -1) {
			return -1;
		}
		return aIndex - bIndex;
	});
}
