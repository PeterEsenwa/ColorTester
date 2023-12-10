<template>
	<NavigationMenuRoot class="app-header">
		<nav class="nav">
			<NavigationMenuList class="flex flex-row gap-4 cursor-pointer">
				<HeaderNavItem title="Home" to="/" />
				<HeaderNavItem title="About" to="/about" />
				<HeaderNavItem v-if="!isAuthenticated"
					title="Login" @click="() => $auth0.loginWithPopup()">
					<template #default>
						<span>Login</span>
					</template>
				</HeaderNavItem>
				<HeaderNavItem title="Register" to="/register" />
			</NavigationMenuList>
		</nav>
	</NavigationMenuRoot>
</template>

<script setup lang="ts">
import HeaderNavItem from '~/components/app/HeaderNavItem.vue';
import { NavigationMenuList, NavigationMenuRoot, } from 'radix-vue';
import type { Auth0Client } from '@auth0/auth0-spa-js';

const {
	$auth0
} = useNuxtApp();

let loginWithPopup: typeof $auth0['loginWithPopup'];
let isAuthenticated: typeof $auth0['isAuthenticated'];

watch(() => $auth0, (auth: Auth0Client) => {
	if (!auth) return;

	loginWithPopup = auth.loginWithPopup;
	isAuthenticated = auth.isAuthenticated;
}, {
	immediate: true
});
</script>

<style scoped lang="css">
.app-header {
	@apply flex justify-end items-center font-ss3v p-4;
	@apply bg-background text-onbackground;

	.dark & {
		@apply bg-background-dark text-onbackground-dark;
	}

	.nav {
		@apply flex justify-between items-center gap-4;

		[data-orientation='horizontal'] {
			@apply flex flex-row gap-4 cursor-pointer;

			.nav-links {
			}

			.other-links {
			}
		}
	}
}
</style>
