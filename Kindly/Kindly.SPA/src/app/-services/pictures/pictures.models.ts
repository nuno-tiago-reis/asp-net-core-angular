export interface CreateRequest
{
	file: any;
	description: string;
	isProfilePicture: boolean;
}

export interface UpdateRequest
{
	description: string;
	isProfilePicture: boolean;
}