<template>
	<div
		ref="colorViewRef"
		class="color-view"
		tabindex="0"
		:style="{ backgroundColor: color.value }"
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
			class="color-view__color-picker hidden"
			:value="color.value"
			@input="changeColor"
		>
	</div>
</template>

<script setup lang="ts">
import type { PropType } from 'vue';
import type { ColorValue, HEX } from '~/types/PaletteColor';

defineOptions({
	name: 'ColorView',
});

defineEmits<{
	(event: 'delete'): void
}>()

const colorViewRef = ref<HTMLDivElement | null>(null);

const dragHandleRef = ref<HTMLButtonElement | null>(null)

const colorPickerRef = ref<HTMLInputElement>();

const color = defineModel({
	type: Object as PropType<ColorValue>,
	required: true,
})

const openColorPicker = () => {
	colorPickerRef.value?.click();
}

const changeColor = (event: Event) => {
	const target = event.target as HTMLInputElement;
	color.value.value = target.value as HEX;
}
</script>

<style scoped lang="scss">
.color-view {
	@apply w-max aspect-[3/1.2];
	@apply rounded-[0.15em];
	@apply grid relative grid-rows-1 grid-flow-row;

	grid-template-columns: 1fr auto 1fr;

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
		@apply bg-transparent;
		@apply flex items-center justify-center;
		@apply text-on-surface;
	}

	&__drag-handle {
		//@apply absolute left-0;
	}

	&__icon {
		@apply inline-block w-1.25em h-1.25em;
	}

	&__color-picker {
		@apply hidden;
	}
}
</style>
