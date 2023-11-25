<template>
	<div class="svg-renderer" ref="svgContainer"></div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import type { ColorSettings, ThemeColors, ColorVariants, LightDarkColorSettings } from '@/types/ColorSettings';

// Props
const props = defineProps<{
	svgPath: string;
	colorSettings: ColorSettings;
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
			svgContainer.value.innerHTML = applyColorSettings(svgText, props.colorSettings);
		}
	} catch (error) {
		console.error(error);
	}
};

const applyColorSettings = (svgText: string, colorSettings: ColorSettings): string => {
	const parser = new DOMParser();
	const svgDoc = parser.parseFromString(svgText, 'image/svg+xml');
	const svgElement = svgDoc.documentElement;
	const colorRoles: ThemeColors | undefined = colorSettings.colors;

	if (!colorRoles) {
		return svgElement.outerHTML;
	}

	// Apply color settings to SVG elements
	for (const colorRoleKey in colorRoles) {
		if (colorRoles.hasOwnProperty(colorRoleKey)) {
			const colorRole = colorRoles[colorRoleKey as keyof typeof colorRoles] as ColorVariants;
			// Only use color string
			if (typeof colorRole === 'string') {
				const svgElements = svgElement.querySelectorAll(`[data-color-role="${ colorRoleKey }"]`);
				svgElements.forEach((svgElement) => {
					svgElement.setAttribute('fill', colorRole);
				});
			}
		}
	}

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
}

/* Styles for interactive highlighting */
.svg-renderer [data-interactive]:hover,
.svg-renderer [data-interactive]:focus {
	stroke: blue; /* Example highlight styling */
	stroke-width: 2;
}
</style>
