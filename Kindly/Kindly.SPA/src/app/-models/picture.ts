export interface Picture
{
	id: string;
	url: string;
	publicID: string;
	description: string;
	isProfilePicture: boolean;
	createdAt: Date;
	userID: string;
}