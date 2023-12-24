<template>
	<div
		ref="paletteEditorRef"
		class="palette-editor"
	>
		<div class="color-column">
			<ColorRole
				v-for="role in regularRoles"
				:key="role"
				:role="role"
			/>
		</div>
		<div class="on-color-column">
			<ColorRole
				v-for="role in onRoles"
				:key="role"
				:role="role"
			/>
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
		@roles-selected="addColorRole"
	/>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue';
import Sortable, { Swap } from 'sortablejs';
import ColorRole from '~/components/color-role/ColorRole.vue';
import { useToggle } from '@vueuse/core';
import { type ColorRole as ColorRoleType } from '~/types/ColorRole';

Sortable.mount(new Swap());

const roles = ref<Set<ColorRoleType>>(new Set());
const paletteEditorRef = ref<HTMLDivElement | null>(null);

const regularRoles = computed(() => Array.from(roles.value).filter(r => !r.startsWith('on')));
const onRoles = computed(() => Array.from(roles.value).filter(r => r.startsWith('on')));

const [ isOpen, toggle ] = useToggle(false);

const addColorRole = (selectedRoles: ColorRoleType[]) => {
	if (!selectedRoles || !selectedRoles.length) return;

	for (const role of selectedRoles) {
		roles.value.add(role);
	}

	toggle(false);
};

watch(paletteEditorRef, (element) => {
	if (element) {
		new Sortable(element, {
			handle: '.color-view__drag-handle',
			swapThreshold: 0.5,
			swapClass: 'color-view--swap',
			swap: true,
			animation: 200,
			onEnd: () => {
				// Update colors based on the new order
			},
		});
	}
}, {
	immediate: true,
});
</script>

<style scoped lang="scss">
.palette-editor {
	@apply bg-surface text-on-surface;
	@apply rounded-lg shadow-lg;
	@apply p-4;
	@apply grid gap-4;
	@apply grid-cols-2;

	grid-template-rows: 1fr auto;

	.color-column {
		@apply grid grid-flow-row;
		@apply col-span-1;
	}

	.on-color-column {
		@apply grid grid-flow-row;
		@apply col-span-1;
	}

	.add-button {
		@apply bg-blue-500 text-white;
		@apply rounded-full h-8 w-8;
		@apply flex items-center justify-center;
		@apply hover:bg-blue-600 active:bg-blue-700;
		@apply transition-colors duration-200;
		@apply row-start-3 col-span-2;
	}
}
</style>
