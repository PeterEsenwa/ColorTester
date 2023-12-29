<template>
	<div
		ref="colorRoleRef"
		class="color-role"
	>
		<span class="color-role__role">
			{{ roleModel }}
		</span>
		<ColorView
			:key="colorModel"
			ref="colorViewRef"
			v-model="colorModel"
			@delete="handleDelete()"
		/>
	</div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import type { PaletteColor } from '~/types/PaletteColor';
import ColorView from '~/components/color-view/ColorView.vue';
import { deleteColorRoleFnKey } from '~/utils/injectionKeys';
import { useVModel } from '@vueuse/core';
import { type ColorRole, MainColorRole } from '~/types/ColorRole';
import type { SortableOptions, SortableEvent } from 'sortablejs';
import { initSortable } from '~/utils/initSortable';
import { isDef } from '@vueuse/shared';

const deleteFn = inject(deleteColorRoleFnKey)

const props = defineProps<{
	color: PaletteColor['value']
	role: ColorRole
}>()

const rolesStore = usePaletteEditorColorRolesStore();

const sortOptions = {
	group: 'color-views',
	handle: '.color-view__drag-handle',
	swapThreshold: 0.5,
	swapClass: 'color-view--swap',
	swap: true,
	animation: 200,
	onMove: (event: SortableEvent) => {
		nextTick(() => {
			const oldIndex = event.oldIndex - 1;
			const newIndex = event.newIndex - 1;
			const from = event.from;
			const to = event.to;

			console.log(event);

			/*if (!isDef(oldIndex) || !isDef(newIndex)) return;

			console.log(event);
			console.log('oldIndex', oldIndex);
			console.log('newIndex', newIndex);
			console.log('role', props.role);

			// moving within the same list
			if (from !== to) {
				console.log('moving within the same list');
				if (props.role in MainColorRole) {
					// swap the color values (not using slice or splice)
					const firstValue = JSON.parse(JSON.stringify(rolesStore.regularColors[oldIndex].value));
					const secondValue = JSON.parse(JSON.stringify(rolesStore.onColors[newIndex].value));

					console.log('firstValue', firstValue);
					console.log('secondValue', secondValue);

					if (!isDef(firstValue) || !isDef(secondValue)) return;

					rolesStore.regularColors[oldIndex].value = JSON.parse(JSON.stringify(secondValue));
					rolesStore.onColors[newIndex].value = JSON.parse(JSON.stringify(firstValue));

					console.log('rolesStore.onColors[newIndex].value', rolesStore.onColors[newIndex].value);
					console.log('rolesStore.regularColors[oldIndex].value', rolesStore.regularColors[oldIndex].value);
				}
			} else {

			}*/
		})

		return false

		/*if (props.role in OnColorRole) {
			const movedItem = rolesStore.onColors.splice(oldIndex, 1)[0];
			rolesStore.onColors.splice(newIndex, 0, movedItem);
		} else {
			const movedItem = rolesStore.regularColors.splice(oldIndex, 1)[0];
			rolesStore.regularColors.splice(newIndex, 0, movedItem);
		}*/
	},
} as SortableOptions;

const colorRoleRef = ref<HTMLElement | null>(null);

watch(colorRoleRef, (element) => {
	element && initSortable(element, sortOptions)
}, {
	immediate: true,
});

const emit = defineEmits<{
	(event: 'update:role', role: ColorRole): void
	(event: 'update:color', color: PaletteColor['value']): void
}>()

const colorModel = useVModel(props, 'color', emit)
const roleModel = useVModel(props, 'role', emit)

const colorViewRef = ref<InstanceType<typeof ColorView> | null>(null);

const handleDelete = () => {
	deleteFn?.({
		role: roleModel.value,
		value: colorModel.value
	})
}
</script>

<style scoped lang="scss">
.color-role {
	@apply flex flex-col items-start gap-2 w-max;
	@apply relative;

	&__role {
		@apply text-body2 font-bold;

		font-variant-caps: all-small-caps;
	}
}
</style>
