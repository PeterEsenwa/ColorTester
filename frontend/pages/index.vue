<template>
	<template v-if="!isLoading && !isAuthenticated">
		<section class="hero-section section">
			<section class="text-section">
				<h1 class="title">
					Find the Perfect Palette for Your Project
				</h1>
				<p class="description">
					<!-- &#8209; is a non breakable hyphen -->
					Explore and apply color palettes on our many low&#8209;fidelity UI mockups.
				</p>
			</section>
			<section class="demo-section">
				<HomeRendererDemo />
			</section>
		</section>
	</template>

	<template v-else-if="!isLoading && user">
		<section class="dashboard-section section">
			<header class="welcome-header">
				<span class="welcome-text">
					Welcome back, {{ user.nickname || user.name }}
				</span>

				<h1 class="title">
					My Palettes and Projects
				</h1>
			</header>

			<div class="flex gap-2em">
				<div class="palettes-area">
					<HeadlessRadioGroup
						as="div"
						class="flex flex-col gap-2em"
						v-model="selectedPalette"
					>
						<template
							v-if="isLoadingPalettes"
						>
							<MiscPaletteViewLoader
								v-for="palette in 5"
								:key="palette"
							/>
						</template>
						<HeadlessRadioGroupOption
							v-for="palette in palettes"
							:key="palette.id"
							v-slot="{ checked }"
							:value="palette"
							as="div"
							class="grid grid-flow-col grid-cols-1 auto-cols-min gap-1em items-center cursor-pointer"
						>
							<PaletteView
								:is-loading="isLoadingPalettes"
								:colors="PaletteColorsToArray(palette.colors)"
							/>
							<span :class="getPaletteIndicatorClasses(checked)" />
						</HeadlessRadioGroupOption>
					</HeadlessRadioGroup>
				</div>
			</div>

<!--			<div
				class="add-palette-area"
			>
				<h5 class="title-2">
					Add a New Palette
				</h5>
				<PaletteEditor />
			</div>-->
		</section>
	</template>
</template>

<script setup lang="ts">
import type {ColorRoles} from '~/types/ColorRole';
import {useAuth0, type User} from '@auth0/auth0-vue';
import PaletteService from '~/services/palette.service';
import type {Palettes} from '~/types/Palette';
import {PaletteColorsToArray} from '~/types/PaletteColor';

const authClient = useAuth0()
const {
	auth,
	$apiFetch
} = useNuxtApp();

const isLoading = authClient?.isLoading;
const user: User | undefined = authClient?.user;
const isAuthenticated = authClient?.isAuthenticated;

const palettes = ref<Palettes | undefined>([]);
const isLoadingPalettes = ref(true);

onMounted(async () => {
	watch([isAuthenticated, $apiFetch], async ([doFetch]) => {
		if (!doFetch || !$apiFetch.value) {
			return;
		}

		const paletteService = new PaletteService($apiFetch.value, await authClient?.getAccessTokenSilently());

		// fetch palettes
		isLoadingPalettes.value = true;
		palettes.value = await paletteService.getPalettes();
		isLoadingPalettes.value = false;
	}, {
		immediate: true,
	});
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

const selectedPalette = ref();

const getPaletteIndicatorClasses = (isChecked: boolean) => {
	return [
		'inline-block h-100% w-1.25em aspect-square',
		{
			'icon-tabler:circle': !isChecked,
			'icon-tabler:circle-check-filled': isChecked,
		},
	];
};
</script>

<style scoped lang="scss">
.section {
  @apply bg-background;

  .title {
    @apply text-h2 font-bold;
    @apply w-full;
  }

  &.hero-section {
    @apply flex flex-col lg:flex-row items-center justify-center gap-4;
    @apply h-[calc(100%-var(--app-header-height))];

    .text-section {
      @apply flex flex-col items-start justify-center gap-4;
      @apply w-full;
      @apply md:w-50vw md:min-w-10em max-w-90vw;
      @apply text-on-background;

      .description {
        @apply text-body text-center font-medium;
        @apply md:w-25% md:min-w-15em max-w-90vw;
      }
    }

    .demo-section {
      @apply grid items-center;
    }
  }

  &.dashboard-section {
    @apply flex flex-col;
    @apply h-[calc(100%-var(--app-header-height))];
    @apply py-0 px-4em gap-2em;

    //	welcome header in top right corner
    .welcome-header {
      @apply align-end;

      // title has smaller font size
      .welcome-text {
        @apply text-overline font-medium;
      }

      .title {
        @apply text-h3;
      }
    }

    .palettes-area {
      @apply w-50%;
      @apply flex flex-col gap-2em;
    }

    .add-palette-area {
      @apply flex flex-col gap-2em;

      .title-2 {
        @apply text-h5 font-semibold text-end w-fit;
      }
    }
  }
}
</style>
