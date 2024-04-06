<template>
	<div
		class="skeleton-loader"
		:style="skeletonStyles"
	/>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue';
import { randomStr } from '@antfu/utils';

const direction = ref<'normal' | 'reverse'>('normal');
const primaryColor = ref<string>('#FFFFFF'); // Default color
const secondaryColor = ref<string>('#000000'); // Default color

function randomizeColors() {
	const randomColor = `#${randomStr(6, '0123456789ABCDEF')}`;
	if (Math.random() > 0.5) {
		secondaryColor.value = randomColor;
	} else {
		primaryColor.value = randomColor;
	}
}

onMounted(() => {
	randomizeColors();
	const interval = setInterval(randomizeColors, 2000);
	return () => clearInterval(interval);
});

const skeletonStyles = computed(() => ({
	'--animation-direction': direction.value,
	'--primary-color': primaryColor.value,
	'--secondary-color': secondaryColor.value,
}));
</script>

<style scoped lang="scss">
.skeleton-loader {
	--palette-view-color-size: 3rem;
	width: 100%;
	height: var(--palette-view-color-size);
	border-radius: 0.375rem; /* 6px */
	opacity: 0.5;
	background: linear-gradient(to right, var(--primary-color) 25%, var(--secondary-color) 50%, var(--primary-color) 75%);
	background-size: 200% 100%;
	animation: shift 2s linear infinite var(--animation-direction);
}

@keyframes shift {
	0% {
		background-position: 200% 0;
	}
	100% {
		background-position: -200% 0;
	}
}
</style>
