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
	<HeadlessDialog
		:open="isOpen"
		as="div"
		class="color-role-dialog flex items-center justify-center fixed z-10 inset-0 p-6"
		aria-labelledby="modal-title"
		:initial-focus="okButtonRef"
	>
		<div>
			<HeadlessDialogOverlay class="color-role-dialog__overlay" />
			<div
				class="inline-block align-bottom  bg-background text-on-background rounded-lg text-left shadow-xl transform transition-all sm:align-middle sm:max-w-lg sm:w-full"
			>
				<HeadlessDialogTitle class="color-role-dialog__title">
					<h2 class="text-xl font-medium">
						Select a Role
					</h2>
				</HeadlessDialogTitle>
				<HeadlessDialogPanel class="color-role-dialog__panel">
					<HeadlessDialogDescription class="color-role-dialog__description">
						Select a role for the color you want to add.

						<HeadlessListbox v-model="selectedRole">
							<HeadlessListboxButton
								class="color-role-dialog__listbox-button"
							>
								{{ selectedRole }}
							</HeadlessListboxButton>
							<div class="color-role-dialog__options-wrapper">
								<HeadlessListboxOptions
									as="ul"
									class="color-role-dialog__options"
								>
									<HeadlessListboxOption
										v-for="role in AllColorRoles"
										:key="role"
										class="color-role-dialog__option"
										as="button"
										:value="role"
									>
										{{ role }}
									</HeadlessListboxOption>
								</HeadlessListboxOptions>
							</div>
						</HeadlessListbox>
					</HeadlessDialogDescription>
					<button
						ref="okButtonRef"
						class="okay-button"
						@click="addColorRole"
					>
						OK
					</button>
				</HeadlessDialogPanel>
			</div>
		</div>
	</HeadlessDialog>
</template>

<script setup lang="ts">
import { ref, watch, computed } from 'vue';
import Sortable, { Swap } from 'sortablejs';
import ColorRole from '~/components/ColorRole/ColorRole.vue';
import { useToggle } from '@vueuse/core';
import { type ColorRole as ColorRoleType, AllColorRoles, MainColorRole } from '~/types/ColorRole';

Sortable.mount(new Swap());

const roles = ref<ColorRoleType[]>([]);
const paletteEditorRef = ref<HTMLDivElement | null>(null);
const okButtonRef = ref<HTMLButtonElement | null>(null);

const regularRoles = computed(() => roles.value.filter(r => !r.startsWith('on')));
const onRoles = computed(() => roles.value.filter(r => r.startsWith('on')));

const [ isOpen, toggle ] = useToggle(false);

const selectedRole = ref<ColorRoleType>(MainColorRole.accent);

const addColorRole = () => {
	// colors.value.push({
	// 	role: selectedRole.value,
	// 	value: {
	// 		value: '#FFFFFF',
	// 		type: ColorType.Color,
	// 	},
	// });

	roles.value.push(selectedRole.value);

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
	@apply grid-rows-3;

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

.color-role-dialog {
	.color-role-dialog__overlay {
		@apply fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity;
	}

	.color-role-dialog__description {
		@apply inline-block align-bottom rounded-lg text-left transform transition-all sm:mb-8 sm:align-middle sm:max-w-lg sm:w-full;
		@apply flex flex-col gap-4;
	}

	.color-role-dialog__title {
		@apply bg-surface p-4 rounded-t-lg;
	}

	.color-role-dialog__panel {
		@apply p-4;
	}

	.color-role-dialog__listbox-button {
		@apply w-full bg-primary p-2 rounded text-on-primary;
	}

	.color-role-dialog__options-wrapper {
		@apply absolute top-100% left-0 right-0 bottom-0;
		@apply overflow-hidden h-fit;
		@apply rounded-b-lg;

		.color-role-dialog__options {
			@apply flex flex-col overflow-y-auto;
			@apply z-10 bg-surface max-h-32;

			.color-role-dialog__option {
				@apply block px-4 py-2 text-sm cursor-pointer select-none;
				@apply hover:bg-primary hover:text-on-primary bg-inherit text-inherit;
			}
		}
	}
}

.okay-button {
	@apply px-4 py-2;
	@apply bg-success/95 text-white;
	@apply rounded;
	@apply hover:bg-success;
}
</style>
