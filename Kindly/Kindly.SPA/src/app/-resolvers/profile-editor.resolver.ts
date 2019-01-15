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
import { AuthService } from '../-services/auth/auth.service';

@Injectable()
export class ProfileEditorResolver implements Resolve<User>
{
	/**
	 * Creates an instance of the profile editor resolver.
	 *
	 * @param router The router.
	 * @param authApi The auth service.
	 * @param usersApi The users service.
	 * @param alertify The alertify service.
	 */
	public constructor
	(
		private readonly router: Router,
		private readonly authApi: AuthService,
		private readonly usersApi: UsersService,
		private readonly alertify: AlertifyService
	)
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
		const id = this.authApi.user.id;

		return this.usersApi.get(id).pipe
		(
			catchError
			((error) =>
			{
				this.alertify.error('Problem retrieving the profile.');
				this.router.navigate(['/home']);

				return of(null);
			})
		);
	}
}