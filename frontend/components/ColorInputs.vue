<template>
	<div class="color-inputs" v-if="themeColors">
		<div class="color-input"
			v-for="name in Object.keys(themeColors) as[ MainColorRole | OnColorRole]"
			:key="name"
		>
			<label :for="name" class="color-label">{{ name }}</label>
			<label
				:for="name"
				class="color-display"
				:class="{ 'filled': inputHasValue(themeColors[name]?.value) }"
				:style="{ backgroundColor: getColor(themeColors[name]) }"
			>
				<input
					type="color"
					:id="name"
					:name="name"
					class="sr-only top-100%"
					:value="getColor(themeColors[name])"
					@input="updateColor(name, $event.target?.value)"
				/>
			</label>
		</div>
	</div>
</template>

<script setup lang="ts">
import { getColor } from '~/utils/getColor';
import { MainColorRole, OnColorRole, type MainColorRoles, type OnColorRoles, type ColorRoles } from '~/types/ColorRole';
import { ColorType } from '~/types/ColorType';
import type { HEX, PaletteColor, ColorValue, ColorDictionaryValue } from '~/types/PaletteColor';

const inputHasValue = (input?: ColorValue | ColorDictionaryValue) => {
	if (!input) return false;

	if (input.type === ColorType.Color) {
		return !!input.value;
	}

	return Object.values(input.value).some((value) => !!value);
};

const themeColors = defineModel<Partial<ColorRoles>>()

const updateColor = (
	name: MainColorRole | OnColorRole,
	value: HEX
) => {
	if (!themeColors.value) return;

	themeColors.value[name] = {
		role: name,
		value: {
			type: ColorType.Color,
			value,
		}
	} as PaletteColor;
};
</script>

<style scoped lang="scss">
.color-inputs {
	@apply flex flex-wrap justify-start gap-4;

	.color-input {
		@apply flex flex-col items-center justify-center gap-0.5em;

		.color-display {
			@apply inline-block w-8 h-8 border border-dashed border-[#000000] rounded-full;
		}
	}
}
</style>
