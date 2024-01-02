<template>
	<div
		ref="colorViewRef"
		class="color-view"
		tabindex="0"
		:class="{ 'no-color': !color?.value }"
	>
		<button
			ref="dragHandleRef"
			class="color-view__action color-view__drag-handle"
		>
			<span class="icon-ic:round-more-vert color-view__icon" />
		</button>
		<div
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
	</div>
</template>

<script setup lang="ts">
import type { ColorValue, HEX } from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';
import { useDark, useDraggable } from '@vueuse/core';
import { ref } from 'vue';

defineOptions({
	name: 'ColorView',
});

defineEmits<{
	(event: 'delete'): void
}>()

const colorViewRef = ref<HTMLDivElement | null>(null);

/*const {
	x,
	y,
	style,
} = useDraggable(colorViewRef, {
	initialValue: {
		x: 0,
		y: 0,
	},
})*/

const dragHandleRef = ref<HTMLButtonElement | null>(null)

const colorPickerRef = ref<HTMLInputElement>();

const color = defineModel<ColorValue | undefined>()

const displayColor = computed(() => {
	if (!color.value) return 'transparent';

	return color.value?.value;
})

const luminosity = computed(() => {
	if (!color.value) return 0;

	const value = color.value?.value;

	if (!value) return 0;

	return getHEXLuminosity(value);
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

const changeColor = (event: Event) => {
	const target = event.target as HTMLInputElement;

	const valueAsHEX = target.value as HEX;

	if (!color.value) {
		color.value = {
			value: valueAsHEX,
			type: ColorType.Color
		}
		return;
	}

	color.value.value = valueAsHEX;
}

defineExpose({
	openColorPicker,
})
</script>

<style scoped lang="scss">
.color-view {
	--color-view-display-color: v-bind(displayColor);

	@apply w-max aspect-[3/1.2];
	@apply rounded-[0.15em];
	@apply grid relative grid-rows-1 grid-flow-row;
	@apply transition-colors duration-300 ease-in-out;
	@apply border-2 border-[--color-view-display-color];
	@apply bg-[--color-view-display-color];

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
