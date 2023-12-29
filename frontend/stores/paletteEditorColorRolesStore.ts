import { defineStore } from 'pinia'
import type { PaletteColor } from '~/types/PaletteColor';
import { MainColorRole, OnColorRole } from '~/types/ColorRole';
import { ColorType } from '~/types/ColorType';

export const usePaletteEditorColorRolesStore = defineStore('paletteEditorColorRolesStore', () => {
	const regularColors = ref(new Array<PaletteColor>({
		role: MainColorRole.primary,
		value: {
			type: ColorType.Color,
			value: '#000000',
		}
	}));
	const onColors = ref(new Array<PaletteColor>({
		role: OnColorRole.onPrimary,
		value: {
			type: ColorType.Color,
			value: '#ffffff',
		}
	}));

	const colors = computed(() => [
		...regularColors.value,
		...onColors.value,
	]);

	return {
		colors,
		regularColors,
		onColors,
	}
})
