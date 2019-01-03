export interface CreateRequest
{
	targetID: string;
}

export interface UpdateRequest
{
	targetID: string;
}

export enum LikeMode
{
	Targets = 'targets',
	Sources = 'sources'
}

export interface LikeParameters
{
	mode: LikeMode;
	includeRequestUser: boolean;
}