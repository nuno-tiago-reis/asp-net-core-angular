export interface Token
{
	token: string;
}

export interface DecodedToken
{
	id: string;
	userName: string;
	profileName: string;
	phoneNumber: string;
	emailAddress: string;
	nbf: number;
	exp: number;
	iat: number;
}