
export interface LoginWithUserNameRequest
{
	userName: string;
	password: string;
}

export interface LoginWithPhoneNumberRequest
{
	phoneNumber: string;
	password: string;
}

export interface LoginWithEmailAddressRequest
{
	emailAddress: string;
	password: string;
}

export interface RegisterRequest
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

export interface AddPasswordRequest
{
	id: string;
	password: string;
}

export interface ChangePasswordRequest
{
	id: string;
	oldPassword: string;
	newPassword: string;
}