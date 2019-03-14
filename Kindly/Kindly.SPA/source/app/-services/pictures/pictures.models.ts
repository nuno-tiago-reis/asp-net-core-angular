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

export enum PictureMode
{
	Approved = 'approved',
	Unapproved = 'unapproved',
	Everything = 'everything'
}

export interface PictureParameters
{
	container: PictureMode;
}