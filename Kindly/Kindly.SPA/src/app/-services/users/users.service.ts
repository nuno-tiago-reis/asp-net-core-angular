import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

@Injectable({

	providedIn: 'root'
})

export class UsersService
{
	/**
	 * The users API base url.
	 */
	protected baseURL = 'https://localhost:44351/api/users/';

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
		observable.subscribe();

		return observable;
	}

	/**
	 * Updates a user.
	 * The id is mandatory (sample model below).
	 *
	 * {
	 * 	"userName" : "<placeholder>",
	 * 	"phoneNumber" : "<placeholder>",
	 * 	"emailAddress" : "<placeholder>"
	 * }
	 *
	 * @param id The id.
	 * @param model The model.
	 */
	public update (id: string, model: UpdateRequest): Observable<void>
	{
		const observable = this.http.put<void>(this.baseURL + id, model);
		observable.subscribe();

		return observable;
	}

	/**
	 * Deletes a user.
	 * The id is mandatory.
	 *
	 * @param id The id.
	 */
	public delete (id: string): Observable<void>
	{
		const observable = this.http.delete<void>(this.baseURL + id);
		observable.subscribe();

		return observable;
	}

	/**
	 * Gets a user.
	 * The id is mandatory.
	 *
	 * @param id The id.
	 */
	public get (id: string): Observable<User>
	{
		const observable = this.http.get<User>(this.baseURL + id);
		observable.subscribe();

		return observable;
	}

	/**
	 * Gets all users.
	 */
	public getAll (): Observable<User[]>
	{
		const observable = this.http.get<User[]>(this.baseURL);
		observable.subscribe();

		return observable;
	}
}

export interface User
{
	id: string;
	userName: string;
	phoneNumber: string;
	emailAddress: string;
}

export interface CreateRequest
{
	userName: string;
	phoneNumber: string;
	emailAddress: string;
}

export interface UpdateRequest
{
	userName: string;
	phoneNumber: string;
	emailAddress: string;
}