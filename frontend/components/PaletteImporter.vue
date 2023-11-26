<template>
	<div class="palette-importer">
		<!-- Button to open the dialog -->
		<button
			@click="isOpen = true"
			class="open-btn"
		>
			Import Palette
		</button>

		<!-- Dialog for importing colors -->
		<Dialog
			as="div"
			v-model:open="isOpen"
			class="dialog"
		>
			<DialogOverlay class="overlay" />

			<!-- This is the modal content -->
			<div class="content">
				<DialogPanel class="panel">
					<DialogTitle class="title">Import Color Palette</DialogTitle>
					<DialogDescription class="description">
						Enter color values manually or import a JSON configuration.
					</DialogDescription>
					<div class="checkbox-container">
						<input
							type="checkbox"
							id="toggle-on-colors"
							v-model="showOnColors"
						/>
						<label for="toggle-on-colors">Show "On*" Colors</label>
					</div>
					<div class="inputs">
						<!-- Manual color input fields -->
						<div class="color-inputs">
							<div
								class="color-input"
								v-for="[name, value] in getThemeColorsEntries(themeColors)"
								:key="name"
							>
								<label
									:for="name"
									class="color-label"
								>{{ name }}
								</label>
								<label
									:for="name"
									class="color-display"
									:style="{ backgroundColor: getColor(value) }"
								>
									<input
										type="color"
										hidden
										:id="name"
										:name="name"
										class="color-picker"
										v-model="themeColors[name]"
									/>
								</label>
							</div>
						</div>
						<div class="json-import my-4">
							<textarea
								placeholder="Paste your JSON here"
								v-model="jsonInput"
								@input="parseJsonInput"
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
import type { ColorSettings, ThemeColors } from '@/types/ColorSettings';
import { generateThemeColorsObject, getColor, getThemeColorsEntries } from '~/utils/generateThemeColorsObject';
import { useToggle } from '@vueuse/core';

const isOpen = ref(false);
const jsonInput = ref('');
const [ showOnColors, toggleOnColors ] = useToggle(false);
const themeColors = ref<ThemeColors>(generateThemeColorsObject());

// Emits
const emit = defineEmits<{
	(event: 'update-color-settings', colorSettings: ColorSettings): void;
}>();

const parseJsonInput = () => {
	try {
		const parsedColors = JSON.parse(jsonInput.value);
	} catch (error) {
		console.error('Invalid JSON:', error);
	}
};

const confirmColors = () => {
	emit('update-color-settings', { themeType: 'default', colors: themeColors.value });
	isOpen.value = false;
};
</script>

<style
	scoped
	lang="scss"
>
.palette-importer {
	.open-btn {
		@apply px-4 py-2 rounded bg-blue-600 text-white;
	}
}

.dialog {
	@apply bg-gray-100 fixed inset-0 z-10 overflow-y-auto;

	.overlay {
		@apply bg-black opacity-50 fixed inset-0 bg-black opacity-30;
	}

	.content {
		@apply flex flex-col items-center justify-center min-h-screen px-4 text-center;

		.panel {
			@apply bg-white rounded-lg shadow p-8 m-4 max-w-md max-h-full text-center w-full max-w-lg p-6 my-8 overflow-hidden;
			@apply text-left align-middle transition-all transform bg-white shadow-xl rounded-2xl;

			.title {
				@apply font-bold text-lg mb-4;
			}

			.description {
				@apply text-gray-700;
			}

			.inputs {
				@apply mt-4;

				.color-inputs {
					@apply flex flex-wrap justify-center gap-4;

					.color-input {
						@apply flex flex-col items-center justify-center gap-0.5em;

						.color-display {
							@apply inline-block w-8 h-8 border rounded-full;
						}
					}
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
