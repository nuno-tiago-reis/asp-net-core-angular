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
	password: string;
}

export interface UpdateRequest
{
	id: string;
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
	password: string;
}