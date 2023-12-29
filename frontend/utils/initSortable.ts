import Sortable, {Swap} from 'sortablejs';

Sortable.mount(new Swap());

export const initSortable = (
	element: HTMLElement,
	options: Sortable.Options = {
	} as Sortable.Options,
) => {
	// check if Sortable is mounted

	return new Sortable(element, {
		...options,
	});
}
