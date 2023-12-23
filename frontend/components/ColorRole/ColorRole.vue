<template>
	<div class="color-role">
		<span class="color-role__role">
			{{ role }}
		</span>
		<ColorView
			ref="colorViewRef"
			v-model="color.value"
		/>
	</div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import type { PaletteColor } from '~/types/PaletteColor';
import ColorView from '~/components/color-view/ColorView.vue';
import type { ColorRole } from '~/types/ColorRole';

const props = defineProps<{
	role: ColorRole,
}>();

const color = ref<PaletteColor>({
	role: props.role,
});

const colorViewRef = ref<InstanceType<typeof ColorView> | null>(null);
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
