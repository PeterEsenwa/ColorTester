<template>
	<header class="app-header">
		<nav class="nav">
			<ul class="links nav-links">
				<HeaderNavItem title="Home" to="/" />
				<HeaderNavItem title="About" to="/about" />
			</ul>

			<ul class="links other-links">
				<HeaderNavItem title="Log In" to="/login" />
				<HeaderNavItem title="Sign Up" to="/signup" />
			</ul>
		</nav>
	</header>
</template>

<script setup lang="ts">
import HeaderNavItem from '~/components/app/HeaderNavItem.vue';
</script>

<style scoped lang="scss">
.app-header {
	@apply flex justify-end items-center font-ss3v p-4;
	@apply bg-background text-onbackground;

	.dark & {
		@apply bg-background-dark text-onbackground-dark;
	}

	.nav {
		@apply flex justify-between items-center gap-4;

		.links {
			@apply flex flex-row gap-4 cursor-pointer;

			.nav-links {
			}

			.other-links {
			}
		}
	}
}
</style>
