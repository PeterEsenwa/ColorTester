import {API_BASE_URL} from 'assets/urls';
import ApiService from '~/services/api.service';
import type {$Fetch} from 'nitropack';

class MockupService extends ApiService {
	private readonly _accessToken: string | undefined;

	constructor(fetch: $Fetch | undefined, accessToken?: string) {
		if (!fetch) {
			throw new Error('MockupService: fetch is undefined');
		}

		super(fetch);
		this._accessToken = accessToken;
	}

	async fetchMockup(fileName: string): Promise<string | undefined> {
		try {
			// Fetch the signed URL from the backend
			const response = await this.fetch<{ url: string }>(`${API_BASE_URL}/api/MockUp/SignedURL/${fileName}`, {
				headers: {
					'Content-Type': 'application/json',
					'Authorization': 'Bearer ' + this._accessToken
				}
			});

			if (!(response && response.url)) {
				console.error('Failed to fetch signed URL');
				return undefined
			}

			// Fetch the MOCKUP content using the signed URL
			const mockupResponse = await fetch(response.url);

			// Render the MOCKUP content to the HTML element
			return await mockupResponse.text()
		} catch (e) {
			console.error(e);
			throw new Error('Failed to fetch MOCKUP');
		}
	}
}

export default MockupService;
