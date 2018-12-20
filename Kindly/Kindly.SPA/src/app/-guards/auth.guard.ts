// components
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';

// services
import { AuthService } from '../-services/auth/auth.service';
import { AlertifyService } from '../-services/alertify/alertify.service';

@Injectable
({
	providedIn: 'root'
})

export class AuthGuard implements CanActivate
{
	/**
	 * Creates an instance of the auth guard.
	 */
	public constructor(private auth: AuthService, private router: Router, private alertify: AlertifyService)
	{
		// Nothing to do here.
	}

	public canActivate (): Observable<boolean> | Promise<boolean> | boolean
	{
		if (this.auth.isLoggedIn())
			return true;

		this.alertify.error('You need to log in to access this page.');
		this.router.navigate(['home']);

		return false;
	}
}