module.exports = {
	env: {
		browser: true,
		node: true,
	},
	overrides: [
		{
			files: [ '*.vue' ],
			parser: require.resolve('vue-eslint-parser'),
			parserOptions: {
				parser: require.resolve('@typescript-eslint/parser'),
				extraFileExtensions: [ '.vue' ]
			},
			extends: [
				'plugin:vue/vue3-recommended',
				'@vue/typescript/recommended'
			],
			rules: {
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
						]
					}
				],
			}
		},
		{
			files: [ '*.ts', '*.tsx' ],
			parser: require.resolve('@typescript-eslint/parser'),
			extends: [
				'@vue/typescript/recommended'
			],
			rules: {
				// Custom rules here
			}
		}
	]
};
