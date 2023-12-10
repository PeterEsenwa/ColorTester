<template>
	<div
		ref="svgContainer"
		class="svg-renderer"
	/>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import type { ColorRoles } from '~/types/ColorRole';
import type { PaletteColors } from '~/types/PaletteColor';

// Props
const props = defineProps<{
	svgPath: string;
	paletteColors: PaletteColors;
}>();

const emit = defineEmits<{
	(event: 'svg-load-error', error: Error): void;
}>();

// Template ref
const svgContainer = ref<HTMLElement | null>(null);

// Methods
const loadAndRenderSVG = async () => {
	try {
		const response = await fetch(props.svgPath);
		if (!response.ok) {
			return emit('svg-load-error', new Error(`Failed to load SVG from ${ props.svgPath }`));
		}

		const svgText = await response.text();

		if (svgContainer.value) {
			svgContainer.value.innerHTML = applyColorSettings(svgText, props.paletteColors);
		}
	} catch (error) {
		console.error(error);
	}
};

const applyColorSettings = (svgText: string, paletteColors?: PaletteColors): string => {
	const parser = new DOMParser();
	const svgDoc = parser.parseFromString(svgText, 'image/svg+xml');
	const svgElement = svgDoc.documentElement;

	if (!paletteColors) {
		return svgElement.outerHTML;
	}

	// Apply color settings to SVG elements
	for (const colorRoleKey in paletteColors) {


		const colorRole = paletteColors[colorRoleKey as keyof ColorRoles];
		const color = colorRole?.value;

		if (!color) {
			continue;
		}

		const svgElements = svgElement.querySelectorAll(`[data-color-role="${ colorRoleKey.toLowerCase() }"]`);

		for (const svgElement of svgElements) {
			if (color.value && typeof color.value !== 'object') {
				svgElement.setAttribute('fill', color.value);
			}
		}
	}

	// add styles to SVG element
	svgElement.setAttribute('style', 'width: auto; height: 100%;');
	svgElement.classList.add('rounded-lg');

	return svgElement.outerHTML;
};


// Lifecycle hooks
onMounted(() => {
	loadAndRenderSVG();
});
</script>

<style scoped>
.svg-renderer {
	@apply flex items-center justify-center;
	@apply h-full;
}

/* Styles for interactive highlighting */
.svg-renderer [data-interactive]:hover,
.svg-renderer [data-interactive]:focus {
	stroke: blue; /* Example highlight styling */
	stroke-width: 2;
}
</style>
