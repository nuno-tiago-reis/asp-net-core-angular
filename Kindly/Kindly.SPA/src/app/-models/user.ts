import { Like } from './like';
import { Picture } from './picture';

export interface User
{
	id: string;
	userName: string;
	phoneNumber: string;
	emailAddress: string;
	knownAs: string;
	gender: Gender;
	age: number;
	city: string;
	country: string;
	introduction?: string;
	lookingFor?: string;
	interests?: string;
	profilePictureUrl?: string;
	pictures?: Picture[];
	likeTargets: Like[];
	likeSources: Like[];
	createdAt: Date;
	lastActiveAt: Date;
}

export enum Gender
{
	male = 'male',
	female = 'female',
	undefined = 'undefined'
}