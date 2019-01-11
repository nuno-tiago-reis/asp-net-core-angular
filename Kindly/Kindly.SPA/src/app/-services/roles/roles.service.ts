// components
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// models
import { Role } from '../../-models/role';
import { PaginatedResult } from '../../-models/paginated-result';
import { CreateRequest, UpdateRequest, RoleParameters } from '../roles/roles.models';

// environment
import { environment } from '../../../environments/environment';

@Injectable
({
	providedIn: 'root'
})

export class RolesService
{
	/**
	 * The roles API base url.
	 */
	private readonly baseURL = environment.apiUrl + `roles/`;

	/**
	 * Creates an instance of the roles service.
	 *
	 * @param http The http client.
	 */
	public constructor (private readonly http: HttpClient)
	{
		// Nothing to do here.
	}

	/**
	 * Creates a role.
	 *
	 * @param model The model.
	 */
	public create (model: CreateRequest): Observable<Role>
	{
		const observable = this.http.post<Role>(this.baseURL, model);

		return observable;
	}

	/**
	 * Updates a role (the role id is mandatory).
	 *
	 * @param roleID The role ID.
	 * @param model The model.
	 */
	public update (roleID: string, model: UpdateRequest): Observable<void>
	{
		const observable = this.http.put<void>(this.baseURL + roleID, model);

		return observable;
	}

	/**
	 * Deletes a role (the role id ismandatory).
	 *
	 * @param roleID The role ID.
	 */
	public delete (roleID: string, userID: string): Observable<void>
	{
		const observable = this.http.delete<void>(this.baseURL + roleID);

		return observable;
	}

	/**
	 * Gets a role (the role id and user id are mandatory).
	 *
	 * @param roleID The role ID.
	 */
	public get (roleID: string): Observable<Role>
	{
		const observable = this.http.get<Role>(this.baseURL + roleID);

		return observable;
	}

	/**
	 * Gets all roles.
	 *
	 * @param pageNumber The page number.
	 * @param pageSize The page size.
	 * @param filterParameters The filter parameters.
	 */
	public getAll (pageNumber?: number, pageSize?: number, filterParameters?: RoleParameters): Observable<PaginatedResult<Role>>
	{
		let parameters = new HttpParams();

		if (pageNumber !== null && pageSize !== null)
		{
			parameters = parameters.append('pageNumber', pageNumber.toString());
			parameters = parameters.append('pageSize', pageSize.toString());
		}

		if (filterParameters != null)
		{
			// Nothing to do here
		}

		const observable = this.http.get<Role[]>(this.baseURL, { observe: 'response', params: parameters }).pipe(map
		(
			(response) =>
			{
				const paginatedResult: PaginatedResult<Role> = new PaginatedResult<Role>();
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