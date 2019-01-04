// components
import { Injectable } from '@angular/core';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

// services
import { AuthService } from '../-services/auth/auth.service';
import { MessagesService } from '../-services/messages/messages.service';
import { AlertifyService } from '../-services/alertify/alertify.service';

// models
import { Message } from '../-models/message';
import { PaginatedResult } from '../-models/paginated-result';
import { MessageParameters, ContainerMode } from '../-services/messages/messages.models';

@Injectable()
export class MessagesResolver implements Resolve<PaginatedResult<Message>>
{
	/**
	 * The page number.
	 */
	public readonly pageNumber = 1;

	/**
	 * The page size.
	 */
	public readonly pageSize = 18;

	/**
	 * The filter parameters.
	 */
	public readonly filterParameters: MessageParameters =
	{
		container: ContainerMode.Unread
	};

	/**
	 * Creates an instance of the member list resolver.
	 *
	 * @param router The router.
	 * @param authApi The auth service.
	 * @param messagesApi The messages service.
	 * @param alertify The alertify service.
	 */
	public constructor(private router: Router, private authApi: AuthService, private messagesApi: MessagesService, private alertify: AlertifyService)
	{
		// Nothing to do here.
	}

	/**
	 * Resolves a route.
	 *
	 * @param route the route.
	 */
	public resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<Message>>
	{
		return this.messagesApi.getAll(this.authApi.user.id, this.pageNumber, this.pageSize, this.filterParameters).pipe
		(
			catchError
			((error) =>
			{
				this.alertify.error('Problem retrieving the messages.');
				this.router.navigate(['/home']);

				return of(null);
			})
		);
	}
}