<template>
	<div
		ref="svgContainer"
		class="svg-renderer"
		:class="{
			'disabled': isDisabled,
			'rounded': props.rounded,
		}"
	/>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import type { ColorRoles } from '~/types/ColorRole';
import type { PaletteColors } from '~/types/PaletteColor';
import { isDefined, watchDebounced } from '@vueuse/core';
import { ColorIntensity } from '~/types/ColorIntensity';

// Props
const props = withDefaults(defineProps<{
	svgPath: string;
	paletteColors?: PaletteColors;
	rounded?: boolean;
}>(), {
	paletteColors: undefined,
	rounded: true,
});

const isDisabled = ref(!isDefined(props.paletteColors));

const cancelWatch = watchDebounced(() => props.paletteColors, () => {
	isDisabled.value = !isDefined(props.paletteColors);

	cancelWatch();
}, {
	deep: true,
	debounce: 150,
});

const emit = defineEmits<{
	(event: 'svg-load-error', error: Error): void;
}>();

// Template ref
const svgContainer = ref<HTMLElement | null>(null);

const isLoaded = ref(false);

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
	} finally {
		isLoaded.value = true;
	}
};

const getPrimaryColor = (paletteColors?: PaletteColors): string | undefined => {
	if (!paletteColors) {
		return undefined;
	}

	const primaryColor = paletteColors.primary?.value;

	if (!primaryColor) {
		return undefined;
	}

	if (typeof primaryColor.value === 'object') {
		return primaryColor.value[ColorIntensity.Intensity900];
	}

	return primaryColor.value;
};

const primaryColor = computed(() => getPrimaryColor(props.paletteColors) ?? 'currentColor');

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

	return svgElement.outerHTML;
};

// Lifecycle hooks
onMounted(() => {
	watch(() => [ props.svgPath, props.paletteColors ], () => {
		loadAndRenderSVG();
	}, {
		immediate: true,
	});
});
</script>

<style scoped lang="scss">
.svg-renderer {
	--un-default-border-color: v-bind(primaryColor);

	@apply flex;
	@apply h-full relative;
	@apply text-base;

	&::after {
		@apply block absolute inset-0 pointer-events-none;
		@apply bg-background opacity-0 rounded-md;
		@apply content-empty transition-opacity;
	}

	&.disabled {
		@apply filter-grayscale filter-blur-1;

		&::after {
			@apply opacity-20;
		}
	}

	&.rounded ::v-deep(svg) {
		@apply rounded-md;
	}

	::v-deep(svg) {
		[data-color-role='background'] {
			@apply stroke-[--un-default-border-color] stroke-2;
		}
	}
}

/* Styles for interactive highlighting */
.svg-renderer [data-interactive]:hover,
.svg-renderer [data-interactive]:focus {
	stroke: blue; /* Example highlight styling */
	stroke-width: 2;
}
</style>
