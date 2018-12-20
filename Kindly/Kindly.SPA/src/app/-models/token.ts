export interface Token
{
	token: string;
}

export interface DecodedToken
{
	id: string;
	profileName: string;
	nbf: number;
	exp: number;
	iat: number;
}