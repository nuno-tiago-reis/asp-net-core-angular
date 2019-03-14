import { User } from './user';

export interface Message
{
	id: string;
	content: string;
	senderID: string;
	sender: User;
	senderDeleted: boolean;
	recipientID: string;
	recipient: User;
	recipientDeleted: boolean;
	isRead: boolean;
	readAt: Date;
	createdAt: Date;
}