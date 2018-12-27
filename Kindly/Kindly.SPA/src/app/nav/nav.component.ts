// components
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

// services
import { AuthService } from '../-services/auth/auth.service';
import { AlertifyService } from '../-services/alertify/alertify.service';

// models
import { LoginWithUserNameRequest } from '../-services/auth/auth.models';
import { DecodedToken } from '../-models/token';
import { User } from '../-models/user';

@Component
({
	selector: 'app-nav',
	templateUrl: './nav.component.html',
	styleUrls: [ './nav.component.css' ]
})

export class NavComponent implements OnInit
{
	/**
	 * The login request.
	 */
	public loginRequest: LoginWithUserNameRequest =
	{
		userName: '',
		password: ''
	};

	/**
	 * Creates an instance of the nav component.
	 *
	 * @param authApi The auth service.
	 */
	public constructor (public authApi: AuthService, private alertify: AlertifyService, private router: Router)
	{
		// Nothing to do here.
	}

	/**
	 * A lifecycle hook that is called after Angular has initialized all data-bound properties of a directive.
	 */
	public ngOnInit (): void
	{
		// Nothing to do here.
	}

	/**
	 * Called when the log in button is clicked on.
	 */
	public logIn (): void
	{
		this.authApi.logInWithUserName(this.loginRequest).subscribe
		(
			(next: any) =>
			{
				this.alertify.success('Logged in successfully.');
				this.router.navigate(['/members']);
			},
			(error: any) =>
			{
				this.alertify.error(error);
			}
		);
	}

	/**
	 * Called when the log out button is clicked on.
	 */
	public logOut (): void
	{
		this.authApi.logOut();

		this.alertify.success('Logged out successfully.');
		this.router.navigate(['/home']);
	}

	/**
	 * Checks whether the user is logged in.
	 */
	public isLoggedIn(): boolean
	{
		return this.authApi.isLoggedIn();
	}

	/**
	 * Checks whether the user is logged out.
	 */
	public isLoggedOut (): boolean
	{
		return this.authApi.isLoggedOut();
	}
}