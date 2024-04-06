import { definePreset } from '@unocss/core'

const remRE = /(-?[.\d]+)rem/g

export interface RemToEmOptions {
	/**
	 * 1rem = n em
	 * @default 1
	 */
	baseFontSize?: number
}

export const presetRemToEM = definePreset((options: RemToEmOptions = {}) => {
	const {
		baseFontSize = 1,
	} = options

	return {
		name: '@unocss/preset-rem-to-em',
		postprocess: (util) => {
			util.entries.forEach((i) => {
				const value = i[1]
				if (typeof value === 'string' && remRE.test(value))
					i[1] = value.replace(remRE, (_, p1) => `${p1 * baseFontSize}em`)
			})
		},
	}
})

export default presetRemToEM
