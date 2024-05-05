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
import {AllColorRoles, type ColorRole, type ColorRoles, MainColorRole} from '~/types/ColorRole';
import type {PaletteColor} from '~/types/PaletteColor';
import { isDefined, watchDebounced } from '@vueuse/core';
import { ColorIntensity } from '~/types/ColorIntensity';
import camelcase from "camelcase";

// Props
const props = withDefaults(defineProps<{
	svgPath: string;
	paletteColors?: PaletteColor[];
	rounded?: boolean;
}>(), {
	rounded: true,
});

const isDisabled = ref(!isDefined(props.paletteColors));

const cancelWatch = watchDebounced(() => props.paletteColors, () => {
	isDisabled.value = !isDefined(props.paletteColors);

	cancelWatch();
}, {
	deep: true,
	debounce: 150,
	once: true,
});

const emit = defineEmits<{
	(event: 'svg-load-error', error: Error): void;
}>();

// Template ref
const svgContainer = ref<HTMLElement | null>(null);

const isLoaded = ref(false);

// Methods
const loadAndRenderSVG = async (
	svgPath: string,
) => {
	isLoaded.value = false;
	try {
		const response = await fetch(svgPath);
		if (!response.ok) {
			return emit('svg-load-error', new Error(`Failed to load SVG from ${ svgPath }`));
		}

		const svgText = await response.text();

		if (svgContainer.value) {
			svgContainer.value.appendChild(new DOMParser().parseFromString(svgText, 'image/svg+xml').documentElement);
		}
	} catch (error) {
		console.error(error);
	} finally {
		isLoaded.value = true;
	}
};

const getPrimaryColor = (paletteColors?: PaletteColor[]): string | undefined => {
	if (!paletteColors || !paletteColors.length) {
		return undefined;
	}

	const primaryColor = paletteColors.find((color) => color.role === MainColorRole.primary as ColorRole);

	if (!primaryColor || !primaryColor.value) {
		return undefined;
	}

	const colorValue = primaryColor.value.value;

	if (typeof colorValue === 'object') {
		return colorValue[ColorIntensity.Intensity900];
	}

	return colorValue;
};

const primaryColor = computed(() => getPrimaryColor(props.paletteColors) ?? 'currentColor');

const applyColorSettings = (paletteColors?: PaletteColor[]) => {
	const svgElement = svgContainer.value?.querySelector('svg');

	if (!paletteColors || !svgElement) {
		return;
	}

	// Apply color settings to SVG elements
	for (const color of paletteColors) {
		const colorRoleKey = color.role;

		if (!color) {
			continue;
		}

		const camelcaseRole = camelcase(colorRoleKey);
		console.log('camelcaseRole', camelcaseRole);

		const svgElements = svgElement.querySelectorAll(`[data-color-role="${ camelcaseRole }"]`);

		for (const svgElement of svgElements) {
			if (color.value?.value && typeof color.value.value !== 'object') {
				svgElement.setAttribute('fill', color.value.value);
			}
		}
	}

	// add styles to SVG element
	svgElement.setAttribute('style', 'width: auto; height: 100%;');
};

// Lifecycle hooks
onMounted(() => {
	watchEffect(() => {
		loadAndRenderSVG(props.svgPath);
	});

	watch(() => [props.paletteColors, isLoaded], () => {
		if (isLoaded.value) {
			applyColorSettings(props.paletteColors);
		}
	}, {
		deep: true,
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
