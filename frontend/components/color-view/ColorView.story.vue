<template>
	<Story name="ColorView">
		<draggable
			ref="colorViewRef"
			v-model="colors"
			class="flex flex-col gap-4 relative"
			group="colors"
			swap-threshold="0.25"
			item-key="role"
			:wap="true"
			handle=".color-view__drag-handle"
		>
			<template #item="{element}">
				<ColorView
					v-model="element.value"
				/>
			</template>
		</draggable>
	</Story>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import type { PaletteColor } from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';

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

// const colorViewRef = ref<HTMLDivElement | null>(null);

// const animation = 200;

// watch(colorViewRef, (element) => {
// 	if (element) {
// 		console.log(element);
// 		// @ts-expect-error - Vueuse types issues
// 		const sortable = useSortable(element, colors, {
// 			handle: '.color-view__drag-handle',
// 			swapThreshold: 0.25,
// 			swapClass: 'color-view--swap',
// 			swap: true,
// 			drag: true,
// 			sort: false,
// 			animation,
// 		});
//
// 		sortable.start();
//
// 		console.log(sortable);
//
// 		sortable.option('animation', animation);
// 	}
// }, {
// 	immediate: true,
// });
</script>

<style scoped lang="scss">

</style>
