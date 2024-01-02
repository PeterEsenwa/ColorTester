<template>
	<div
		ref="paletteEditorRef"
		class="palette-editor"
	>
		<div class="columns">
			<div
				ref="regularColorColumnRef"
				class="color-column regular-color-column"
			>
				<ColorRole
					v-for="color in rolesStore.regularColors"
					:key="color.role"
					v-model:color="color.value"
					v-model:role="color.role"
				/>
			</div>
			<div
				ref="onColorColumnRef"
				class="color-column on-color-column"
			>
				<ColorRole
					v-for="color in rolesStore.onColors"
					:key="color.role"
					v-model:color="color.value"
					v-model:role="color.role"
				/>
			</div>
		</div>
		<button
			class="add-button"
			@click="toggle(true)"
		>
			<span>+</span>
		</button>
	</div>
	<SelectColorRoleDialog
		v-model="isOpen"
		:current-colors="rolesStore.colors"
		@roles-selected="addColor"
	/>
</template>

<script setup lang="ts">
import ColorRole from '~/components/color-role/ColorRole.vue';
import { useToggle } from '@vueuse/core';
import { type ColorRole as ColorRoleType, OnColorRole } from '~/types/ColorRole';
import { usePaletteEditorColorRolesStore } from '~/stores/paletteEditorColorRolesStore';
import { deleteColorRoleFnKey } from '~/utils/injectionKeys';
import type { PaletteColor } from '~/types/PaletteColor';
import dragula from 'dragula';

const rolesStore = usePaletteEditorColorRolesStore();

const paletteEditorRef = ref<HTMLDivElement | null>(null);
const regularColorColumnRef = ref<Element | null>(null);
const onColorColumnRef = ref<Element | null>(null);

const [ isOpen, toggle ] = useToggle(false);

const drake = dragula({
	copy: true,
	accepts: () => {
		return false;
	},
	isContainer(el) {
		return el?.classList.contains('color-role');
	},
	invalid(el, handle) {
		return !handle?.parentElement?.classList.contains('color-view__drag-handle');
	}
});

const addColor = (selectedRoles: ColorRoleType[]) => {
	if (!selectedRoles || !selectedRoles.length) return;

	const existingRoles = new Set(rolesStore.colors.map(color => color.role));
	console.log(existingRoles);

	for (const role of selectedRoles) {
		if (existingRoles.has(role)) {
			console.log('role already exists', role);
			continue;
		}

		// add color role to the array
		if (role in OnColorRole) {
			rolesStore.onColors.push({
				role,
				value: undefined,
			});
		} else {
			rolesStore.regularColors.push({
				role,
				value: undefined,
			});
		}
	}

	toggle(false);
};

provide(deleteColorRoleFnKey, (paletteColor: PaletteColor) => {
	// delete color role from the array
	if (paletteColor.role in OnColorRole) {
		rolesStore.onColors = rolesStore.onColors.filter((color) => {
			return color.role !== paletteColor.role;
		});
	} else {
		rolesStore.regularColors = rolesStore.regularColors.filter((color) => {
			return color.role !== paletteColor.role;
		});
	}
})
</script>

<style scoped lang="scss">
//noinspection CssUnknownAtRule
.palette-editor {
	@apply bg-surface text-on-surface;
	@apply rounded-lg shadow-lg;
	@apply flex flex-col gap-4 p-4;
	@apply w-full;
	@apply min-w-fit overscroll-auto;
	@apply @container;

	.columns {
		@apply flex gap-4 flex-col xs:flex-row xs:justify-between;

		[class*="-column"] {
			@apply flex flex-col gap-4;
		}
	}

	.add-button {
		@apply bg-blue-500 text-white;
		@apply rounded-full h-8 w-8;
		@apply flex items-center justify-center;
		@apply hover:bg-blue-600 active:bg-blue-700;
		@apply transition-colors duration-200;
	}
}
</style>
