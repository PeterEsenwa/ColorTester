<template>
	<NavigationMenuRoot class="app-header">
		<nav class="nav">
			<NavigationMenuList class="flex flex-row gap-4 cursor-pointer">
				<HeaderNavItem
					title="Home"
					to="/"
				/>
				<HeaderNavItem
					title="About"
					to="/about"
				/>
				<HeaderNavItem
					v-if="!isAuthenticated"
					title="Login"
					@click="() => loginWithPopup()"
				>
					<span>Login</span>
				</HeaderNavItem>
				<HeaderNavItem
					v-if="!isAuthenticated"
					title="Sign Up"
					@click="() => loginWithPopup({
						authorizationParams: {
							screen_hint: 'signup'
						}
					})"
				>
					<span>Sign Up</span>
				</HeaderNavItem>
				<!-- TODO: Add Profile avatar and hover card -->
				<HeaderNavItem
					v-if="isAuthenticated"
					title="Logout"
					@click="() => logout()"
				>
					<span>Logout</span>
				</HeaderNavItem>
			</NavigationMenuList>
		</nav>
	</NavigationMenuRoot>
</template>

<script setup lang="ts">
import HeaderNavItem from '~/components/app/HeaderNavItem.vue';
import { NavigationMenuList, NavigationMenuRoot, } from 'radix-vue';
import { useAuth0 } from '@auth0/auth0-vue';

const { loginWithPopup, logout, user, isAuthenticated } = useAuth0();
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
