<template>
	<div class="color-inputs">
		<div class="color-input"
			v-for="name in Object.keys(themeColors) as [MainColorRole]"
			:key="name"
		>
			<label :for="name" class="color-label">{{ name }}</label>
			<label
				:for="name"
				class="color-display"
				:style="{ backgroundColor: getColor(themeColors[name]) }"
			>
				<input
					type="color"
					hidden
					:id="name"
					:name="name"
					class="color-picker"
					:value="themeColors[name]?.value"
					@input="updateColor(name, $event.target.value)"
				/>
			</label>
		</div>
	</div>
</template>

<script setup lang="ts">
import { getColor } from '~/utils/getColor';
import type { PropType } from 'vue';
import { type MainColorRoles, MainColorRole } from '~/types/ColorRole';
import { ColorType } from '~/types/ColorType';
import type { HEX } from '~/types/PaletteColor';

defineProps({
	colors: {
		type: Object as PropType<MainColorRoles>,
		required: true,
	},
});

const themeColors = ref<MainColorRoles>(generateThemeColorsObject());

const updateColor = (name: MainColorRole, value: HEX) => {
	themeColors.value[name] = {
		type: ColorType.Color,
		value,
	};
};
</script>

<style scoped lang="scss">
.color-inputs {
	@apply flex flex-wrap justify-start gap-4;

	.color-input {
		@apply flex flex-col items-center justify-center gap-0.5em;

		.color-display {
			@apply inline-block w-8 h-8 border rounded-full;
		}
	}
}
</style>
