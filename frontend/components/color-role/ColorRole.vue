<template>
	<component
		:is="wrapper"
		ref="colorRoleRef"
		:delay-duration="0"
	>
		<component
			:is="panel"
			side="bottom"
			class="color-role__role"
			:class="roleTextClasses"
			:side-offset="4"
			style="{
					font-variant-caps: all-small-caps;
				}"
		>
			{{ getPanelText() }}
		</component>
		<component
			:is="viewWrapper"
			as-child
			:class="wrapperClasses"
		>
			<ColorView
				v-model="colorModel"
				@delete="handleDelete()"
			/>
		</component>
	</component>
</template>

<script setup lang="ts">
import {ref, type WritableComputedRef} from 'vue';
import type {ColorDictionaryValue, ColorValue, PaletteColor} from '~/types/PaletteColor';
import ColorView from '~/components/color-view/ColorView.vue';
import {deleteColorRoleFnKey} from '~/utils/injectionKeys';
import {useVModel} from '@vueuse/core';
import {type ColorRole} from '~/types/ColorRole';
import {TooltipContent, TooltipRoot, TooltipTrigger,} from 'radix-vue';
import {ColorType} from "~/types/ColorType";
import {ColorIntensity} from "~/types/ColorIntensity";

const deleteFn = inject(deleteColorRoleFnKey, () => void 0)
const liteMode = inject(liteModeKey, ref(false))

// Wrapper is either a div or a Popover depending on lite mode
const wrapper = computed(() => liteMode?.value ? TooltipRoot : 'div')

// role is shown in a span or a PopoverPanel depending on lite mode
const panel = computed(() => liteMode?.value ? TooltipContent : 'span')

// view wrapper is either a div or a PopoverButton depending on lite mode
const viewWrapper = computed(() => liteMode?.value ? TooltipTrigger : 'div')

const props = withDefaults(defineProps<{
	color: ColorValue | ColorDictionaryValue | undefined
	role: ColorRole
}>(), {
	color: () => ({
		type: ColorType.Color,
		value: '#000000',
	}),
})

const colorRoleRef = ref<HTMLElement | typeof TooltipRoot | null>(null);
const colorWrapperRef = ref<HTMLElement | typeof TooltipTrigger | null>(null);

const emit = defineEmits<{
	(event: 'update:role', role: ColorRole): void
	(event: 'update:color', color: PaletteColor['value']): void
}>()

const colorModel = useVModel(props, 'color', emit) as WritableComputedRef<ColorValue | ColorDictionaryValue | undefined>
const roleModel = useVModel(props, 'role', emit)

const getPanelText = () => {
	if (!liteMode.value) {
		return roleModel.value
	}

	if (colorModel.value?.type === ColorType.Color) {
		return colorModel.value?.value.toUpperCase()
	}

	return colorModel.value?.value[ColorIntensity.Intensity900]
}

const handleDelete = () => {
	deleteFn?.({
		role: roleModel.value,
		value: colorModel.value
	})
}

const roleTextClasses = computed(() => {
	return [
		'text-body2 text-on-surface',
		{
			'font-medium': !liteMode.value,
			'text-caption': liteMode.value,
		}]
})

// on mounted, apply the colorRoleClasses to the colorRoleRef
onMounted(() => {
	const colorRoleRefValue = colorRoleRef.value;
	if (!liteMode.value && colorRoleRefValue instanceof HTMLElement) {
		// error when adding classes at once
		colorRoleRefValue.classList.add('relative', 'flex', 'flex-col', 'items-start', 'gap-2', 'w-full')
	}
})

const wrapperClasses = computed(() => {
	return [
		'color-role__wrapper',
		// {
		// 	'hover:outline outline-on-background hover:outline-4 !hover:rounded overflow-hidden hover:z-1': liteMode.value,
		// }
	]
})
</script>

<style lang="scss">
</style>
