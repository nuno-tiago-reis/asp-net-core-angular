// modules
import { DEFAULT_PICTURE } from '../../app.constants';

// components
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// models
import { User } from '../../-models/user';
import { CreateRequest, UpdateRequest } from './users.models';

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
	public getAll (): Observable<User[]>
	{
		const observable = this.http.get<User[]>(this.baseURL).pipe(map
		(
			(body: User[]) =>
			{
				body.forEach(

					(user) =>
					{
						if (user.profilePictureUrl === '' || user.profilePictureUrl === null)
						{
							user.profilePictureUrl = DEFAULT_PICTURE;
						}
					}
				);

				return body;
			}
		));

		return observable;
	}
}