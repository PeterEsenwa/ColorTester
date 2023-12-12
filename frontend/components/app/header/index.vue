<template>
	<NavigationMenuRoot class="app-header">
		<nav class="nav">
			<NavigationMenuList class="flex flex-row gap-4 cursor-pointer items-center">
				<AppHeaderNavItem
					title="Home"
					to="/"
				/>
				<AppHeaderNavItem
					title="About"
					to="/about"
				/>
				<template v-if="!isLoading">
					<AppHeaderNavItem
						v-if="!isAuthenticated"
						title="Login"
						@click="() => loginWithPopup?.()"
					>
						<span>Login</span>
					</AppHeaderNavItem>
					<AppHeaderNavItem
						v-if="!isAuthenticated"
						title="Sign Up"
						@click="() => loginWithPopup?.({
							authorizationParams: {
								screen_hint: 'signup'
							}
						})"
					>
						<span>Sign Up</span>
					</AppHeaderNavItem>
				</template>
				<LazyMiscUserMenu
					v-if="isLoading || isAuthenticated && user"
					:is-loading="isLoading"
					:user="user"
					@logout="logout"
				/>
			</NavigationMenuList>
		</nav>
	</NavigationMenuRoot>
</template>

<script setup lang="ts">
import { NavigationMenuList, NavigationMenuRoot, } from 'radix-vue';
import { useAuth0 } from '@auth0/auth0-vue';

const authClient = useAuth0()

const loginWithPopup = authClient?.loginWithPopup;
const isLoading = authClient?.isLoading;
const logout = authClient?.logout;
const user = authClient?.user;
const isAuthenticated = authClient?.isAuthenticated;
</script>

<style scoped lang="css">
.app-header {
	@apply flex justify-end items-center font-ss3v p-4;
	@apply bg-background text-on-background;

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
