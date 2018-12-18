import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse, HttpErrorResponse } from '@angular/common/http';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Service } from './../service';

@Injectable({

	providedIn: 'root'
})

export class UsersService extends Service
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
		super();
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
		const observable = this.http.post<User>(this.baseURL, model, { observe: 'response' });
		observable.subscribe(

			(response: HttpResponse<User>) =>
			{
				this.handleResponse(response);
			},
			(errorResponse: HttpErrorResponse) =>
			{
				this.handleErrorResponse(errorResponse);
			}
		);

		return observable.pipe(map((response: HttpResponse<User>) => response.body));
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
		const observable = this.http.put<void>(this.baseURL + id, model, { observe: 'response' });
		observable.subscribe(

			(response: HttpResponse<void>) =>
			{
				this.handleResponse(response);
			},
			(errorResponse: HttpErrorResponse) =>
			{
				this.handleErrorResponse(errorResponse);
			}
		);

		return observable.pipe(map((response: HttpResponse<void>) => response.body));
	}

	/**
	 * Deletes a user.
	 * The id is mandatory.
	 *
	 * @param id The id.
	 */
	public delete (id: string): Observable<void>
	{
		const observable = this.http.delete<void>(this.baseURL + id, { observe: 'response' });
		observable.subscribe(

			(response: HttpResponse<void>) =>
			{
				this.handleResponse(response);
			},
			(errorResponse: HttpErrorResponse) =>
			{
				this.handleErrorResponse(errorResponse);
			}
		);

		return observable.pipe(map((response: HttpResponse<void>) => response.body));
	}

	/**
	 * Gets a user.
	 * The id is mandatory.
	 *
	 * @param id The id.
	 */
	public get (id: string): Observable<User>
	{
		const observable = this.http.get<User>(this.baseURL + id, { observe: 'response' });
		observable.subscribe(

			(response: HttpResponse<User>) =>
			{
				this.handleResponse(response);
			},
			(errorResponse: HttpErrorResponse) =>
			{
				this.handleErrorResponse(errorResponse);
			}
		);

		return observable.pipe(map((response: HttpResponse<User>) => response.body));
	}

	/**
	 * Gets all users.
	 */
	public getAll (): Observable<User[]>
	{
		const observable = this.http.get<User[]>(this.baseURL, { observe: 'response' });
		observable.subscribe(

			(response: HttpResponse<User[]>) =>
			{
				this.handleResponse(response);
			},
			(errorResponse: HttpErrorResponse) =>
			{
				this.handleErrorResponse(errorResponse);
			}
		);

		return observable.pipe(map((response: HttpResponse<User[]>) => response.body));
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