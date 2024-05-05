<template>
	<article
		ref="colorViewRef"
		class="color-view"
		tabindex="0"
		:class="{
			'no-color': !color?.value,
			'color-view--lite-mode': liteMode,
		}"
	>
		<button
			v-if="!liteMode"
			ref="dragHandleRef"
			class="color-view__action color-view__drag-handle"
		>
			<span class="icon-ic:round-more-vert color-view__icon" />
		</button>
		<div
			v-if="!liteMode"
			class="color-view__actions"
		>
			<button
				class="color-view__action color-view__edit"
				@click="openColorPicker"
			>
				<span class="icon-ic:round-colorize color-view__icon" />
			</button>
			<button
				class="color-view__action color-view__delete"
				@click="$emit('delete')"
			>
				<span class="icon-ic:round-delete color-view__icon" />
			</button>
		</div>
		<!-- hidden color picker input -->
		<input
			ref="colorPickerRef"
			type="color"
			class="color-view__color-picker sr-only"
			:value="color?.value"
			@input="changeColor"
		>
	</article>
</template>

<script setup lang="ts">
import {type ColorDictionaryValue, type ColorValue, type HEX, isColorDictionaryValue} from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';
import {useDark, useDebounceFn} from '@vueuse/core';
import { ref } from 'vue';
import {ColorIntensity} from "~/types/ColorIntensity";

defineOptions({
	name: 'ColorView',
});

defineEmits<{
	(event: 'delete'): void
}>()

const liteMode = inject(liteModeKey, ref(false))

const colorViewRef = ref<HTMLDivElement | null>(null);

const dragHandleRef = ref<HTMLButtonElement | null>(null)

const colorPickerRef = ref<HTMLInputElement>();

const color = defineModel<ColorValue | ColorDictionaryValue | undefined>()

const displayColor = computed(() => {
	if (!color.value) return 'transparent';

	return color.value?.value;
})

const luminosity = computed(() => {
	if (!color.value || !color.value.value) return 0;

	if (isColorDictionaryValue(color.value)) {
		return getHEXLuminosity(color.value.value[ColorIntensity.Intensity900]);
	}

	return getHEXLuminosity(color.value.value);
})

const iconColor = computed(() => {
	const isDark = useDark()

	if (!color.value) {
		return 'var(--un-preset-theme-colors-on-surface)';
	}

	if (luminosity.value <= 128) {
		return isDark.value ? 'var(--un-preset-theme-colors-on-surface)' : 'var(--un-preset-theme-colors-surface)';
	}

	return isDark.value ? 'var(--un-preset-theme-colors-surface)' : 'var(--un-preset-theme-colors-on-surface)';
});

const openColorPicker = () => {
	colorPickerRef.value?.click();
}

const changeColor = useDebounceFn((event: Event) => {
	// ignore if in lite mode
	if (liteMode.value) return;

	const target = event.target as HTMLInputElement;

	const valueAsHEX = target.value as HEX;

	color.value = {
		value: valueAsHEX,
		type: ColorType.Color
	}
})

defineExpose({
	openColorPicker,
})
</script>

<style scoped lang="scss">
.color-view {
	--color-view-display-color: v-bind(displayColor);

	@apply w-full h-[--palette-view-color-size] aspect-[3/1.2];
	@apply rounded-[0.15em] cursor-pointer;
	@apply inline-flex grid relative grid-rows-1 grid-flow-row;
	@apply transition-colors duration-300 ease-in-out;
	@apply border-2 border-on-surface;
	@apply bg-[--color-view-display-color];

	// lite mode
	&--lite-mode {
		@apply aspect-[3/1.5] border-0 rounded-none;

		&:first-of-type {
			@apply rounded-l-md;
		}

		&:last-of-type {
			@apply rounded-r-md;
		}
	}

	grid-template-columns: 1fr auto 1fr;

	&.no-color {
		@apply border-style-dashed border-on-surface/50;
		@apply hover:border-on-surface/100;
	}

	&__actions {
		@apply flex items-center justify-center gap-2 px-2;
		@apply w-full h-full;
		@apply transition-all duration-300 ease-in-out;

		// hide by default and do not allow click events
		@apply opacity-0 pointer-events-none;

		@media (hover: hover) and (pointer: fine) {
			// show on hover
			.color-view:hover & {
				@apply opacity-100 pointer-events-auto;
			}
		}

		@media (hover: none) and (pointer: coarse) {
			// show on focus
			.color-view:focus-within & {
				@apply opacity-100 pointer-events-auto;
			}
		}
	}

	&__action {
		--action-color: rgb(v-bind(iconColor));

		@apply bg-transparent;
		@apply flex items-center justify-center;
		@apply text-[--action-color];
	}

	&__icon {
		@apply inline-block w-1.25em h-1.25em;
	}

	&__color-picker {
		@apply w-full h-full;
	}
}
</style>
