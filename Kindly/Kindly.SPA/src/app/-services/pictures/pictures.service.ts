// components
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// models
import { Picture } from '../../-models/picture';
import { PaginatedResult } from '../../-models/paginated-result';
import { CreateRequest, UpdateRequest, PictureParameters } from '../pictures/pictures.models';

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
	 * The pictures API base url for administration operations.
	 */
	private readonly administrationBaseURL = environment.apiUrl + 'users/pictures/';

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
	 * @param pageNumber The page number.
	 * @param pageSize The page size.
	 * @param filterParameters The filter parameters.
	 */
	public getAll (userID: string, pageNumber?: number, pageSize?: number, filterParameters?: PictureParameters): Observable<PaginatedResult<Picture>>
	{
		let parameters = new HttpParams();

		if (pageNumber !== null && pageSize !== null)
		{
			parameters = parameters.append('pageNumber', pageNumber.toString());
			parameters = parameters.append('pageSize', pageSize.toString());
		}

		if (filterParameters != null)
		{
			parameters = parameters.append('container', filterParameters.container);
		}

		const observable = this.http.get<Picture[]>(this.baseURL.replace(this.userID, userID), { observe: 'response', params: parameters }).pipe(map
		(
			(response) =>
			{
				const paginatedResult: PaginatedResult<Picture> = new PaginatedResult<Picture>();
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
	 * Approves a picture (the picture id is mandatory).
	 *
	 * @param pictureID The picture ID.
	 */
	public approve (pictureID: string): Observable<void>
	{
		const observable = this.http.put<void>(this.administrationBaseURL + pictureID, null);

		return observable;
	}

	/**
	 * Rejects a picture (the picture id is mandatory).
	 *
	 * @param pictureID The picture ID.
	 */
	public reject (pictureID: string): Observable<void>
	{
		const observable = this.http.delete<void>(this.administrationBaseURL + pictureID);

		return observable;
	}

	/**
	 * Gets all pictures for approval/rejection.
	 *
	 * @param pageNumber The page number.
	 * @param pageSize The page size.
	 * @param filterParameters The filter parameters.
	 */
	public getAllForAdministration (pageNumber?: number, pageSize?: number, filterParameters?: PictureParameters): Observable<PaginatedResult<Picture>>
	{
		let parameters = new HttpParams();

		if (pageNumber !== null && pageSize !== null)
		{
			parameters = parameters.append('pageNumber', pageNumber.toString());
			parameters = parameters.append('pageSize', pageSize.toString());
		}

		if (filterParameters != null)
		{
			parameters = parameters.append('container', filterParameters.container);
		}

		const observable = this.http.get<Picture[]>(this.administrationBaseURL, { observe: 'response', params: parameters }).pipe(map
		(
			(response) =>
			{
				const paginatedResult: PaginatedResult<Picture> = new PaginatedResult<Picture>();
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