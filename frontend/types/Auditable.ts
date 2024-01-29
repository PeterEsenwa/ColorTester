import z from 'zod';

export type Auditable<T> = T & {
	createdAt: string;
	updatedAt?: string;
	deletedAt?: string;
}

// Zod schema for Auditable
export const AuditableSchema = z.object({
	createdAt: z.string(),
	// updatedAt and deletedAt are optional
	updatedAt: z.string().optional(),
	deletedAt: z.string().optional(),
});
