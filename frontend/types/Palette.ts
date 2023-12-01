import type { Auditable } from '~/types/Auditable';
import type { ColorRole } from '~/types/ColorRole';
import type { PaletteColor, PaletteColors } from '~/types/PaletteColor';

export type Palettes = Auditable<{
	id: string;
	colors: Record<ColorRole, PaletteColor>
}>[]

export type Palette = Auditable<{
	id: string;
	colors: PaletteColors;
}>
