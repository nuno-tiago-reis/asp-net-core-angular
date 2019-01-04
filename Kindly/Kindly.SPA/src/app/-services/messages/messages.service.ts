// components
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// models
import { Message } from '../../-models/message';
import { PaginatedResult } from '../../-models/paginated-result';
import { CreateRequest, UpdateRequest, MessageParameters } from '../messages/messages.models';

// environment
import { environment } from '../../../environments/environment';

@Injectable
({
	providedIn: 'root'
})

export class MessagesService
{
	/**
	 * The user id template name.
	 */
	private readonly userID = 'userID';

	/**
	 * The messages API base url.
	 */
	private readonly baseURL = environment.apiUrl + `users/${this.userID}/messages/`;

	/**
	 * Creates an instance of the messages service.
	 *
	 * @param http The http client.
	 */
	public constructor (private readonly http: HttpClient)
	{
		// Nothing to do here.
	}

	/**
	 * Creates a message (the user id is mandatory).
	 *
	 * @param userID The user id.
	 * @param model The model.
	 */
	public create (userID: string, model: CreateRequest): Observable<Message>
	{
		const observable = this.http.post<Message>(this.baseURL.replace(this.userID, userID), model);

		return observable;
	}

	/**
	 * Updates a message (the message id and user id are mandatory).
	 *
	 * @param messageID The message ID.
	 * @param userID The user id.
	 * @param model The model.
	 */
	public update (messageID: string, userID: string, model: UpdateRequest): Observable<void>
	{
		const observable = this.http.put<void>(this.baseURL.replace(this.userID, userID) + messageID, model);

		return observable;
	}

	/**
	 * Deletes a message (the message id and user id are mandatory).
	 *
	 * @param messageID The message ID.
	 * @param userID The user id.
	 */
	public delete (messageID: string, userID: string): Observable<void>
	{
		const observable = this.http.delete<void>(this.baseURL.replace(this.userID, userID) + messageID);

		return observable;
	}

	/**
	 * Gets a message (the message id and user id are mandatory).
	 *
	 * @param messageID The message ID.
	 * @param userID The user id.
	 */
	public get (messageID: string, userID: string): Observable<Message>
	{
		const observable = this.http.get<Message>(this.baseURL.replace(this.userID, userID) + messageID);

		return observable;
	}

	/**
	 * Gets all messages (the user id is mandatory).
	 *
	 * @param userID The user id.
	 * @param pageNumber The page number.
	 * @param pageSize The page size.
	 * @param filterParameters The filter parameters.
	 */
	public getAll (userID: string, pageNumber?: number, pageSize?: number, filterParameters?: MessageParameters): Observable<PaginatedResult<Message>>
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

		const observable = this.http.get<Message[]>(this.baseURL.replace(this.userID, userID), { observe: 'response', params: parameters }).pipe(map
		(
			(response) =>
			{
				const paginatedResult: PaginatedResult<Message> = new PaginatedResult<Message>();
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
	 * Gets a message (the message id and user id are mandatory).
	 *
	 * @param senderID The sender id.
	 * @param recipientID The recipient ID.
	 */
	public getThread (senderID: string, recipientID: string): Observable<Message[]>
	{
		const observable = this.http.get<Message[]>(this.baseURL.replace(this.userID, senderID) + `thread/${recipientID}`);

		return observable;
	}
}