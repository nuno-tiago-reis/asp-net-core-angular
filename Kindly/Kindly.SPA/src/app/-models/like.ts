import { User } from './user';

export interface Like
{
	id: string;
	sourceID: string;
	source: User;
	targetID: string;
	target: User;
	createdAt: Date;
}