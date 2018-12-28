// modules
import { DEFAULT_PICTURE } from '../../app.constants';

// components
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// models
import { CreateRequest, UpdateRequest } from './users.models';
import { User } from '../../-models/user';
import { PaginatedResult } from '../../-models/paginated-result';

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
	 */
	public getAll (pageNumber?: number, pageSize?: number): Observable<PaginatedResult<User>>
	{
		let parameters = new HttpParams();
		if (pageNumber !== null && pageSize !== null)
		{
			parameters = parameters.append('pageNumber', pageNumber.toString());
			parameters = parameters.append('pageSize', pageSize.toString());
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
}