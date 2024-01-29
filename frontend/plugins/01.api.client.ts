import { useAuth0 } from '@auth0/auth0-vue';
import { computedAsync } from '@vueuse/core';

/**
 * Plugin to provide a $fetch instance with default settings
 * to the nuxt app.
 */
export default defineNuxtPlugin((nuxtApp) => {
	const apiFetch = computedAsync(async (
		onCancel
	) => {
		// We want to return $fetch.create but use an if statement to check if a
		// user is authenticated. If there is an authenticated user, we want to
		// return the $fetch.create with the Authorization header set to the
		// user's access token. If there is no authenticated user, we want to
		// return the $fetch.create without the Authorization header.

		const authClient = useAuth0();
		const abortController = new AbortController()

		onCancel(() => abortController.abort())

		/*
				if (authClient.isAuthenticated) {
					const accessTokenSilently = await authClient.getAccessTokenSilently();

					return $fetch.create({
						baseURL: nuxtApp.$config.public.apiURL,
						headers: {
							Authorization: `Bearer ${accessTokenSilently}`
						},
						signal: abortController.signal
					})
				}
		*/

		return $fetch.create({
			baseURL: nuxtApp.$config.public.apiURL,
			signal: abortController.signal
		})
	})
	return {
		provide: {
			apiFetch
		}
	}
})
