import { User } from './user';

export interface LoginToken
{
	user: User;
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