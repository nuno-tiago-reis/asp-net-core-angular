// modules
import { defaultPicture as DEFAULT_PICTURE } from '../../app.constants';

// components
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// models
import { Like } from '../../-models/like';
import { PaginatedResult } from '../../-models/paginated-result';
import { CreateRequest, UpdateRequest, LikeParameters } from '../likes/likes.models';

// environment
import { environment } from '../../../environments/environment';

@Injectable
(({
	providedIn: 'root'
}) as any)

export class LikesService
{
	/**
	 * The user id template name.
	 */
	private readonly userID = 'userID';

	/**
	 * The likes API base url.
	 */
	private readonly baseURL = environment.apiUrl + `users/${this.userID}/likes/`;

	/**
	 * Creates an instance of the likes service.
	 *
	 * @param http The http client.
	 */
	public constructor (private readonly http: HttpClient)
	{
		// Nothing to do here.
	}

	/**
	 * Creates a like (the user id is mandatory).
	 *
	 * @param userID The user id.
	 * @param model The model.
	 */
	public create (userID: string, model: CreateRequest): Observable<Like>
	{
		const observable = this.http.post<Like>(this.baseURL.replace(this.userID, userID), model).pipe(map
		(
			(response) =>
			{
				if (response.sender.profilePictureUrl === '' || response.sender.profilePictureUrl === null)
				{
					response.sender.profilePictureUrl = DEFAULT_PICTURE;
				}
				if (response.recipient.profilePictureUrl === '' || response.recipient.profilePictureUrl === null)
				{
					response.recipient.profilePictureUrl = DEFAULT_PICTURE;
				}

				return response;
			}
		));

		return observable;
	}

	/**
	 * Updates a like (the like id and user id are mandatory).
	 *
	 * @param likeID The like ID.
	 * @param userID The user id.
	 * @param model The model.
	 */
	public update (likeID: string, userID: string, model: UpdateRequest): Observable<void>
	{
		const observable = this.http.put<void>(this.baseURL.replace(this.userID, userID) + likeID, model);

		return observable;
	}

	/**
	 * Deletes a like (the like id and user id are mandatory).
	 *
	 * @param likeID The like ID.
	 * @param userID The user id.
	 */
	public delete (likeID: string, userID: string): Observable<void>
	{
		const observable = this.http.delete<void>(this.baseURL.replace(this.userID, userID) + likeID);

		return observable;
	}

	/**
	 * Gets a like (the like id and user id are mandatory).
	 *
	 * @param likeID The like ID.
	 * @param userID The user id.
	 */
	public get (likeID: string, userID: string): Observable<Like>
	{
		const observable = this.http.get<Like>(this.baseURL.replace(this.userID, userID) + likeID).pipe(map
		(
			(response) =>
			{
				if (response.sender.profilePictureUrl === '' || response.sender.profilePictureUrl === null)
				{
					response.sender.profilePictureUrl = DEFAULT_PICTURE;
				}
				if (response.recipient.profilePictureUrl === '' || response.recipient.profilePictureUrl === null)
				{
					response.recipient.profilePictureUrl = DEFAULT_PICTURE;
				}

				return response;
			}
		));

		return observable;
	}

	/**
	 * Gets all likes (the user id is mandatory).
	 *
	 * @param userID The user id.
	 * @param pageNumber The page number.
	 * @param pageSize The page size.
	 * @param filterParameters The filter parameters.
	 */
	public getAll (userID: string, pageNumber?: number, pageSize?: number, filterParameters?: LikeParameters): Observable<PaginatedResult<Like>>
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
			parameters = parameters.append('includeRequestUser', filterParameters.includeRequestUser.toString());
		}

		const observable = this.http.get<Like[]>(this.baseURL.replace(this.userID, userID), { observe: 'response', params: parameters }).pipe(map
		(
			(response) =>
			{
				const paginatedResult = new PaginatedResult<Like>();
				paginatedResult.results = response.body;

				const pagination = response.headers.get('Pagination');
				if (pagination !== null)
				{
					paginatedResult.pagination = JSON.parse(pagination);
				}

				response.body.forEach((like) =>
				{
					if (like.sender !== null && (like.sender.profilePictureUrl === '' || like.sender.profilePictureUrl === null))
					{
						like.sender.profilePictureUrl = DEFAULT_PICTURE;
					}
					if (like.recipient !== null && (like.recipient.profilePictureUrl === '' || like.recipient.profilePictureUrl === null))
					{
						like.recipient.profilePictureUrl = DEFAULT_PICTURE;
					}
				});

				return paginatedResult;
			}
		));

		return observable;
	}
}