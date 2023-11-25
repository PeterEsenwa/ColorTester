<template>
	<Story>
		<div class="flex flex-col gap-2">
			<svg-renderer
				:svg-path="StorySVG"
				:color-settings="colorSettings"
			/>
		</div>
	</Story>
</template>

<script setup lang="ts">
import StorySVG from '@/samples/Chat_App.svg'
import type { ColorSettings } from '@/types/ColorSettings';

const colorSettings: ColorSettings = {
	themeType: 'default',
	colors: {
		primary: '#1D3557',
		secondary: '#A8DADC',
		background: '#F1FAEE',
		accent: '#457B9D',
		error: '#E63946',
		text: '#FFFFFF'
	}
}
</script>

<style scoped>

</style>
