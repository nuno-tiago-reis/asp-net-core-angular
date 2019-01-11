export interface CreateRequest
{
	recipientID: string;
}

export interface UpdateRequest
{
	recipientID: string;
}

export enum LikeMode
{
	Recipients = 'recipients',
	Senders = 'senders'
}

export interface LikeParameters
{
	mode: LikeMode;
	includeRequestUser: boolean;
}