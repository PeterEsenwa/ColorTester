<template>
	<TooltipProvider>
		<component
			:delay-duration="0"
			:is="wrapper"
			:class="colorRoleClasses"
			ref="colorRoleRef"
		>
			<component
				side="bottom"
				:is="panel"
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
				as-child
				:is="viewWrapper">
				<ColorView
					v-model="colorModel"
					@delete="handleDelete()"
				/>
			</component>
		</component>
	</TooltipProvider>
</template>

<script setup lang="ts">
import { ref, type WritableComputedRef } from 'vue';
import type { PaletteColor, ColorValue } from '~/types/PaletteColor';
import ColorView from '~/components/color-view/ColorView.vue';
import { deleteColorRoleFnKey } from '~/utils/injectionKeys';
import { useVModel } from '@vueuse/core';
import { type ColorRole } from '~/types/ColorRole';
import { TooltipProvider, TooltipRoot, TooltipTrigger, TooltipContent, } from 'radix-vue';

const deleteFn = inject(deleteColorRoleFnKey)
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

const colorRoleRef = ref<HTMLElement | null>(null);

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

const colorRoleClasses = computed(() => [
	{
		'relative flex flex-col items-start gap-2 w-max': !liteMode.value,
	}
])

const roleTextClasses = computed(() => {
	return [
		'text-body2 text-on-surface',
		{
			'font-medium': !liteMode.value,
			'text-caption': liteMode.value,
		} ]
})
</script>

<style scoped lang="scss">
</style>
