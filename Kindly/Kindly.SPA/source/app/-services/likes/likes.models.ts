export interface CreateRequest
{
	recipientID: string;
}

export interface UpdateRequest
{
	recipientID: string;
}

export enum LikeContainer
{
	Recipients = 'recipients',
	Senders = 'senders'
}

export interface LikeParameters
{
	container: LikeContainer;
	includeRequestUser: boolean;
}