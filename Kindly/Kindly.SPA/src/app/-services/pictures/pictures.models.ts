export interface CreateRequest
{
	url: string;
	addedAt: Date;
	description: string;
	isProfilePicture: boolean;
	userID: string;
}

export interface UpdateRequest
{
	id: string;
	url: string;
	addedAt: Date;
	description: string;
	isProfilePicture: boolean;
	userID: string;
}