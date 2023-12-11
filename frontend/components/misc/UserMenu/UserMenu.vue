<template>
	<Popover.Root>
		<Popover.Trigger
			class="bg-transparent data-[state=open]:bg-surface p-2 rounded-t-lg"
		>
			<AvatarRoot class="flex h-[2em] w-[2em] select-none overflow-hidden rounded-full align-middle">
				<AvatarImage
					class="h-full w-full rounded-[inherit] object-cover"
					:src="user?.picture"
				/>
				<AvatarFallback
					class="grid h-full w-full rounded-[inherit] object-cover bg-surface"
				>
					<span
						v-if="isLoading"
						class="icon-svg-spinners:180-ring-with-bg inline-block col-end-1 row-end-1 place-self-center"
					/>
					<span
						v-else
						class="icon-ic:round-account-circle inline-block h-full w-full col-end-1 row-end-1"
					/>
				</AvatarFallback>
			</AvatarRoot>
		</Popover.Trigger>
		<Popover.Content
			class="bg-surface rounded-lg shadow-lg p-4 border border-surface rounded-rt-none cursor-default"
			align="end"
		>
			<FormKit
				type="form"
				:actions="false"
				:form-class="'grid gap-2'"
			>
				<b class="text-on-surface font-bold capitalize">{{ welcomeMessage }}</b>
				<FormKit
					type="submit"
					:outer-class="'justify-self-end'"
					@click="$emit('logout')"
				>
					<span class="icon-ic:round-logout inline-block w-1.25em h-1.25em" />
				</FormKit>
			</FormKit>
		</Popover.Content>
	</Popover.Root>
</template>

<script setup lang="ts">
import { Popover } from 'radix-vue/namespaced';
import { AvatarRoot, AvatarFallback } from 'radix-vue';
import type { User } from '@auth0/auth0-vue';

const props = defineProps<{
	isLoading?: boolean
	user?: User
}>();

defineEmits<{
	(e: 'logout'): void
}>();

const welcomeMessage = computed(() => {
	return `Hey ${ props.user?.nickname }`;
});
</script>

<style scoped lang="scss">
::v-deep(.formkit-input[type="submit"]) {
	@apply color-on-error bg-error transition-colors duration-300 ease-in-out;
	@apply rounded-lg transition-colors duration-300 ease-in-out font-inherit;
	@apply w-2.5em h-2.5em;
	@apply flex items-center justify-center;
}
</style>
