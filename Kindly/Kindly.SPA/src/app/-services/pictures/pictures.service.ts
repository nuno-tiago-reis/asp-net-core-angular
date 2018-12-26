// components
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// models
import { Picture } from '../../-models/picture';
import { CreateRequest, UpdateRequest } from '../pictures/pictures.models';

// environment
import { environment } from '../../../environments/environment';

@Injectable
({
	providedIn: 'root'
})

export class PicturesService
{
	/**
	 * The user id template name.
	 */
	private readonly userID = 'userID';

	/**
	 * The pictures API base url.
	 */
	private readonly baseURL = environment.apiUrl + `users/${this.userID}/pictures/`;

	/**
	 * Creates an instance of the pictures service.
	 *
	 * @param http The http client.
	 */
	public constructor (private readonly http: HttpClient)
	{
		// Nothing to do here.
	}

	/**
	 * Creates a picture (the user id is mandatory).
	 *
	 * @param userID The user id.
	 * @param model The model.
	 */
	public create (userID: string, model: CreateRequest): Observable<Picture>
	{
		const observable = this.http.post<Picture>(this.baseURL.replace(this.userID, userID), model);

		return observable;
	}

	/**
	 * Updates a picture (the picture id and user id are mandatory).
	 *
	 * @param pictureID The picture ID.
	 * @param userID The user id.
	 * @param model The model.
	 */
	public update (pictureID: string, userID: string, model: UpdateRequest): Observable<void>
	{
		const observable = this.http.put<void>(this.baseURL.replace(this.userID, userID) + pictureID, model);

		return observable;
	}

	/**
	 * Deletes a picture (the picture id and user id are mandatory).
	 *
	 * @param pictureID The picture ID.
	 * @param userID The user id.
	 */
	public delete (pictureID: string, userID: string): Observable<void>
	{
		const observable = this.http.delete<void>(this.baseURL.replace(this.userID, userID) + pictureID);

		return observable;
	}

	/**
	 * Gets a picture (the picture id and user id are mandatory).
	 *
	 * @param pictureID The picture ID.
	 * @param userID The user id.
	 */
	public get (pictureID: string, userID: string): Observable<Picture>
	{
		const observable = this.http.get<Picture>(this.baseURL.replace(this.userID, userID) + pictureID);

		return observable;
	}

	/**
	 * Gets all pictures (the user id is mandatory).
	 *
	 * @param userID The user id.
	 */
	public getAll (userID: string): Observable<Picture[]>
	{
		const observable = this.http.get<Picture[]>(this.baseURL.replace(this.userID, userID));

		return observable;
	}
}