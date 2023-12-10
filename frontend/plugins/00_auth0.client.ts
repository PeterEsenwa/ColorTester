import { createAuth0 } from '@auth0/auth0-vue';
import authConfig from '~/auth.config.json';

export default defineNuxtPlugin((nuxtApp) => {
	const auth0 = createAuth0({
		domain: authConfig.auth0.domain,
		clientId: authConfig.auth0.clientId,
		authorizationParams: {
			redirect_uri: window.location.origin
		}
	});

	nuxtApp.vueApp.use(auth0);

	return {
		provide: {
			auth0
		}
	};
});
