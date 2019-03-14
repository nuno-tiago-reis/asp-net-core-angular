import { User } from './user';

export interface LoginToken
{
	user: User;
	token: string;
}

export interface DecodedToken
{
	nameid: string;
	role: string[];
	nbf: number;
	exp: number;
	iat: number;
}