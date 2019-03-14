// components
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

// services
import { AuthService } from '../-services/auth/auth.service';
import { AlertifyService } from '../-services/alertify/alertify.service';

// models
import { LoginWithUserNameRequest } from '../-services/auth/auth.models';

@Component
({
	selector: 'app-navbar',
	templateUrl: './navbar.component.html',
	styleUrls: [ './navbar.component.css' ]
})

export class NavBarComponent implements OnInit
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
	 * Creates an instance of the nav bar component.
	 *
	 * @param router The router.
	 * @param authApi The auth service.
	 * @param alertify The alertify service.
	 */
	public constructor (public router: Router, public authApi: AuthService, private readonly alertify: AlertifyService)
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
				this.alertify.error('An error occured while logging in.');
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