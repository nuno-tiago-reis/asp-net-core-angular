import { User } from './user';

export interface Like
{
	id: string;
	senderID: string;
	sender: User;
	recipientID: string;
	recipient: User;
	createdAt: Date;
}