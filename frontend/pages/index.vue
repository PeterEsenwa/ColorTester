<template>
	<div class="container">
		<ClientOnly>
			<PaletteImporter
				v-if="step === Step.Import"
				@update="updateColors"
			/>
			<SvgRenderer
				v-else-if="step === Step.View"
				:svg-path="StorySVG"
				:palette-colors="colors"
			/>
		</ClientOnly>
	</div>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import type { ColorRoles } from '~/types/ColorRole';
import StorySVG from '@/samples/Chat_App.svg'

enum Step {
	Import = 1,
	View = 2,
}

const step = ref(Step.Import);

const colors = ref();

const updateColors = (newColors: ColorRoles) => {
	colors.value = newColors;
	step.value = Step.View;
};
</script>

<style scoped lang="scss">
.container {
	@apply m-0 p-4 rounded-lg bg-[#00000040] h-full w-full place-self-center;
}
</style>
