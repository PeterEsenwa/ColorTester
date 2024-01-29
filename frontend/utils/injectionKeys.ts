import type { InjectionKey, Ref } from 'vue';
import type { PaletteColor } from '~/types/PaletteColor';

export const deleteColorRoleFnKey = Symbol('DeleteColorRoleFn') as InjectionKey<(color: PaletteColor) => void>;

// inject liteMode into ColorView and its parent, ColorRole
export const liteModeKey = Symbol('LiteMode') as InjectionKey<Ref<boolean>>;
