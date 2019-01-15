export interface Picture
{
	id: string;
	url: string;
	publicID: string;
	description: string;
	isApproved: boolean;
	isProfilePicture: boolean;
	createdAt: Date;
	userID: string;
}