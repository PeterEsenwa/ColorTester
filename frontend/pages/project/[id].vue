<template>
	<div class="renderer-editor-container">
		<SvgRenderer :palette-colors="staticPalette" :svg-path="ChatAppSVG"/>
		<PaletteEditor/>
	</div>
</template>

<script setup lang="ts">
import ChatAppSVG from '@/samples/Chat_App.svg'
import SvgRenderer from '~/components/SvgRenderer.vue';
import PaletteEditor from '~/components/palette-editor/PaletteEditor.vue';
import {usePaletteEditorColorRolesStore} from "~/stores/paletteEditorColorRolesStore";
import type {PaletteColor} from "~/types/PaletteColor";
import {ColorType} from "~/types/ColorType";
import {watchDebounced} from "@vueuse/core";

const rolesStore = usePaletteEditorColorRolesStore();

// Define your static palette here
const staticPalette = ref<PaletteColor[]>();

watchDebounced(() => rolesStore.colors, () => {
	staticPalette.value = rolesStore.colors;
}, {
	deep: true,
	debounce: 150
});

onMounted(() => {
	const palette: PaletteColor[] = [
		{
			role: 'Accent',
			value: {
				value: '#FF9B85',
				type: ColorType.Color,
			},
		},
		{
			role: 'Background',
			value: {
				value: '#FFF5EB',
				type: ColorType.Color,
			},
		},
		{
			role: 'Error',
			value: {
				value: '#FF616D',
				type: ColorType.Color,
			},
		},
		{
			role: 'Primary',
			value: {
				value: '#6A8D73',
				type: ColorType.Color,
			},
		},
		{
			role: 'Secondary',
			value: {
				value: '#A7BED3',
				type: ColorType.Color,
			},
		},
	]
	rolesStore.regularColors = palette.filter((color) => !color.role.startsWith('On'));
	rolesStore.onColors = palette.filter((color) => color.role.startsWith('On'));
});
</script>

<style scoped lang="scss">
.renderer-editor-container {
  display: flex;
  justify-content: space-between;
}
</style>
