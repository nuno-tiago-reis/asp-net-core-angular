export interface CreateRequest
{
	userName: string;
	phoneNumber: string;
	emailAddress: string;
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
	phoneNumber: string;
	emailAddress: string;
	knownAs: string;
	gender: string;
	age: number;
	city: string;
	country: string;
	introduction: string;
	lookingFor: string;
	interests: string;
}

export interface UserParameters
{
	gender: string;
	minimumAge: number;
	maximumAge: number;
	orderBy: string;
}