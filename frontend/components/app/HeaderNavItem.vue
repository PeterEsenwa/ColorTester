<template>
		<NavigationMenuItem>
			<slot>
				<router-link :to="to">{{ title }}</router-link>
			</slot>
		</NavigationMenuItem>
</template>

<script setup lang="ts">
const props = defineProps<{
	title: string;
	to?: string;
}>();
</script>

<style scoped lang="scss">
li {
	@apply flex flex-row gap-4 cursor-pointer p-2;
	@apply hover:bg-accent hover:text-onaccent;

	@apply rd-[--item-rd,_0.25em];

	a {
		// remove default link styles
		@apply no-underline appearance-none;
	}
}
</style>
