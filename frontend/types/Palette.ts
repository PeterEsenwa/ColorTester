import type { Auditable } from '~/types/Auditable';
import { AuditableSchema } from '~/types/Auditable';
import type { PaletteColors } from '~/types/PaletteColor';
import { PaletteColorsSchema } from '~/types/PaletteColor';
import z from 'zod';

export type Palette = Auditable<{
	id: string;
	colors: PaletteColors;
}>

export const PaletteSchema = z.object({
	id: z.string(),
	colors: PaletteColorsSchema,
}).merge(AuditableSchema);

export type Palettes = Auditable<{
	id: string;
	colors: PaletteColors;
}>[]

export const PalettesSchema = z.array(PaletteSchema);
