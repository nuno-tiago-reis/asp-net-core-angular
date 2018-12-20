// components
import { Injectable } from '@angular/core';
import { Router, Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';

// services
import { UsersService } from '../-services/users/users.service';
import { AlertifyService } from '../-services/alertify/alertify.service';

// models
import { User } from '../-models/user';
import { catchError } from 'rxjs/operators';

@Injectable()

export class MemberDetailResolver implements Resolve<User>
{
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
	public resolve(route: ActivatedRouteSnapshot): Observable<User>
	{
		return this.usersApi.get(route.params['id']).pipe
		(
			catchError
			((error) =>
			{
				this.alertify.error('Problem retrieving user data.');
				this.router.navigate(['/members']);

				return of(null);
			})
		);
	}
}