import axios from 'axios';
import type { Palettes } from '~/types/Palette';

const API_BASE_URL = 'http://localhost:5112';

class PaletteService {
	static async getPalettes(): Promise<Palettes | undefined> {
		const axiosResponse = await axios.get(`${ API_BASE_URL }/api/Palette`);

		if (axiosResponse.status !== 200) {
			throw new Error('Failed to fetch palettes');
		}

		if (axiosResponse.data) {
			const { data } = axiosResponse;
			if (Array.isArray(data)) {
				return data as Palettes;
			}
		}

		return undefined;
	}

	static async getPalette(id: string) {
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
	}
}

export default PaletteService;
