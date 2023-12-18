// preset to create CSS variables from theme
import type { Theme } from 'unocss/preset-uno';
import { definePreset } from '@unocss/core';

export default definePreset(() => {
	return {
		name: 'theme-to-variables',
		preflights: [ {
			getCSS: context => {
				const { theme } = context;

				let colorVariables: string = '';

				try {
					// get colors from theme
					const colors = (theme as Theme).colors;

					// get color entries
					const colorEntries = Object.entries(colors ?? {});

					// get color variables
					colorVariables = colorEntries.map(([ key, value ]) => {
						if (typeof value === 'string') {
							return `--color-${ key }: ${ value };`
						}
						return Object.entries(value).map(([ k, v ]) => {
							return `--color-${ key }-${ k }: ${ v };`
						}).join('\n')
					}).join('\n');
				} catch (error) {

				}

				return `
					:root {
						${ colorVariables }
					}
				`
			}
		} ],
	}
})
