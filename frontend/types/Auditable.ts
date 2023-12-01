export type Auditable<T> = T & {
	createdAt: string;
	updatedAt: string;
	deletedAt: string;
}
