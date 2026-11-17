module.exports = {
	root: true,
	ignorePatterns: [ 'dist', '.output', '.nuxt' ],
	env: {
		browser: true,
		node: true,
	},
	extends: [
		'@nuxt/eslint-config',
		'plugin:vue/vue3-recommended',
	],
	overrides: [
		{
			files: [ '*.vue' ],
			rules: {
				'vue/no-multiple-template-root': 'off',
				'vue/html-indent': [ 'error', 'tab' ],
				'vue/no-unused-vars': [
					'error', {
						ignorePattern: '^_'
					}
				],
				'vue/multi-word-component-names': [
					'error', {
						ignores: [
							'index',
							'Index.story',
							'*.story.vue',
							'[id]',
						]
					}
				],
			}
		},
	]
};
