// modules
import { DEFAULT_PICTURE } from '../../app.constants';

// components
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// models
import { User } from '../../-models/user';
import { PaginatedResult } from '../../-models/paginated-result';
import { CreateRequest, UpdateRequest, UserParameters } from './users.models';

// environment
import { environment } from '../../../environments/environment';

@Injectable
({
	providedIn: 'root'
})

export class UsersService
{
	/**
	 * The users API base url.
	 */
	private readonly baseURL = environment.apiUrl + 'users/';

	/**
	 * Creates an instance of the users service.
	 *
	 * @param http The http client.
	 */
	public constructor (private readonly http: HttpClient)
	{
		// Nothing to do here.
	}

	/**
	 * Creates a user.
	 *
	 * @param model The model.
	 */
	public create (model: CreateRequest): Observable<User>
	{
		const observable = this.http.post<User>(this.baseURL, model);

		return observable;
	}

	/**
	 * Updates a user (the id is mandatory).
	 *
	 * @param id The id.
	 * @param model The model.
	 */
	public update (id: string, model: UpdateRequest): Observable<void>
	{
		const observable = this.http.put<void>(this.baseURL + id, model);

		return observable;
	}

	/**
	 * Deletes a user (the id is mandatory).
	 *
	 * @param id The id.
	 */
	public delete (id: string): Observable<void>
	{
		const observable = this.http.delete<void>(this.baseURL + id);

		return observable;
	}

	/**
	 * Gets a user (the id is mandatory).
	 *
	 * @param id The id.
	 */
	public get (id: string): Observable<User>
	{
		const observable = this.http.get<User>(this.baseURL + id).pipe(map
		(
			(body: User) =>
			{
				if (body.profilePictureUrl === '' || body.profilePictureUrl === null)
				{
					body.profilePictureUrl = DEFAULT_PICTURE;
				}

				return body;
			}
		));

		return observable;
	}

	/**
	 * Gets all users.
	 *
	 * @param pageNumber The page number.
	 * @param pageSize The page size.
	 * @param filterParameters The filter parameters.
	 */
	public getAll (pageNumber?: number, pageSize?: number, filterParameters?: UserParameters): Observable<PaginatedResult<User>>
	{
		let parameters = new HttpParams();

		if (pageNumber !== null && pageSize !== null)
		{
			parameters = parameters.append('pageNumber', pageNumber.toString());
			parameters = parameters.append('pageSize', pageSize.toString());
		}

		if (filterParameters != null)
		{
			parameters = parameters.append('gender', filterParameters.gender);
			parameters = parameters.append('minimumAge', filterParameters.minimumAge.toString());
			parameters = parameters.append('maximumAge', filterParameters.maximumAge.toString());
			parameters = parameters.append('orderBy', filterParameters.orderBy);
		}

		const observable = this.http.get<User[]>(this.baseURL, { observe: 'response', params: parameters }).pipe(map
		(
			(response) =>
			{
				response.body.forEach
				(
					(user) =>
					{
						if (user.profilePictureUrl === '' || user.profilePictureUrl === null)
						{
							user.profilePictureUrl = DEFAULT_PICTURE;
						}
					}
				);

				const paginatedResult: PaginatedResult<User> = new PaginatedResult<User>();
				paginatedResult.results = response.body;

				const pagination = response.headers.get('Pagination');
				if (pagination !== null)
				{
					paginatedResult.pagination = JSON.parse(pagination);
				}

				return paginatedResult;
			}
		));

		return observable;
	}

	/**
	 * Gets all users with roles.
	 *
	 * @param pageNumber The page number.
	 * @param pageSize The page size.
	 */
	public getAllWithRoles (pageNumber?: number, pageSize?: number): Observable<PaginatedResult<User>>
	{
		let parameters = new HttpParams();

		if (pageNumber !== null && pageSize !== null)
		{
			parameters = parameters.append('pageNumber', pageNumber.toString());
			parameters = parameters.append('pageSize', pageSize.toString());
		}

		const observable = this.http.get<User[]>(this.baseURL + 'roles/', { observe: 'response', params: parameters }).pipe(map
		(
			(response) =>
			{
				response.body.forEach
				(
					(user) =>
					{
						if (user.profilePictureUrl === '' || user.profilePictureUrl === null)
						{
							user.profilePictureUrl = DEFAULT_PICTURE;
						}
					}
				);

				const paginatedResult: PaginatedResult<User> = new PaginatedResult<User>();
				paginatedResult.results = response.body;

				const pagination = response.headers.get('Pagination');
				if (pagination !== null)
				{
					paginatedResult.pagination = JSON.parse(pagination);
				}

				return paginatedResult;
			}
		));

		return observable;
	}
}