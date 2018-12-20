// components
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// models
import { Picture } from '../../-models/picture';
import { CreateRequest, UpdateRequest } from '../pictures/pictures.models';

// environment
import { environment } from '../../../environments/environment.development';

@Injectable
({
	providedIn: 'root'
})

export class PicturesService
{
	/**
	 * The pictures API base url.
	 */
	private baseURL = environment.apiUrl + 'users/pictures';

	/**
	 * Creates an instance of the pictures service.
	 *
	 * @param http The http client.
	 */
	public constructor (protected readonly http: HttpClient)
	{
		// Nothing to do here.
	}

	/**
	 * Creates a picture.
	 *
	 * @param model The model.
	 */
	public create (model: CreateRequest): Observable<Picture>
	{
		const observable = this.http.post<Picture>(this.baseURL, model);

		return observable;
	}

	/**
	 * Updates a picture.
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
	 * Deletes a picture (the id is mandatory).
	 *
	 * @param id The id.
	 */
	public delete (id: string): Observable<void>
	{
		const observable = this.http.delete<void>(this.baseURL + id);

		return observable;
	}

	/**
	 * Gets a picture (the id is mandatory).
	 *
	 * @param id The id.
	 */
	public get (id: string): Observable<Picture>
	{
		const observable = this.http.get<Picture>(this.baseURL + id);

		return observable;
	}

	/**
	 * Gets all pictures.
	 */
	public getAll (): Observable<Picture[]>
	{
		const observable = this.http.get<Picture[]>(this.baseURL);

		return observable;
	}
}