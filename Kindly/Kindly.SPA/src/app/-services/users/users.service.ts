// components
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// models
import { User } from '../../-models/user';
import { CreateRequest, UpdateRequest } from './users.models';

// environment
import { environment } from '../../../environments/environment.development';

@Injectable
({
	providedIn: 'root'
})

export class UsersService
{
	/**
	 * The users API base url.
	 */
	private baseURL = environment.apiUrl + 'users/';

	/**
	 * Creates an instance of the users service.
	 *
	 * @param http The http client.
	 */
	public constructor (protected readonly http: HttpClient)
	{
		// Nothing to do here.
	}

	/**
	 * Creates a user.
	 * The username, phone number and email address are mandatory (sample model below).
	 *
	 * {
	 * 	"userName" : "<placeholder>",
	 * 	"phoneNumber" : "<placeholder>",
	 * 	"emailAddress" : "<placeholder>"
	 * }
	 *
	 * @param model The model.
	 */
	public create (model: CreateRequest): Observable<User>
	{
		const observable = this.http.post<User>(this.baseURL, model);

		return observable;
	}

	/**
	 * Updates a user.
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
		const observable = this.http.get<User>(this.baseURL + id);

		return observable;
	}

	/**
	 * Gets all users.
	 */
	public getAll (): Observable<User[]>
	{
		const observable = this.http.get<User[]>(this.baseURL);

		return observable;
	}
}