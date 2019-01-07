
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

export interface LoginWithEmailRequest
{
	email: string;
	password: string;
}

export interface RegisterRequest
{
	userName: string;
	phoneNumber: string;
	email: string;
	knownAs: string;
	gender: string;
	birthDate: Date;
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