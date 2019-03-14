import { Pagination } from './pagination';

export class PaginatedResult<T>
{
	public results: T[];
	public pagination: Pagination;
}