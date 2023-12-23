<template>
	<Story name="ColorView">
		<div
			ref="colorViewRef"
			class="flex flex-col gap-4 relative"
		>
			<ColorView
				v-for="color in colors"
				:key="color.role"
				v-model="color.value"
			/>
		</div>
	</Story>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import type { PaletteColor } from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';
import Sortable, { Swap } from 'sortablejs';

Sortable.mount(new Swap());

const colors = ref<PaletteColor[]>([
	{
		role: 'accent',
		value: {
			value: '#FC5130',
			type: ColorType.Color,
		},
	},
	{
		role: 'background',
		value: {
			value: '#FDFDFD',
			type: ColorType.Color,
		},
	},
	{
		role: 'error',
		value: {
			value: '#D7263D',
			type: ColorType.Color,
		},
	},
	{
		role: 'primary',
		value: {
			value: '#2F3061',
			type: ColorType.Color,
		},
	},
	{
		role: 'secondary',
		value: {
			value: '#A7A6CB',
			type: ColorType.Color,
		},
	},
	{
		role: 'onAccent',
		value: {
			value: '#FFFFFF',
			type: ColorType.Color,
		},
	},
	{
		role: 'onBackground',
		value: {
			value: '#000000',
			type: ColorType.Color,
		},
	},
	{
		role: 'onError',
		value: {
			value: '#FFFFFF',
			type: ColorType.Color,
		},
	},
	{
		role: 'onPrimary',
		value: {
			value: '#FFFFFF',
			type: ColorType.Color,
		},
	},
	{
		role: 'onSecondary',
		value: {
			value: '#000000',
			type: ColorType.Color,
		},
	},
]);

const colorViewRef = ref<HTMLDivElement | null>(null);

watch(colorViewRef, (element) => {
	if (element) {
/*		const sortableInstance = new Sortable(element, {
			handle: '.color-view__drag-handle',
			swapThreshold: 0.5,
			swapClass: 'color-view--swap',
			swap: true,
			animation: 200,
		});*/
	}
}, {
	immediate: true,
});
</script>

<style scoped lang="scss">

</style>
