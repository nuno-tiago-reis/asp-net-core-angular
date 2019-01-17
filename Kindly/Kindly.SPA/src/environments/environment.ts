export const environment =
{
	production: false,

	whitelistedDomains:
	[
		'localhost:44351',
		'kindly.com:44351'
	],

	blacklistedRoutes:
	[
		'localhost:44351/api/auth',
		'kindly.com:44351/api/auth'
	],

	apiUrl: 'https://localhost:44351/api/'
};