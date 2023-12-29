import type { InjectionKey } from 'vue';
import type { PaletteColor } from '~/types/PaletteColor';

export const deleteColorRoleFnKey = Symbol('DeleteColorRoleFn') as InjectionKey<(color: PaletteColor) => void>;
