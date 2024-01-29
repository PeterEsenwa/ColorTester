import type { Palettes } from '~/types/Palette';
import ApiService from '~/services/api.service';
import { API_BASE_URL } from 'assets/urls';
import type { $Fetch } from 'nitropack';
import { PalettesSchema } from '~/types/Palette';
import { parseDataAs } from '@sidebase/nuxt-parse';
import camelcaseKeys from 'camelcase-keys';

class PaletteService extends ApiService {
	private readonly _accessToken: string | undefined;
	constructor(
		fetch: $Fetch | undefined,
		accessToken: string | undefined
	) {
		if (!fetch) {
			throw new Error('PaletteService: fetch is undefined');
		}

		super(fetch);
		this._accessToken = accessToken;
	}

	async getPalettes(): Promise<Palettes | undefined> {
		try {
			const palettes = await this.fetch<Palettes | undefined>(`${ API_BASE_URL }/api/Palette`, {
				headers: {
					'Content-Type': 'application/json',
					'Authorization': 'Bearer ' + this._accessToken,
					'token_dialect': 'access_token'
				}
			});

			if (palettes && Array.isArray(palettes)) {
				return PalettesSchema.parse(palettes);
			}

		} catch (e) {
			console.error(e);
			throw new Error('Failed to fetch palettes');
		}

		return undefined;
	}

	/*static async getPalette(id: string) {
		return await axios.get(`${ API_BASE_URL }/api/Palette/${ id }`);
	}

	static async createPalette(data: Record<string, string>) {
		return await axios.post(`${ API_BASE_URL }/api/Palette`, data);
	}

	static async updatePalette(id: string, data: Record<string, string>) {
		return await axios.put(`${ API_BASE_URL }/api/Palette/${ id }`, data);
	}

	static async deletePalette(id: string) {
		return await axios.delete(`${ API_BASE_URL }/api/Palette/${ id }`);
	}*/
}

export default PaletteService;
