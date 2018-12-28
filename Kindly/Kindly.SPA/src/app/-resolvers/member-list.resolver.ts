// components
import { Injectable } from '@angular/core';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

// services
import { UsersService } from '../-services/users/users.service';
import { AlertifyService } from '../-services/alertify/alertify.service';

// models
import { User } from '../-models/user';
import { PaginatedResult } from '../-models/paginated-result';

@Injectable()

export class MemberListResolver implements Resolve<PaginatedResult<User>>
{
	public readonly pageNumber = 1;
	public readonly pageSize = 18;

	/**
	 * Creates an instance of the member detail resolver.
	 *
	 * @param router The router.
	 * @param usersApi The users service.
	 * @param alertify The alertify service.
	 */
	public constructor(private router: Router, private usersApi: UsersService, private alertify: AlertifyService)
	{
		// Nothing to do here.
	}

	/**
	 * Resolves a route.
	 *
	 * @param route the route.
	 */
	public resolve(route: ActivatedRouteSnapshot): Observable<PaginatedResult<User>>
	{
		return this.usersApi.getAll(this.pageNumber, this.pageSize).pipe
		(
			catchError
			((error) =>
			{
				this.alertify.error('Problem retrieving members data.');
				this.router.navigate(['/home']);

				return of(null);
			})
		);
	}
}