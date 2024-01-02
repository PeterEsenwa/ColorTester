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
import { type ColorRole } from '~/types/ColorRole';

const deleteFn = inject(deleteColorRoleFnKey)

const props = defineProps<{
	color: PaletteColor['value']
	role: ColorRole
}>()

const rolesStore = usePaletteEditorColorRolesStore();

const colorRoleRef = ref<HTMLElement | null>(null);

const emit = defineEmits<{
	(event: 'update:role', role: ColorRole): void
	(event: 'update:color', color: PaletteColor['value']): void
}>()

const colorModel = useVModel(props, 'color', emit)
const roleModel = useVModel(props, 'role', emit)

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
