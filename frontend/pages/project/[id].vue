<template>
	<div class="renderer-editor-container">
		<SvgRenderer
			v-if="mockupSVG"
			:palette-colors="staticPalette"
			:svg="mockupSVG"
		/>
		<PaletteEditor />
	</div>
</template>

<script setup lang="ts">
import SvgRenderer from '~/components/SvgRenderer.vue';
import PaletteEditor from '~/components/palette-editor/PaletteEditor.vue';
import {usePaletteEditorColorRolesStore} from "~/stores/paletteEditorColorRolesStore";
import type {PaletteColor} from "~/types/PaletteColor";
import MockupService from "~/services/mockup.service";
import {ColorType} from "~/types/ColorType";
import {watchDebounced} from "@vueuse/core";
import {useAuth0} from "@auth0/auth0-vue";

const rolesStore = usePaletteEditorColorRolesStore();

const authClient = useAuth0()
const {
	$apiFetch
} = useNuxtApp();

// ref to store the mockup SVG
const mockupSVG = ref<string>();

// fetch mockup SVG from new API
watchEffect(async () => {
	// use MockupService
	const mockupService = new MockupService($apiFetch.value, await authClient?.getAccessTokenSilently());
	mockupService.fetchMockup('Chat_App.svg').then((mockup) => {
		mockupSVG.value = mockup;
	});
});

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
