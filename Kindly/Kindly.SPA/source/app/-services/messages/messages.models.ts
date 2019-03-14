export interface CreateRequest
{
	content: string;
	recipientID: string;
}

export interface UpdateRequest
{
	isRead: boolean;
	readAt: Date;
}

export enum ContainerMode
{
	Inbox = 'inbox',
	Outbox = 'outbox',
	Unread = 'unread'
}

export interface MessageParameters
{
	container: ContainerMode;
}