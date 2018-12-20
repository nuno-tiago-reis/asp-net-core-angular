export interface Picture
{
	id: string;
	url: string;
	description: string;
	isProfilePicture: boolean;
	addedAt: Date;
	userID: string;
}