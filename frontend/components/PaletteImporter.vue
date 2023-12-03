<template>
	<div class="palette-importer">
		<!-- Button to open the dialog -->
		<div class="open-btn-container">
			<button
				@click="isOpen = true"
				class="open-btn"
			>
				Import Palette
			</button>
		</div>

		<!-- Dialog for importing colors -->
		<Dialog
			as="div"
			v-model:open="isOpen"
			class="dialog fixed inset-0 z-10 overflow-y-auto"
		>
			<DialogOverlay class="overlay" />

			<!-- This is the modal content -->
			<div class="content">
				<DialogPanel class="panel">
					<DialogTitle class="title">
						Import Color Palette

						<!-- Close button -->
						<MiscCloseButton
							@click="isOpen = false"
							class="close-btn"
						/>
					</DialogTitle>
					<DialogDescription class="description">
						Enter color values manually or import a JSON configuration.
					</DialogDescription>
					<div class="inputs">
						<!-- Manual color input fields -->
						<ColorInputs
							v-model="themeColors"
						/>
						<div class="checkbox-container">
							<input
								type="checkbox"
								id="toggle-on-colors"
								v-model="showOnColors"
							/>
							<label
								id="toggle-on-colors-label"
								for="toggle-on-colors"
							>
								Show "On*" Colors
							</label>
						</div>
						<ColorInputs
							v-if="showOnColors"
							v-model="themeOnColors"
						>
						</ColorInputs>
						<div class="json-import my-4">
							<textarea
								placeholder="Paste your JSON here"
								v-model="jsonInput"
								@input="parseJsonInput"
								name="json-color-input"
								class="json-field"
							></textarea>
						</div>
					</div>

					<div class="confirmation mt-4">
						<button
							type="button"
							@click="confirmColors"
							class="confirm-btn"
						>
							Confirm
						</button>
					</div>
				</DialogPanel>
			</div>
		</Dialog>
	</div>
</template>

<script
	setup
	lang="ts"
>
import { Dialog, DialogPanel, DialogTitle, DialogDescription, DialogOverlay } from '@headlessui/vue';
import { generateThemeColorsObject, generateThemeOnColorsObject } from '~/utils/generateThemeColorsObject';
import { useToggle } from '@vueuse/core';
import { type MainColorRoles, type OnColorRoles, type ColorRoles, type ColorRole, MainColorRole, OnColorRole } from '~/types/ColorRole';
import type { PaletteColor } from '~/types/PaletteColor';
import { ColorType } from '~/types/ColorType';
import type { RequireAtLeastOne } from 'type-fest';

const isOpen = ref(false);
const jsonInput = ref('');
const [ showOnColors, toggleOnColors ] = useToggle(false);
const themeColors = ref(generateThemeColorsObject());
const themeOnColors = ref(generateThemeOnColorsObject());

// Emits
const emit = defineEmits<{
	(event: 'update', colorSettings: RequireAtLeastOne<Record<ColorRole, PaletteColor>>): void;
}>();

const parseJsonInput = () => {
	try {
		const parsedColors = JSON.parse(jsonInput.value);

		// json will be an object with keys that are the color roles
		// and values that are the color values
		// e.g. { primary: '#000000' }

		// Loop through the keys and update the themeColors object and themeOnColors object
		for (const [ role, value ] of Object.entries(parsedColors)) {
			if (themeColors.value[role as MainColorRole]) {
				themeColors.value[role as MainColorRole] = {
					role: role as ColorRole,
					value: {
						type: ColorType.Color,
						value,
					}
				} as PaletteColor;
			} else if (themeOnColors.value[role as OnColorRole]) {
				themeOnColors.value[role as OnColorRole] = {
					role: role as ColorRole,
					value: {
						type: ColorType.Color,
						value,
					}
				} as PaletteColor;
			}
		}
	} catch (error) {
		console.error('Invalid JSON:', error);
	}
};

const colorSettings = computed<ColorRoles>(() => {
	return {
		...themeColors.value,
		...themeOnColors.value,
	};
});

const confirmColors = () => {
	emit('update', colorSettings.value);
	isOpen.value = false;
};
</script>

<style
	scoped
	lang="scss"
>
* {
	@apply box-border font-inherit;
}

.palette-importer {
	font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;

	.open-btn-container {
		@apply flex p-2 w-fit bg-white rounded overflow-hidden;

		.open-btn {
			@apply px-4 py-2 rounded bg-blue-600 text-white;
			@apply outline-none border-none cursor-pointer;
		}
	}
}

.dialog {
	.overlay {
		@apply bg-black opacity-50 fixed inset-0 bg-black opacity-30;
	}

	.content {
		font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;

		@apply flex flex-col items-center justify-center min-h-screen px-4 text-center;

		.panel {
			@apply bg-white rounded-lg shadow p-8 m-4 max-w-md max-h-full text-center w-full max-w-lg p-6 my-8 overflow-hidden;
			@apply text-left align-middle transition-all transform bg-white shadow-xl rounded-2xl;

			.title {
				@apply font-bold text-lg mb-4;
				@apply flex flex-row justify-between items-center;
			}

			.description {
				@apply text-gray-700;
			}

			.inputs {
				@apply mt-4;

				.checkbox-container {
					@apply flex flex-row my-4 gap-1;
				}
			}

			.json-field {
				@apply w-full px-3 py-2 border rounded text-gray-700 w-full p-2 border rounded;
			}

			.confirmation {
			}

			/* Apply styles as needed */

			.confirm-btn {
				@apply px-4 py-2 rounded bg-indigo-600 text-white;
			}
		}
	}
}
</style>
