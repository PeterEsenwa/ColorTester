<template>
	<div>
		<LazyAppHeader ref="appHeaderRef" />
		<slot />
	</div>
</template>

<script setup lang="ts">
defineOptions({
	name: 'DefaultLayout',
});

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

</script>

<style scoped lang="scss">
div {
	--app-header-height: v-bind(appHeaderHeightString);
}
</style>
