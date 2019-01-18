export const environment =
{
	production: false,

	whitelistedDomains:
	[
		'localhost:44350',
		'kindly.com:44350'
	],

	blacklistedRoutes:
	[
		'localhost:44350/api/auth',
		'kindly.com:44350/api/auth'
	],

	apiUrl: 'https://kindly.com:44350/api/'
};