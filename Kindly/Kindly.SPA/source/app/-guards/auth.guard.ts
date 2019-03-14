// components
import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

// services
import { AuthService } from '../-services/auth/auth.service';
import { AlertifyService } from '../-services/alertify/alertify.service';

@Injectable
(({
	providedIn: 'root'
}) as any)

export class AuthGuard implements CanActivate
{
	/**
	 * Creates an instance of the auth guard.
	 *
	 * @param router The router.
	 * @param authApi The auth service.
	 * @param alertify The alertify service.
	 */
	public constructor
	(
		private readonly authApi: AuthService,
		private readonly router: Router,
		private readonly alertify: AlertifyService
	)
	{
		// Nothing to do here.
	}

	/**
	 * Checks if a route can be activated.
	 *
	 * @param next The activated route.
	 */
	public canActivate (next: ActivatedRouteSnapshot): Observable<boolean> | Promise<boolean> | boolean
	{
		const roles = next.firstChild.data['roles'] as Array<string>;

		if (roles && this.authApi.isInRoles(roles) === false)
		{
			this.alertify.error('You are not authorized to access this page.');
			this.router.navigate(['home']);

			return false;
		}

		if (this.authApi.isLoggedIn() === false)
		{
			this.alertify.error('You need to log in to access this page.');
			this.router.navigate(['home']);

			return false;
		}

		return true;
	}
}