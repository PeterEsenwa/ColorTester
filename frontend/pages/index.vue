<template>
	<AppHeader ref="appHeaderRef" />
	<ClientOnly>
		<section class="hero-section section">
			<h1 class="title">
				Find the Perfect Palette for Your Project
			</h1>
			<p class="description">
				<!-- &#8209; is a non breakable hyphen -->
				Explore and apply color palettes on our many low&#8209;fidelity UI mockups.
			</p>
		</section>

		<template v-if="false">
			<PaletteImporter
				v-if="step === Step.Import"
				@update="updateColors"
			/>
			<SvgRenderer
				v-else-if="step === Step.View"
				:svg-path="StorySVG"
				:palette-colors="colors"
			/>
		</template>
	</ClientOnly>
</template>

<script setup lang="ts">
import type { ColorRoles } from '~/types/ColorRole';
import StorySVG from '@/samples/Chat_App.svg';

// use AppHeaderRef to access the AppHeader component instance and get its height with utility from Vueuse
import { useElementSize, } from '@vueuse/core';

const appHeaderRef = ref<HTMLElement>();

const {
	height: appHeaderHeight
	// @ts-expect-error - Vueuse types are not up-to-date
} = useElementSize(appHeaderRef, 48, {
	box: 'border-box',
});

const appHeaderHeightString = computed(() => {
	return `${ appHeaderHeight.value }px`;
});

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
.section {
	--app-header-height: v-bind(appHeaderHeightString);

	@apply bg-background;

	&.hero-section {
		@apply flex flex-col items-center justify-center gap-4;
		@apply h-[calc(100%-var(--app-header-height))];

		.title {
			@apply text-h2 font-bold text-center;
			@apply md:w-50vw md:min-w-10em max-w-90vw;
			@apply text-on-background;
		}

		.description {
			@apply text-body text-center font-medium;
			@apply md:w-25vw md:min-w-15em max-w-90vw;
			@apply text-on-background line-height-snug;
		}
	}
}
</style>
