export interface CreateRequest
{
	userName: string;
	email: string;
	phoneNumber: string;
	knownAs: string;
	gender: string;
	age: number;
	city: string;
	country: string;
	introduction: string;
	lookingFor: string;
	interests: string;
}

export interface UpdateRequest
{
	userName: string;
	email: string;
	phoneNumber: string;
	knownAs: string;
	gender: string;
	age: number;
	city: string;
	country: string;
	introduction: string;
	lookingFor: string;
	interests: string;
}

export interface UpdateRolesRequest
{
	roles: string[]
}

export interface UserParameters
{
	gender: string;
	minimumAge: number;
	maximumAge: number;
	orderBy: string;
}