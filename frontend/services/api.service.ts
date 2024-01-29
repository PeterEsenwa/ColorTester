import { type  $Fetch } from 'nitropack';

export default abstract class ApiService {
	fetch: $Fetch
	protected constructor(
		fetch: $Fetch,
	) {
		this.fetch = fetch
	}
}
