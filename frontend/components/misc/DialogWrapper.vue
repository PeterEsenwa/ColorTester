<template>
	<DialogRoot>
		<DialogTrigger
			as="button"
			class="w-fit px-4 py-2 bg-blue-500 hover:bg-blue-700 text-white rounded"
		>
			<!-- Slot for custom button content -->
			<slot name="trigger">
				Open Dialog
			</slot>
		</DialogTrigger>
		<DialogPortal>
			<DialogOverlay class="fixed inset-0 bg-black opacity-50" />

			<Transition
				enter="ease-out duration-300"
				enter-from="opacity-0"
				enter-to="opacity-100"
				leave="ease-in duration-200"
				leave-from="opacity-100"
				leave-to="opacity-0"
			>
				<DialogContent
					class="fixed top-[50%] left-[50%] translate-x-[-50%] translate-y-[-50%] shadow-[hsl(206_22%_7%_/_35%)_0px_10px_38px_-10px,_hsl(206_22%_7%_/_20%)_0px_10px_20px_-15px] focus:outline-none"
				>
					<DialogTitle
						v-if="showTitle"
						class="font-bold text-lg mb-4"
					>
						<!-- Slot for custom title content -->
						<slot name="title">
							Dialog Title
						</slot>
						<DialogClose class="text-gray-400 hover:text-gray-500" />
					</DialogTitle>
					<LazyVisuallyHidden v-else>
						<DialogTitle
							class="font-bold text-lg mb-4"
						>
							<!-- Slot for custom title content -->
							<slot name="title">
								Dialog Title
							</slot>
							<DialogClose class="text-gray-400 hover:text-gray-500" />
						</DialogTitle>
					</LazyVisuallyHidden>
					<!-- Slot for custom content -->
					<slot />
				</DialogContent>
			</Transition>
		</DialogPortal>
	</DialogRoot>
</template>

<script setup
	lang="ts"
>
import { DialogClose, DialogContent, DialogOverlay, DialogPortal, DialogRoot, DialogTitle, DialogTrigger, } from 'radix-vue'

const props = withDefaults(defineProps<{
	showTitle?: boolean
}>(), {
	showTitle: true,
})

const emit = defineEmits([ 'update:open' ])

function close() {
	emit('update:open', false)
}
</script>

<style scoped>
/* Add any additional styling here */
</style>
