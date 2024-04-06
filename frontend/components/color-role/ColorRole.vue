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
			{{ roleModel }}
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
import { ref, type WritableComputedRef } from 'vue';
import type { PaletteColor, ColorValue } from '~/types/PaletteColor';
import ColorView from '~/components/color-view/ColorView.vue';
import { deleteColorRoleFnKey } from '~/utils/injectionKeys';
import { useVModel } from '@vueuse/core';
import { type ColorRole } from '~/types/ColorRole';
import { TooltipRoot, TooltipTrigger, TooltipContent, } from 'radix-vue';

const deleteFn = inject(deleteColorRoleFnKey, () => void 0)
const liteMode = inject(liteModeKey, ref(false))

// Wrapper is either a div or a Popover depending on lite mode
const wrapper = computed(() => liteMode?.value ? TooltipRoot : 'div')

// role is shown in a span or a PopoverPanel depending on lite mode
const panel = computed(() => liteMode?.value ? TooltipContent : 'span')

// view wrapper is either a div or a PopoverButton depending on lite mode
const viewWrapper = computed(() => liteMode?.value ? TooltipTrigger : 'div')

const props = defineProps<{
	color: ColorValue
	role: ColorRole
}>()

const colorRoleRef = ref<HTMLElement | typeof TooltipRoot | null>(null);
const colorWrapperRef = ref<HTMLElement | typeof TooltipTrigger | null>(null);

const emit = defineEmits<{
	(event: 'update:role', role: ColorRole): void
	(event: 'update:color', color: PaletteColor['value']): void
}>()

const colorModel = useVModel(props, 'color', emit) as WritableComputedRef<ColorValue>
const roleModel = useVModel(props, 'role', emit)

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
		} ]
})

// on mounted, apply the colorRoleClasses to the colorRoleRef
onMounted(() => {
	const colorRoleRefValue = colorRoleRef.value;
	if (!liteMode.value && colorRoleRefValue instanceof HTMLElement) {
		colorRoleRefValue.classList.add('relative flex flex-col items-start gap-2 w-full')
	}
})

const wrapperClasses = computed(() => {
	return [
		'color-role__wrapper',
		{
			'hover:outline outline-on-background hover:outline-4 !hover:rounded overflow-hidden hover:z-1': liteMode.value,
		}
	]
})
</script>

<style lang="scss">
</style>
