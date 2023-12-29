<template>
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

						<HeadlessListbox
							v-model="selectedRoles"
							multiple
						>
							<HeadlessListboxButton
								class="color-role-dialog__listbox-button"
							>
								{{ selectedRolesText }}
							</HeadlessListboxButton>
							<div class="color-role-dialog__options-wrapper">
								<HeadlessListboxOptions
									as="ul"
									class="color-role-dialog__options"
								>
									<HeadlessListboxOption
										v-for="role in AllColorRoles"
										v-slot="{ selected }"
										:key="role"
										class="color-role-dialog__option"
										as="button"
										:value="role"
									>
										<input
											type="checkbox"
											:checked="selected"
										>
										<span>{{ role }}</span>
									</HeadlessListboxOption>
								</HeadlessListboxOptions>
							</div>
						</HeadlessListbox>
					</HeadlessDialogDescription>
					<div class="color-role-dialog__actions">
						<button
							ref="okButtonRef"
							class="okay-button"
							@click="confirmSelectedRoles"
						>
							OK
						</button>
						<button
							class="cancel-button"
							@click="cancel"
						>
							Cancel
						</button>
					</div>
				</HeadlessDialogPanel>
			</div>
		</div>
	</HeadlessDialog>
</template>

<script setup lang="ts">
import { AllColorRoles, type ColorRole } from '~/types/ColorRole';
import type { PaletteColor } from '~/types/PaletteColor';

const props = defineProps<{
	currentColors: Array<PaletteColor>;
}>();

const selectedRoles = ref<ColorRole[]>([]);

const isOpen = defineModel<boolean>({
	default: () => false,
});

const okButtonRef = ref<HTMLButtonElement | null>(null);

const emit = defineEmits<{
	(e: 'rolesSelected', value: ColorRole[]): void;
}>();

const selectedRolesText = computed(() => {
	if (selectedRoles.value.length > 2) {
		return `Selected Roles: ${selectedRoles.value.length}`;
	} else {
		return selectedRoles.value.join(', ') || 'Select a role';
	}
});

watch(isOpen, (value) => {
	if (!value) {
		return;
	}
	selectedRoles.value = props.currentColors.map((color) => color.role);
}, {
	immediate: true,
});

const confirmSelectedRoles = () => {
	if (!selectedRoles.value) return;

	emit('rolesSelected', selectedRoles.value);

	isOpen.value = false;
};

const cancel = () => {
	// Restore the backup roles
	selectedRoles.value = props.currentColors.map((color) => color.role);
	isOpen.value = false;
};

</script>

<style scoped lang="scss">
.color-role-dialog {
	&__overlay {
		@apply fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity;
	}

	&__description {
		@apply inline-block align-bottom rounded-lg text-left transform transition-all sm:mb-8 sm:align-middle sm:max-w-lg sm:w-full;
		@apply flex flex-col gap-4;
		@apply font-ss3v;
	}

	.color-role-dialog__title {
		@apply bg-surface p-4 rounded-t-lg;
	}

	.color-role-dialog__panel {
		@apply p-4;
		@apply font-ss3v;

		.color-role-dialog__actions {
			@apply flex justify-end gap-2;

			> button {
				@apply flex-1;
				@apply px-4 py-2;
				@apply rounded;
				@apply font-inherit;
			}

			.okay-button {
				@apply bg-success/95 text-on-background;
				@apply hover:bg-success;
			}

			.cancel-button {
				@apply bg-error/95 text-on-error;
				@apply hover:bg-error;
			}
		}
	}

	.color-role-dialog__listbox-button {
		@apply w-full bg-primary p-2 rounded text-on-primary;
	}

	.color-role-dialog__options-wrapper {
		@apply absolute top-100% left-0 right-0 bottom-0;
		@apply overflow-hidden h-fit;
		@apply rounded-b-lg;
		@apply text-left;

		.color-role-dialog__options {
			@apply flex flex-col overflow-y-auto;
			@apply z-10 bg-surface max-h-32;

			.color-role-dialog__option {
				@apply grid grid-cols-[1em_1fr] items-center gap-2;
				@apply px-4 py-2 text-sm cursor-pointer select-none;
				@apply hover:bg-primary hover:text-on-primary bg-inherit text-inherit;
				@apply text-left;
			}
		}
	}
}
</style>
